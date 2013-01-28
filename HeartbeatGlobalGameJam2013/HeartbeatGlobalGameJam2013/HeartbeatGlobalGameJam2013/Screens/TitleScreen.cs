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

namespace HeartbeatGlobalGameJam2013.Screens
{
    class TitleScreen : Screen
    {
        List<Label> labels = new List<Label>();
        List<Button> buttons = new List<Button>();
        List<Checkbox> checkboxes = new List<Checkbox>();
        Cursor cursor = new Cursor();
        Texture2D imageRBC;
        Microsoft.Xna.Framework.Content.ContentManager Content;
        Microsoft.Xna.Framework.GraphicsDeviceManager Graphics;
        
        public TitleScreen()
        {
            
        }

        public override void Initialize()
        {
            
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.GraphicsDeviceManager graphics)
        {
            Content = content;
            Graphics = graphics;

            cursor.LoadContent(content);

            loadAllContent(content, graphics);

            //FullScreen Toggle
            checkboxes.Add(new Checkbox(new Rectangle(publicStatics.screenSize.Right - 47, publicStatics.screenSize.Bottom - 47, 32, 32)));
            checkboxes[0].Checked = publicStatics.fullScreen;
            //Sound Toggle
            checkboxes.Add(new Checkbox(new Rectangle(15, publicStatics.screenSize.Bottom - 47, 32, 32)));
            checkboxes[1].Checked = publicStatics.soundsEnabled;
        }

        public void UnloadContent()
        {
            buttons.Clear();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Updates for events in the buttons
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].UpdateEvents();
            }
            if (buttons[0].Clicked())//Start
            {
                ScreenLoader.ScreenLoader.LoadNextScreen("GamePlay");
            }
            if (buttons[1].Clicked())//HowToPlay
            {
                ScreenLoader.ScreenLoader.LoadNextScreen("HowToPlay");
            }
            if (buttons[2].Clicked())//Highscore
            {
                ScreenLoader.ScreenLoader.LoadNextScreen("HighScore");
            }
            if (buttons[3].Clicked())//Exit
            {
                publicStatics.exitGame = true;
            }

            //Update Checkboxes
            for (int i = 0; i < checkboxes.Count; i++)
            {
                checkboxes[i].UpdateEvents();
            }
            if (checkboxes[0].Checked && publicStatics.fullScreen == false)
            {
                publicStatics.fullScreen = true;
                UnloadContent();
                loadAllContent(Content, Graphics);
            }
            else if (checkboxes[0].Checked == false && publicStatics.fullScreen)
            {
                publicStatics.fullScreen = false;
                UnloadContent();
                loadAllContent(Content, Graphics);
            }

            if (checkboxes[1].Clicked())
            {
                publicStatics.soundsEnabled = !publicStatics.soundsEnabled;
                checkboxes[1].Checked = publicStatics.soundsEnabled;
                if (publicStatics.soundsEnabled == false)
                    MediaPlayer.Pause();
                if (publicStatics.soundsEnabled)
                    MediaPlayer.Resume();
            }

            //Update Mouse Cursor
            cursor.Update(gameTime);
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, publicStatics.spriteScale);
            //Background
            spriteBatch.Draw(Image.titleScreen, new Rectangle(0, 0, publicStatics.screenSize.Width, publicStatics.screenSize.Height), Color.White);

            //Draw all labels
            for(int i = 0; i < labels.Count; i++)
            {
                labels[i].Draw(spriteBatch);
            }

            //Draw Bullet Points
            if (buttons[0].mouseHover())
                spriteBatch.Draw(imageRBC, new Rectangle(publicStatics.screenSize.Center.X - 128, publicStatics.screenSize.Center.Y - 27 - 20, 48, 48), Color.White);
            if (buttons[1].mouseHover())
                spriteBatch.Draw(imageRBC, new Rectangle(publicStatics.screenSize.Center.X - 244, publicStatics.screenSize.Center.Y - 27 + 55, 48, 48), Color.White);
            if (buttons[2].mouseHover())
                spriteBatch.Draw(imageRBC, new Rectangle(publicStatics.screenSize.Center.X - 230, publicStatics.screenSize.Center.Y - 27 + 130, 48, 48), Color.White);
            if (buttons[3].mouseHover())
                spriteBatch.Draw(imageRBC, new Rectangle(publicStatics.screenSize.Center.X - 112, publicStatics.screenSize.Center.Y - 27 + 205, 48, 48), Color.White);

            //Draw all Buttons
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(spriteBatch);
            }

            //Draw all Checkboxes
            for (int i = 0; i < checkboxes.Count; i++)
            {
                checkboxes[i].Draw(spriteBatch);
            }

            //Draw Mouse Cursor
            cursor.drawTexture(spriteBatch, graphicsDevice);
            spriteBatch.End();
        }

        void loadAllContent(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.GraphicsDeviceManager graphics)
        {
            

            //Start Button
            buttons.Add(new Button("Start", new Vector2(publicStatics.screenSize.Center.X, publicStatics.screenSize.Center.Y - 25), Font.cooper48, Color.White));
            //HowToPlay Button
            buttons.Add(new Button("How To Play", new Vector2(publicStatics.screenSize.Center.X, publicStatics.screenSize.Center.Y + 55), Font.cooper48, Color.White));
            //HighScores Button
            buttons.Add(new Button("High Scores", new Vector2(publicStatics.screenSize.Center.X, publicStatics.screenSize.Center.Y + 135), Font.cooper48, Color.White));
            //Exit Button
            buttons.Add(new Button("Exit", new Vector2(publicStatics.screenSize.Center.X, publicStatics.screenSize.Center.Y + 210), Font.cooper48, Color.White));
            

            //BulletPoints
            imageRBC = Image.redBloodCell;

            //Toggle Full Screen
            labels.Add(new Label("Full Screen", new Vector2(publicStatics.screenSize.Right - 62 - Font.cooper32.MeasureString("Full Screen").X / 2, publicStatics.screenSize.Bottom - Font.cooper32.MeasureString("Full Screen").Y / 2 - 10), Font.cooper32, Color.White));
            //Toggle Sound Label
            labels.Add(new Label("Enable Sound", new Vector2(62 + Font.cooper32.MeasureString("Enable Sound").X / 2, publicStatics.screenSize.Bottom - Font.cooper32.MeasureString("Enable Sound").Y / 2 - 10), Font.cooper32, Color.White));
        }
    }
}
