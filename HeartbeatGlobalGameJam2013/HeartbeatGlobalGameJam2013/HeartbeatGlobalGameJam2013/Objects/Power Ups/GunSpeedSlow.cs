using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeartbeatGlobalGameJam2013.Screens;

namespace HeartbeatGlobalGameJam2013.Objects.Power_Ups
{
    public class GunSpeedSlow : RenderableObject
    {
        public GunSpeedSlow(int X, int Y, int Speed)
            : base()
        {
            RotatedCollisionBox.CollisionRectangle.Width = 50;
            RotatedCollisionBox.CollisionRectangle.Height = 50;
            this.position.X = X;
            this.position.Y = Y;
            this.speed = Speed;
            this.direction = new Vector2(-1, 0);
            direction.Normalize();
            textureMap = Image.SpeedDown;
            spriteFrame = new Rectangle(0, 0, 64, 64);
            isGood = true;
        }

        public override void Update(GameTime gameTime)
        {
            position += direction * (Heart.Pulse * this.speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
    }
}
