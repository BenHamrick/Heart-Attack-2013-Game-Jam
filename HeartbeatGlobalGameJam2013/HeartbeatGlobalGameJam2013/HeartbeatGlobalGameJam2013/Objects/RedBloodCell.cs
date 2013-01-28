using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using HeartbeatGlobalGameJam2013.Screens;

namespace HeartbeatGlobalGameJam2013.Objects
{
    public class RedBloodCell : RenderableObject
    {
        private int Spin;
        System.Diagnostics.Stopwatch timer;
        public RedBloodCell(int X, int Y, int Speed)
            : base()
        {
            isGood = true;
            scale = 1 + (float)(GamePlay.random.NextDouble() - .5) / 4;
            RotatedCollisionBox.CollisionRectangle.Width = (int)(50 * (double)scale);
            RotatedCollisionBox.CollisionRectangle.Height = (int)(20 * (double)scale);
            this.position.X = X;
            this.position.Y = Y;
            this.speed = Speed;
            this.direction = new Vector2(-1, 0);
            timer = new System.Diagnostics.Stopwatch();
            textureMap = Image.redBloodCell;
            spriteFrame = new Rectangle(0, 0, 64, 32);
            timer.Start();

            Random rand = new Random(29);
            Spin = rand.Next(-10, 11);
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (timer.ElapsedMilliseconds > 100)
            {
                this.rotation += (float)(Math.PI / 180) * Spin;
                timer.Restart();
            }
            if (Health <= 0)
            {
                GamePlay.rbcDestroyed++;
            }
            position += direction * (Heart.Pulse * this.speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
    }
}
