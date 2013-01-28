using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeartbeatGlobalGameJam2013
{
    public class Animation
    {
        private ObjTexture sourceTexture;
        private bool playOnce;
        private int lineNumber = -1;
        private int animSpeed;
        private float duration;

        private int frameIndex;
        private int counter;

        public Animation(ObjTexture tex)
        {
            sourceTexture = tex;
        }

        //Plays animation once
        public void playAnimation(int line, float dur)
        {
            if (line == lineNumber)
                return;

            lineNumber = line;
            duration = dur;
            animSpeed = (int)(dur / sourceTexture.FrameCount);
            playOnce = true;

            reset();
        }

        //Starts a looped animation
        public void startAnimation(int line, int speed)
        {
            if (line == lineNumber)
                return;

            lineNumber = line;
            animSpeed = speed;
            playOnce = false;

            reset();
        }

        private void reset()
        {
            frameIndex = 0;
            counter = 0;
        }

        public void Draw(SpriteBatch s, Vector2 location, Color c, float rot, float scale, int layer)
        {
            if (sourceTexture == null)
                return;

            if(!playOnce)
            {
                if (counter >= animSpeed)
                {
                    frameIndex++;
                    if (frameIndex == sourceTexture.FrameCount)
                        frameIndex = 0;

                    counter = 0;
                }

                counter++;
                s.Draw(sourceTexture.Texture2D, location, sourceTexture.getSourceRectangle(frameIndex, lineNumber), c, rot, sourceTexture.Origin, scale, SpriteEffects.None, layer);
            }
            else
            {
                if (counter >= animSpeed)
                {
                    frameIndex = Math.Min(frameIndex + 1, sourceTexture.FrameCount - 1);
                    counter = 0;
                }

                counter++;
                s.Draw(sourceTexture.Texture2D, location, sourceTexture.getSourceRectangle(frameIndex, lineNumber), c, rot, sourceTexture.Origin, scale, SpriteEffects.None, layer);
            }
        }
    }
}
