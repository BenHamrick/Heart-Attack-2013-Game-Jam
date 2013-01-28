using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace HeartbeatGlobalGameJam2013.Objects
{
    public class Pill : RenderableObject
    {
        System.Diagnostics.Stopwatch timer;
        public Pill(int X, int Y, int Speed)
            : base()
        {
            RotatedCollisionBox.CollisionRectangle.Width = 50;
            RotatedCollisionBox.CollisionRectangle.Height = 20;
            this.position.X = X;
            this.position.Y = Y;
            this.speed = Speed;
            this.direction = new Vector2(-1, 0);
            direction.Normalize();
            timer = new System.Diagnostics.Stopwatch();
            textureMap = Image.pill;
            spriteFrame = new Rectangle(0, 0, 64, 64);
            timer.Start();
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (timer.ElapsedMilliseconds > 100)
            {
                this.rotation += (float)(Math.PI / 180) * 3;
                timer.Restart();
            }
            position += direction * (Heart.Pulse * this.speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
    }
}
