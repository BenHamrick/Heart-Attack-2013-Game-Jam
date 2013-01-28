using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HeartbeatGlobalGameJam2013
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ScreenLoader.ScreenLoader.LoadScreen("SplashScreen");

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

            float screenscale = graphics.PreferredBackBufferWidth / 1280f;
            publicStatics.spriteScale = Matrix.CreateScale(screenscale, screenscale, 1);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            publicStatics.fullScreen = HighScores.Default.FullScreen;
            publicStatics.soundsEnabled = HighScores.Default.SoundsEnabled;
            ScreenLoader.ScreenLoader.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Sound.LoadContent(Content);
            Sound.PlaySong("01_Touhou_pt1");
            MediaPlayer.IsRepeating = true;
            Image.LoadImages(Content);
            Font.LoadFonts(Content);
            ScreenLoader.ScreenLoader.LoadContent(this.Content, graphics);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            ScreenLoader.ScreenLoader.UnloadContent();
            HighScores.Default.FullScreen = publicStatics.fullScreen;
            HighScores.Default.SoundsEnabled = publicStatics.soundsEnabled;
            HighScores.Default.Save();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            ScreenLoader.ScreenLoader.Update(gameTime);
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (publicStatics.exitGame)
                this.Exit();
            if (publicStatics.fullScreen && graphics.IsFullScreen == false)
            {
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
                float screenscale = GraphicsDevice.Viewport.Width / 1280f;
                publicStatics.spriteScale = Matrix.CreateScale(screenscale, screenscale, 1);
            }
            else if (publicStatics.fullScreen == false && graphics.IsFullScreen == true)
            {
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
                float screenscale = GraphicsDevice.Viewport.Width / 1280f;
                publicStatics.spriteScale = Matrix.CreateScale(screenscale, screenscale, 1);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            ScreenLoader.ScreenLoader.Draw(spriteBatch, GraphicsDevice);
            base.Draw(gameTime);
        }
    }
}
