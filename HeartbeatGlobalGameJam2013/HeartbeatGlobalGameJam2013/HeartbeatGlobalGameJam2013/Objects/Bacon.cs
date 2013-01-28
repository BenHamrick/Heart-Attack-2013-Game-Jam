using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using HeartbeatGlobalGameJam2013.Screens;

namespace HeartbeatGlobalGameJam2013.Objects
{
    public class Bacon : RenderableObject
    {
        System.Diagnostics.Stopwatch timer;
        public Bacon(int X, int Y, int Speed)
            : base()
        {
            scale = 1 + (float)(GamePlay.random.NextDouble() - .5) / 4;
            RotatedCollisionBox.CollisionRectangle.Width = (int)(20 * (double)scale);
            RotatedCollisionBox.CollisionRectangle.Height = (int)(110 * (double)scale);
            rotation = (float)(GamePlay.random.NextDouble() * Math.PI);
            this.position.X = X;
            this.position.Y = Y;
            this.speed = Speed;
            this.direction = new Vector2(-1, 0);
            //timer = new System.Diagnostics.Stopwatch();
            textureMap = Image.bacon;
            spriteFrame = new Rectangle(0, 0, 128, 128);
            //timer.Start();
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            //Bacon doesnt rotate
            //it just bloody doesn't
            //if (timer.ElapsedMilliseconds > 100)
            //{
            //    this.rotation += (float)(Math.PI / 180) * 3;
            //    timer.Restart();
            //}

            position += direction * (Heart.Pulse * this.speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
    }
}
