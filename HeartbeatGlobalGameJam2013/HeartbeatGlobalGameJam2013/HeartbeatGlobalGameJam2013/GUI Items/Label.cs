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
    public class Label
    {
        public string text = "";
        public SpriteFont font = Font.cooper48;
        public Vector2 location = new Vector2();
        public Color color = Color.Black;

        public Label(string str, Vector2 loc, SpriteFont Font)
        {
            text = str;
            font = Font;
            location = loc;
        }
        public Label(string str, Vector2 loc, SpriteFont Font, Color Color)
        {
            text = str;
            font = Font;
            location = loc;
            color = Color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, new Vector2(location.X - (font.MeasureString(text).X / 2), location.Y - (font.MeasureString(text).Y / 2)), color);
        }
    }
}
