using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeartbeatGlobalGameJam2013.Screens;
using HeartbeatGlobalGameJam2013.Objects.Power_Ups;

namespace HeartbeatGlobalGameJam2013.Objects
{
    class Bullet : RenderableObject
    {
        Vector2 velocity;
        public Bullet(float X, float Y, float rot, float speed)
            : base()
        {
            
            if(GamePlay.random.NextDouble() > .5)
                Sound.PlaySound("Laser1");
            else
                Sound.PlaySound("Laser2");
            this.position.X = X;
            this.position.Y = Y;
            this.rotation = rot;
            RotatedCollisionBox.CollisionRectangle.Width = 10;
            RotatedCollisionBox.CollisionRectangle.Height = 10;
            collisionBox.Width = 10;
            collisionBox.Height = 10;

            velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            velocity *= speed;

            textureMap = Image.bullet;
            spriteFrame = new Rectangle(0, 0, 16, 16);
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            //textureMap = Image.bullet;
            //spriteFrame = new Rectangle(0, 0, 16, 16);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 prevPosition = position; 
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            collisionBox.X = (int)position.X - collisionBox.Width / 2;
            collisionBox.Y = (int)position.Y - collisionBox.Height / 2;
            foreach (var item in GamePlay.GameObjects)
            {
                if (item.GetType() == typeof(TopWall) || item.GetType() == typeof(BottomWall) || item.GetType() == typeof(RightWall) || item.GetType() == typeof(LeftWall))
                    if (item.collisionBox.Intersects(this.collisionBox))
                    {
                        position = prevPosition;
                        this.isAlive = false;
                    }
                if (item.GetType() == typeof(Cholesterol))
                    if (item.RotatedCollisionBox.Intersects(this.collisionBox))
                    {
                        this.isAlive = false;
                        item.Health -=30;
                    }
                if (item.GetType() == typeof(Bacon))
                    if (item.RotatedCollisionBox.Intersects(this.collisionBox))
                    {
                        this.isAlive = false;
                        item.Health -= 10;
                    }
                if (item.GetType() == typeof(Pill))
                    if (item.RotatedCollisionBox.Intersects(this.collisionBox))
                    {
                        this.isAlive = false;
                        item.Health -= 30;
                    }
                if (item.GetType() == typeof(RedBloodCell))
                    if (item.RotatedCollisionBox.Intersects(this.collisionBox))
                    {
                        this.isAlive = false;
                        item.Health -= 20;
                        
                    }
                if (item.GetType() == typeof(WhiteBloodCell))
                    if (item.RotatedCollisionBox.Intersects(this.collisionBox))
                    {
                        this.isAlive = false;
                        item.Health -= 18;
                    }
                if (item.GetType() == typeof(Bomb))
                    if (item.RotatedCollisionBox.Intersects(this.collisionBox))
                    {
                        this.isAlive = false;
                        item.dead = true;
                    }
            }
        }
    }
}
