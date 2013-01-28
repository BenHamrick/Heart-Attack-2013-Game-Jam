using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using HeartbeatGlobalGameJam2013.Screens;

namespace HeartbeatGlobalGameJam2013.Objects
{
    public class Cholesterol : RenderableObject
    {
        Stopwatch timer;
        public Cholesterol(int X, int Y, int Speed)
        {
            scale = 1 + (float)(GamePlay.random.NextDouble() - .5) / 4;
            RotatedCollisionBox.CollisionRectangle.Width = (int)(35 * (double)scale);
            RotatedCollisionBox.CollisionRectangle.Height = (int)(35 * (double)scale);
            rotation = (float)(GamePlay.random.NextDouble() * Math.PI);
            reGen = true;
            this.position.X = X;
            this.position.Y = Y;
            this.speed = Speed;
            this.direction = new Vector2(-1, 0);
            timer = new System.Diagnostics.Stopwatch();
            textureMap = Image.cholesterol;
            spriteFrame = new Rectangle(0, 0, 64, 64);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            

            if (timer.ElapsedMilliseconds > 100)
            {
                this.rotation += (float)(Math.PI / 180) * 6;
                timer.Restart();
            }
            position += direction * (Heart.Pulse * this.speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
    }
}
