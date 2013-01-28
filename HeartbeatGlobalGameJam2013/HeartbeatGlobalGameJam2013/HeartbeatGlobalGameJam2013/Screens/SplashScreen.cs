using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HeartbeatGlobalGameJam2013;
using HeartbeatGlobalGameJam2013.Objects;
using HeartbeatGlobalGameJam2013.ScreenLoader;
using System.Diagnostics;

namespace HeartbeatGlobalGameJam2013.Screens
{
    class SplashScreen : Screen
    {
        List<Label> labels = new List<Label>();
        Cursor cursor = new Cursor();
        Microsoft.Xna.Framework.Content.ContentManager Content;
        Microsoft.Xna.Framework.GraphicsDeviceManager Graphics;
        Stopwatch startTimer;
        bool smallerStart = true;

        public SplashScreen()
        {
            startTimer = new Stopwatch();
        }

        public override void Initialize()
        {
            startTimer.Start();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.GraphicsDeviceManager graphics)
        {
            Content = content;
            Graphics = graphics;

            loadAllContent(content, graphics);
            cursor.LoadContent(content);
        }

        public void UnloadContent()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Input.lmbClicked)
                ScreenLoader.ScreenLoader.LoadNextScreen("HowToPlay");
            //Update Mouse Cursor
            cursor.Update(gameTime);
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, publicStatics.spriteScale);
            //Background
            spriteBatch.Draw(Image.splashScreen, new Rectangle(0, 0, publicStatics.screenSize.Width, publicStatics.screenSize.Height), Color.White);
            if (startTimer.ElapsedMilliseconds > 500 && startTimer.ElapsedMilliseconds < 1000)
            {
                smallerStart = true;
            }
            if (startTimer.ElapsedMilliseconds > 1000)
            {
                smallerStart = false;
                startTimer.Restart();
            }
            if (smallerStart)
                spriteBatch.Draw(Image.splashStart, new Rectangle(publicStatics.screenSize.Center.X - 181, publicStatics.screenSize.Center.Y + 30, 362, 195), Color.White);
            else
                spriteBatch.Draw(Image.splashStart, new Rectangle(publicStatics.screenSize.Center.X - 181 - 20, publicStatics.screenSize.Center.Y + 30 - 10, 402, 215), Color.White);
            

            //Draw Mouse Cursor
            cursor.drawTexture(spriteBatch, graphicsDevice);
            spriteBatch.End();
        }

        void loadAllContent(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.GraphicsDeviceManager graphics)
        {
            //Start Button
        }
    }
}
