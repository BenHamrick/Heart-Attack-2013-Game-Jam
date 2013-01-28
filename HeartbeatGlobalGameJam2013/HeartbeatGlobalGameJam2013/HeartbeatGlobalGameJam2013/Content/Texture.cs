using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeartbeatGlobalGameJam2013
{
    public class ObjTexture
    {
        private Texture2D textureBase;
        private int frameWidth, frameHeight;
        private Rectangle[,] sourceArray;
        private Vector2 origin;

        //For single sprites
        public ObjTexture(Texture2D tex)
        {
            textureBase = tex;
            frameHeight = tex.Height;
            frameWidth = tex.Width;
            origin = new Vector2(frameWidth / 2, frameHeight / 2);
        }

        //For sprite sheets and animations
        public ObjTexture(Texture2D tex, int frameW, int frameH)
        {
            textureBase = tex;
            frameHeight = frameH;
            frameWidth = frameW;
            origin = new Vector2(frameWidth / 2, frameHeight / 2);

            buildSourceArray();
        }

        private void buildSourceArray()
        {
            int columns = textureBase.Width / frameWidth;
            int rows = textureBase.Height / frameHeight;

            sourceArray = new Rectangle[columns, rows];

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    sourceArray[x, y] = new Rectangle(x * frameWidth, y * frameHeight, frameWidth, frameHeight);
                }
            }
        }

        #region Getters
        public Rectangle getSourceRectangle(int x, int y)
        {
            return sourceArray[x,y];
        }

        public Texture2D Texture2D
        { get {return textureBase;} }

        public Vector2 Origin
        { get { return origin; } }

        public int FrameWidth
        { get { return frameWidth; } }

        public int FrameHeight
        { get {return frameHeight;} }

        public int FrameCount
        { get{return (textureBase.Width / frameWidth);} }
        #endregion

        // Draws the sprite with its origin in the middle of the sprite
        public void Draw(SpriteBatch s, Vector2 location, Color c, float rot, float scale, int layer)
        {
            s.Draw(textureBase, location, null, c, rot, origin, scale, SpriteEffects.None, layer);
        }

        //Draws the sprite with a specified origin
        public void Draw(SpriteBatch s, Vector2 location, Color c, float rot, float scale, int layer, Vector2 orig)
        {
            s.Draw(textureBase, location, null, c, rot, orig, scale, SpriteEffects.None, layer);
        }

        //Draws the sprite with a specified psoition
        public void Draw(SpriteBatch s, Vector2 location, Color c, float rot, float scale, int layer , int x, int y)
        {
            s.Draw(textureBase, location, sourceArray[x,y], c, rot, origin, scale, SpriteEffects.None, layer);
        }

    }
}
