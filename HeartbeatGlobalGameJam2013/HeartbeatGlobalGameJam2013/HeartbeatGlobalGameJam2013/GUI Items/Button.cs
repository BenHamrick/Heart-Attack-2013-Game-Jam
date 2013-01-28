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
    class Button
    {
        public string text = "";
        public SpriteFont font = Font.cooper48;
        public Color color = Color.Black;
        public Rectangle detection = new Rectangle();
        public ObjTexture image = null;
        Tooltip toolTip;

        public Button(string str, Vector2 SizeandLocation, SpriteFont Font, Color Color)
        {
            text = str;
            font = Font;
            detection = new Rectangle((int)SizeandLocation.X - ((int)font.MeasureString(text).X / 2), (int)SizeandLocation.Y - ((int)font.MeasureString(text).Y / 2), (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y);
            color = Color;
        }

        public Button(string str, Rectangle SizeandLocation, SpriteFont Font, Color Color, Tooltip ttip)
        {
            text = str;
            font = Font;
            detection = SizeandLocation;
            color = Color;
            toolTip = ttip;
        }

        public Button(string str, Rectangle SizeandLocation, SpriteFont Font, Color Color, ObjTexture Image)
        {
            text = str;
            font = Font;
            detection = SizeandLocation;
            color = Color;
            image = Image;
        }

        public Button(string str, Rectangle SizeandLocation, SpriteFont Font, Color Color, ObjTexture Image, Tooltip ttip)
        {
            text = str;
            font = Font;
            detection = SizeandLocation;
            color = Color;
            image = Image;
            toolTip = ttip;
        }

        public void UpdateEvents()
        {
            Clicked();
            mouseEnter();
            mouseLeave();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (mouseEnter())
            {
                if (toolTip != null)
                {
                    toolTip.visible = true;
                    toolTip.location = new Vector2(Input.mousePosition.X, Input.mousePosition.Y);
                }
            }
            if (mouseLeave())
                if (toolTip != null)
                {
                    toolTip.visible = false;
                }
            if (image != null)
                spriteBatch.Draw(image.Texture2D, detection, Color.White);
            Label btnLabel = new Label(text, new Vector2(detection.X + ((int)font.MeasureString(text).X / 2), detection.Y + ((int)font.MeasureString(text).Y / 2)), font, color);
            btnLabel.Draw(spriteBatch);
            if(toolTip != null)
                toolTip.Draw(spriteBatch);
        }

        #region Events

        public Boolean Clicked()
        {
            if (detection.Contains(Input.mousePosition) && Input.lmbClicked)
                return true;
            else
                return false;
        }
        public Boolean mouseEnter()
        {
            if (detection.Contains(Input.mousePosition) && (detection.Contains(Input.oldMousePosition) == false))
            {
                return true;
            }
            else
                return false;
        }
        public Boolean mouseLeave()
        {
            if ((detection.Contains(Input.mousePosition) == false) && (detection.Contains(Input.oldMousePosition)))
            {
                return true;
            }
                
            else
                return false;
        }
        public Boolean mouseHover()
        {
            if (detection.Contains(Input.mousePosition))
            {
                return true;
            }

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
                if (font.MeasureString(line + word).Length() > detection.Width)
                {
                    returnString = returnString + line + '\n';
                    line = String.Empty;
                }

                line = line + word + ' ';
            }

            return returnString + line;
        }

        #endregion
    }
}
