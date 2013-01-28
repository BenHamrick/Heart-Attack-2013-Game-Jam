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
    class Tooltip
    {
        public string text = "";
        public SpriteFont font = Font.cooper48;
        public Vector2 location = new Vector2();
        public Color color = Color.Black;
        public bool visible = false;

        public Tooltip(string str, SpriteFont Font)
        {
            text = str;
            font = Font;
            location = new Vector2(Input.mousePosition.X, Input.mousePosition.Y);
        }
        public Tooltip(string str, SpriteFont Font, Color Color)
        {
            text = str;
            font = Font;
            location = new Vector2(Input.mousePosition.X, Input.mousePosition.Y);
            color = Color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            spriteBatch.DrawString(font, parseText(text), location, color);
        }

        public void determineLocation()
        {
            //if()//not enough space to right draw to left
            //{
            
            //}
            //else if()//not enough space to bottom, draw to top
            //else
            //    location = new Vector2(Input.mousePosition.X, Input.mousePosition.Y);
        }

        #region Text Positioning

        private String parseText(String text)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
                if (font.MeasureString(line + word).Length() > 200)
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
        
        private String longestLine(String text)
        {
            String returnString = "";
            for (int x = 0; x < text.Length; x++)
            {
                if (text.Substring(x, 1).Equals("\n"))
                {
                    if (font.MeasureString(returnString).Length() <= font.MeasureString(text.Substring(x + 1)).Length())
                        returnString = text.Substring(x + 1);
                }
            }
            return returnString;
        }

        #endregion
    }
}
