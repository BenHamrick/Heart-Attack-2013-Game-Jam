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
    class Textbox
    {
        public string text = "";
        public SpriteFont font = Font.cooper48;
        public Color color = Color.Black;
        Rectangle detection = new Rectangle();
        public ObjTexture image = Image.textButton;
        public int maxLength;
        bool hasFocus = false;
        Animation cursor = new Animation(Image.textCursor);

        public Textbox(string str, Rectangle SizeandLocation, SpriteFont Font, Color Color, int MaxLength)
        {
            text = str;
            font = Font;
            detection = SizeandLocation;
            color = Color;
            maxLength = MaxLength;

            cursor.startAnimation(0, 20);
        }

        public Textbox(string str, Rectangle SizeandLocation, SpriteFont Font, Color Color, ObjTexture Image, int MaxLength)
        {
            text = str;
            font = Font;
            detection = SizeandLocation;
            color = Color;
            image = Image;
            maxLength = MaxLength;

            cursor.startAnimation(0, 20);
        }

        public void Update(GameTime gameTime)
        {
            if (hasFocus)
            {
                currentKeyboardState = Input.KS;
                lastKeyboardState = Input.KSold;

                foreach (Keys key in keysToCheck)
                {
                    if (CheckKey(key))
                    {
                        AddKeyToText(key);
                        break;
                    }
                }

                if (lastKeyboardState.IsKeyDown(Keys.Back) && currentKeyboardState.IsKeyUp(Keys.Back))
                    if (text.Length != 0)
                        text = text.Remove(text.Length - 1);
            }
            if (Clicked())
                hasFocus = true;
            else if ((detection.Contains(Input.mousePosition) == false) && Input.lmbClicked)
                hasFocus = false;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (hasFocus)
            {
                spriteBatch.Draw(image.Texture2D, detection, Color.Yellow);
                cursor.Draw(spriteBatch, new Vector2(font.MeasureString(lastLine(parseText(text))).X - font.MeasureString(lastLine(" ")).X + detection.X + 19, detection.Y + font.LineSpacing / 2 + font.LineSpacing * countLines(parseText(text) + 5)), Color.White, 0, 1, 0);
            }
            else
                spriteBatch.Draw(image.Texture2D, detection, Color.White);
            spriteBatch.DrawString(font, parseText(text), new Vector2(detection.X + 15, detection.Y + 5), Color.White);
            
        }

        #region Events
        public Boolean Clicked()
        {
            if (detection.Contains(Input.mousePosition) && Input.lmbClicked)
                return true;
            else
                return false;
        }
        #endregion

        #region Text Positioning

        private String parseText(String text)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
                if (font.MeasureString(line + word).Length() > detection.Width && !line.Equals(String.Empty))
                {
                    returnString = returnString + line + '\n';
                    line = String.Empty;
                }

                line = line + word + " ";
            }

            return returnString + line;
        }

        private int countLines(String text)
        {
            int numLines = 0;
            for (int x = 0; x < text.Length; x++)
            {
                if (text.Substring(x, 1).Equals("\n"))
                    numLines += 1;
            }
            return numLines;
        }

        private String lastLine(String text)
        {
            String returnString = text;
            for (int x = 0; x < text.Length; x++)
            {
                if (text.Substring(x, 1).Equals("\n"))
                    returnString = text.Substring(x + 1);
            }
            return returnString;
        }

        #endregion

        #region Get Text

        Keys[] keysToCheck = new Keys[] { 
    Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
    Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
    Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
    Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
    Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
    Keys.Z, Keys.Back, Keys.Space };

        KeyboardState currentKeyboardState = Input.KS;
        KeyboardState lastKeyboardState = Input.KSold;

        private void AddKeyToText(Keys key)
        {
            string newChar = "";

            if (text.Length >= maxLength && key != Keys.Back)
                return;

            switch (key)
            {
                case Keys.A:
                    newChar += "a";
                    break;
                case Keys.B:
                    newChar += "b";
                    break;
                case Keys.C:
                    newChar += "c";
                    break;
                case Keys.D:
                    newChar += "d";
                    break;
                case Keys.E:
                    newChar += "e";
                    break;
                case Keys.F:
                    newChar += "f";
                    break;
                case Keys.G:
                    newChar += "g";
                    break;
                case Keys.H:
                    newChar += "h";
                    break;
                case Keys.I:
                    newChar += "i";
                    break;
                case Keys.J:
                    newChar += "j";
                    break;
                case Keys.K:
                    newChar += "k";
                    break;
                case Keys.L:
                    newChar += "l";
                    break;
                case Keys.M:
                    newChar += "m";
                    break;
                case Keys.N:
                    newChar += "n";
                    break;
                case Keys.O:
                    newChar += "o";
                    break;
                case Keys.P:
                    newChar += "p";
                    break;
                case Keys.Q:
                    newChar += "q";
                    break;
                case Keys.R:
                    newChar += "r";
                    break;
                case Keys.S:
                    newChar += "s";
                    break;
                case Keys.T:
                    newChar += "t";
                    break;
                case Keys.U:
                    newChar += "u";
                    break;
                case Keys.V:
                    newChar += "v";
                    break;
                case Keys.W:
                    newChar += "w";
                    break;
                case Keys.X:
                    newChar += "x";
                    break;
                case Keys.Y:
                    newChar += "y";
                    break;
                case Keys.Z:
                    newChar += "z";
                    break;
                case Keys.Space:
                    newChar += " ";
                    break;
            }
            newChar = newChar.ToUpper();
            String[] wordArray = text.Split(' ');

            if (font.MeasureString(wordArray[wordArray.Length - 1]).Length() <= detection.Width || newChar.Equals(" "))
                text += newChar;
        }

        private bool CheckKey(Keys theKey)
        {
            return lastKeyboardState.IsKeyDown(theKey) && currentKeyboardState.IsKeyUp(theKey);
        }


        #endregion
    }
}
