using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HeartbeatGlobalGameJam2013.ScreenLoader
{
    /// <summary>
    /// Class that handels all the actions, loading, and unloading of active screens
    /// </summary>
    class ScreenLoader
    {

        public static List<Screen> screen = new List<Screen>();//stores all the screens
        public static Microsoft.Xna.Framework.Content.ContentManager content;//stores the content manager used for the screens
        public static GraphicsDeviceManager graphics;//stores the graphics manager used for the screens
        public static Microsoft.Xna.Framework.GameTime gameTime;//stares the game time used for the screens

        /// <summary>
        /// This loads is the first method that must be called and it decides what screen to load first
        /// </summary>
        /// <param name="scr">A string representing the screen name</param>
        public static void LoadScreen(string scr)
        {
            screen.Add((Screen)Activator.CreateInstance(Type.GetType("HeartbeatGlobalGameJam2013.Screens." + scr)));//adds screen to screen list
        }

        /// <summary>
        /// This is the initialize call and just needs to be called once from initialize in the main game class
        /// </summary>
        public static void Initialize()
        {
            screen[screen.Count - 1].Initialize();//initializes top most screen
        }

        /// <summary>
        /// This loads all the content from the screen also needs to be called once from the main game class
        /// </summary>
        /// <param name="con">the content manager that all the screens will use</param>
        /// <param name="con">the graphics manager that all the screens will use</param>
        public static void LoadContent(Microsoft.Xna.Framework.Content.ContentManager con, GraphicsDeviceManager grap)
        {
            content = con;//stores contenmanager
            graphics = grap;//stores graphics manager
            screen[screen.Count - 1].LoadContent(content, graphics);//loadScreen the content for the screen
        }

        /// <summary>
        /// This unloads all the content and gets automatically called by screenloader. It may sometimes need to be used at some point
        /// </summary>
        public static void UnloadContent()
        {
            for (int i = 0; i < screen.Count; i++)//runs for every screen
                screen[i].UnloadContent();//unloads every screen
        }

        /// <summary>
        /// This is the update method and must be called every update from the main game class for this screen loader to work
        /// </summary>
        /// <param name="gameT">the game time</param>
        public static void Update(Microsoft.Xna.Framework.GameTime gameT)
        {
            gameTime = gameT;//stores gametime
            screen[screen.Count - 1].Update(gameTime);//updates top most screen
        }

        /// <summary>
        /// This draws all the screens and must be called from the amin game draw loop
        /// </summary>
        /// <param name="spriteBatch">the spriteBatch that will be used to draw all the screens</param>
        /// <param name="graphicsDevice">the graphics device that will be used by all screens</param>
        public static void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            for (int i = 0; i < screen.Count; i++)//runs through all screens
                screen[i].draw(spriteBatch, graphicsDevice);//draws each screen
        }

        /// <summary>
        /// This loads the next screen and unloads the screen that is currently active
        /// </summary>
        /// <param name="scr">name of the screen</param>
        public static void LoadNextScreen(string scr)
        {
            UnloadContent();//unloads all screens
            screen.Clear();//clears screen list
            screen.Add((Screen)Activator.CreateInstance(Type.GetType("HeartbeatGlobalGameJam2013.Screens." + scr)));//adds the new screen
            Initialize();//initializes new screen
            LoadContent(content, graphics);//loads content of new screen
        }

        /// <summary>
        /// This loads the next screen and unloads the screen that is currently active
        /// </summary>
        /// <param name="scr">name of the screen</param>
        /// <param name="parameters">any parameters that the screen has</param>
        public static void LoadNextScreen(string scr, object[] parameters)
        {
            UnloadContent();//unloads all screens
            screen.Clear();//clears screen list
            screen.Add((Screen)Activator.CreateInstance(Type.GetType("HeartbeatGlobalGameJam2013.Screens." + scr), parameters));//adds the new screen with parameters
            Initialize();//initializes new screen
            LoadContent(content, graphics);//loads content of new screen
        }

        /// <summary>
        /// This loads the next screen ontop of the current screen and pauses the update loop of the screen below it
        /// </summary>
        /// <param name="scr">name of the screen</param>
        public static void PushScreen(string scr)
        {
            screen.Add((Screen)Activator.CreateInstance(Type.GetType("HeartbeatGlobalGameJam2013.Screens." + scr)));//adds new screen to the list
            Initialize();//initlizes the new screen
            LoadContent(content, graphics);//loads content for new screen
        }

        /// <summary>
        /// This loads the next screen ontop of the current screen and pauses the update loop of the screen below it
        /// </summary>
        /// <param name="scr">name of the screen</param>
        /// <param name="parameters">any parameters that the screen has</param>
        public static void PushScreen(string scr, object[] parameters)
        {
            screen.Add((Screen)Activator.CreateInstance(Type.GetType("HeartbeatGlobalGameJam2013.Screens." + scr), parameters));//adds new screen to the list with paramerters
            Initialize();//initlizes the new screen
            LoadContent(content, graphics);//loads content for new screen
        }

        /// <summary>
        /// Unloads the top most screen and resuems the update an the screen below it
        /// </summary>
        public static void PopScreen()
        {
            screen[screen.Count - 1].UnloadContent();//unloads top most screen
            screen.RemoveAt(screen.Count - 1);//removes top most screen from screen list
        }
    }
}
