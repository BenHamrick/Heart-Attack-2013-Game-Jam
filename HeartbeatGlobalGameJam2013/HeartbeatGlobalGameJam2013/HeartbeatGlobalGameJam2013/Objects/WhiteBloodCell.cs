using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using HeartbeatGlobalGameJam2013.Screens;

namespace HeartbeatGlobalGameJam2013.Objects
{
    public class WhiteBloodCell : RenderableObject
    {
        Stopwatch timer;
        public WhiteBloodCell(int X, int Y, int Speed)
        {
            scale = 1 + (float)(GamePlay.random.NextDouble() - .5) / 3;
            RotatedCollisionBox.CollisionRectangle.Width = (int)(40 * (double)scale);
            RotatedCollisionBox.CollisionRectangle.Height = (int)(40 * (double)scale);
            collisionBox.Width = 40;
            collisionBox.Height = 40;
            isGood = true;
            reGen = true;
            this.position.X = X;
            this.position.Y = Y;
            this.speed = Speed;
            this.direction = new Vector2(-1, 0);
            timer = new System.Diagnostics.Stopwatch();
            textureMap = Image.whiteBloodCell;
            spriteFrame = new Rectangle(0, 0, 64, 64);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (timer.ElapsedMilliseconds > 100)
            {
                this.rotation += (float)(Math.PI / 180) * 10;
                timer.Restart();
            }
            position += direction * (Heart.Pulse * this.speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
    }
}
