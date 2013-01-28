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
    class HighScore : Screen
    {
        List<Label> labels = new List<Label>();
        List<Button> buttons = new List<Button>();
        List<Textbox> textboxes = new List<Textbox>();
        Cursor cursor = new Cursor();
        Microsoft.Xna.Framework.Content.ContentManager Content;
        Microsoft.Xna.Framework.GraphicsDeviceManager Graphics;
        int[] hScores = new int[10];
        string[] hScoreNames = new string[10];
        int nameToChange = 11;

        public HighScore()
        {
            //put all HS into an array
            hScores[0] = HighScores.Default.UserScore1;
            hScores[1] = HighScores.Default.UserScore2;
            hScores[2] = HighScores.Default.UserScore3;
            hScores[3] = HighScores.Default.UserScore4;
            hScores[4] = HighScores.Default.UserScore5;
            hScores[5] = HighScores.Default.UserScore6;
            hScores[6] = HighScores.Default.UserScore7;
            hScores[7] = HighScores.Default.UserScore8;
            hScores[8] = HighScores.Default.UserScore9;
            hScores[9] = HighScores.Default.UserScore10;
            //Add names to an array
            hScoreNames[0] = HighScores.Default.User1;
            hScoreNames[1] = HighScores.Default.User2;
            hScoreNames[2] = HighScores.Default.User3;
            hScoreNames[3] = HighScores.Default.User4;
            hScoreNames[4] = HighScores.Default.User5;
            hScoreNames[5] = HighScores.Default.User6;
            hScoreNames[6] = HighScores.Default.User7;
            hScoreNames[7] = HighScores.Default.User8;
            hScoreNames[8] = HighScores.Default.User9;
            hScoreNames[9] = HighScores.Default.User10;
            //Determine position on HS list
            if (publicStatics.curScore > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (hScores[i] <= publicStatics.curScore)
                    {
                        for (int j = 9; j > i; j--)
                        {
                            hScores[j] = hScores[j - 1];
                            hScoreNames[j] = hScoreNames[j - 1];
                        }
                        hScores[i] = publicStatics.curScore;
                        nameToChange = i;
                        break;
                    }
                }
                updateNames();
            }
        }

        public override void Initialize()
        {
            
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.GraphicsDeviceManager graphics)
        {
            Content = content;
            Graphics = graphics;

            loadAllContent(content, graphics);
            textboxes.Add(new Textbox("", new Rectangle(20, 50, 150, 40), Font.cooper32, Color.White, 3));
            cursor.LoadContent(content);
        }

        public override void UnloadContent()
        {
            
            publicStatics.curScore = 0;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Updates for events in the buttons
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].UpdateEvents();
            }
            if (buttons[0].Clicked())//Resume
            {
                ScreenLoader.ScreenLoader.LoadNextScreen("TitleScreen");
            }
            for (int i = 0; i < textboxes.Count; i++)
            {
                textboxes[i].Update(gameTime);
            }
            if (nameToChange != 11)
            {
                hScoreNames[nameToChange] = textboxes[0].text;
            }
            updateNames();

            loadAllContent(Content, Graphics);

            //Update Mouse Cursor
            cursor.Update(gameTime);
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, publicStatics.spriteScale);
            //Background
            spriteBatch.Draw(Image.highScore, new Rectangle(0, 0, publicStatics.screenSize.Width, publicStatics.screenSize.Height), Color.White);

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
            
            //Draw all textboxes
            for (int i = 0; i < textboxes.Count; i++)
            {
                textboxes[i].Draw(spriteBatch);
            }

            //Draw Mouse Cursor
            cursor.drawTexture(spriteBatch, graphicsDevice);
            spriteBatch.End();
        }

        void loadAllContent(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.GraphicsDeviceManager graphics)
        {
            buttons.Clear();
            labels.Clear();
            //Start Button
            buttons.Add(new Button("Continue", new Vector2(publicStatics.screenSize.Right - 30 - (Font.cooper32.MeasureString("Resume").X / 2), publicStatics.screenSize.Bottom - 10 - (Font.cooper32.MeasureString("Resume").Y / 2)), Font.cooper32, Color.White));

            labels.Add(new Label("Your Score: " + (int)(publicStatics.curScore * .1), new Vector2(20 + Font.cooper32.MeasureString("Your Score: " + (int)(publicStatics.curScore * .1)).X / 2, 20 + Font.cooper32.MeasureString("Your Score: " + (int)(publicStatics.curScore * .1)).Y / 2), Font.cooper32, Color.White));

            //Score 1
            labels.Add(new Label("1: " + HighScores.Default.User1 + " : " + (int)(HighScores.Default.UserScore1 * .1), new Vector2(publicStatics.screenSize.Center.X, 65 * 1), Font.cooper32, Color.White));
            //Score 2
            labels.Add(new Label("2: " + HighScores.Default.User2 + " : " + (int)(HighScores.Default.UserScore2 * .1), new Vector2(publicStatics.screenSize.Center.X, 65 * 2), Font.cooper32, Color.White));
            //Score 3
            labels.Add(new Label("3: " + HighScores.Default.User3 + " : " + (int)(HighScores.Default.UserScore3 * .1), new Vector2(publicStatics.screenSize.Center.X, 65 * 3), Font.cooper32, Color.White));
            //Score 4
            labels.Add(new Label("4: " + HighScores.Default.User4 + " : " + (int)(HighScores.Default.UserScore4 * .1), new Vector2(publicStatics.screenSize.Center.X, 65 * 4), Font.cooper32, Color.White));
            //Score 5
            labels.Add(new Label("5: " + HighScores.Default.User5 + " : " + (int)(HighScores.Default.UserScore5 * .1), new Vector2(publicStatics.screenSize.Center.X, 65 * 5), Font.cooper32, Color.White));
            //Score 6
            labels.Add(new Label("6: " + HighScores.Default.User6 + " : " + (int)(HighScores.Default.UserScore6 * .1), new Vector2(publicStatics.screenSize.Center.X, 65 * 6), Font.cooper32, Color.White));
            //Score 7
            labels.Add(new Label("7: " + HighScores.Default.User7 + " : " + (int)(HighScores.Default.UserScore7 * .1), new Vector2(publicStatics.screenSize.Center.X, 65 * 7), Font.cooper32, Color.White));
            //Score 8
            labels.Add(new Label("8: " + HighScores.Default.User8 + " : " + (int)(HighScores.Default.UserScore8 * .1), new Vector2(publicStatics.screenSize.Center.X, 65 * 8), Font.cooper32, Color.White));
            //Score 9
            labels.Add(new Label("9: " + HighScores.Default.User9 + " : " + (int)(HighScores.Default.UserScore9 * .1), new Vector2(publicStatics.screenSize.Center.X, 65 * 9), Font.cooper32, Color.White));
            //Score 10
            labels.Add(new Label("10: " + HighScores.Default.User10 + " : " + (int)(HighScores.Default.UserScore10 * .1), new Vector2(publicStatics.screenSize.Center.X, 65 * 10), Font.cooper32, Color.White));
        }

        void updateNames()
        {
            HighScores.Default.User1 = hScoreNames[0];
            HighScores.Default.User2 = hScoreNames[1];
            HighScores.Default.User3 = hScoreNames[2];
            HighScores.Default.User4 = hScoreNames[3];
            HighScores.Default.User5 = hScoreNames[4];
            HighScores.Default.User6 = hScoreNames[5];
            HighScores.Default.User7 = hScoreNames[6];
            HighScores.Default.User8 = hScoreNames[7];
            HighScores.Default.User9 = hScoreNames[8];
            HighScores.Default.User10 = hScoreNames[9];
            HighScores.Default.UserScore1 = hScores[0];
            HighScores.Default.UserScore2 = hScores[1];
            HighScores.Default.UserScore3 = hScores[2];
            HighScores.Default.UserScore4 = hScores[3];
            HighScores.Default.UserScore5 = hScores[4];
            HighScores.Default.UserScore6 = hScores[5];
            HighScores.Default.UserScore7 = hScores[6];
            HighScores.Default.UserScore8 = hScores[7];
            HighScores.Default.UserScore9 = hScores[8];
            HighScores.Default.UserScore10 = hScores[9];
            HighScores.Default.Save();
        }
    }
}