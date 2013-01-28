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
    class HowToPlay : Screen
    {
        List<Label> labels = new List<Label>();
        List<Button> buttons = new List<Button>();
        List<Checkbox> checkboxes = new List<Checkbox>();
        Cursor cursor = new Cursor();
        Microsoft.Xna.Framework.Content.ContentManager Content;
        Microsoft.Xna.Framework.GraphicsDeviceManager Graphics;
        
        public HowToPlay()
        {
            
        }

        public override void Initialize()
        {
            
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
            buttons.Clear();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Updates for events in the buttons
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].UpdateEvents();
            }
            if (Input.lmbClicked)//Continue
            {
                ScreenLoader.ScreenLoader.LoadNextScreen("TitleScreen");
            }

            //Update Checkboxes
            for (int i = 0; i < checkboxes.Count; i++)
            {
                checkboxes[i].UpdateEvents();
            }

            //Update Mouse Cursor
            cursor.Update(gameTime);
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, publicStatics.spriteScale);
            //Background
            spriteBatch.Draw(Image.howToPlayMenu, new Rectangle(0, 0, publicStatics.screenSize.Width, publicStatics.screenSize.Height), Color.White);

            //Draw all labels
            for(int i = 0; i < labels.Count; i++)
            {
                labels[i].Draw(spriteBatch);
            }

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
            buttons.Add(new Button("Continue", new Vector2(publicStatics.screenSize.Center.X, publicStatics.screenSize.Center.Y), Font.cooper32, Color.White));
        }
    }
}
