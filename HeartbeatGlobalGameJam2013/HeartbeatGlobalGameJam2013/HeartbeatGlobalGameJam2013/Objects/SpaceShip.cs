using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HeartbeatGlobalGameJam2013.Screens;
using HeartbeatGlobalGameJam2013.Objects.Power_Ups;

namespace HeartbeatGlobalGameJam2013.Objects
{
    public class SpaceShip : RenderableObject
    {
        
        Texture2D rocketThruster;
        Rectangle Thruster;
        Rectangle ThrusterPosition;
        Vector2 TPosition;

        Rectangle Explosion;
        Rectangle ExplosionFrame;

        double TotalElapsed;
        double DeadElapsed;
        double InvinsibleElapsed;
        int InvinsibleCounter;
        int initialX;
        int initialY;
        int frame;
        bool isInvinsible = false;
        int explosion;

        public SpaceShip(int X, int Y)
            : base()
        {
            GamePlay.GameObjects.Add(new Gun(this, true));
            GamePlay.GameObjects.Add(new Gun(this, false));
            initialX = X;
            initialY = Y;
            this.position.X = X;
            this.position.Y = Y;
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            textureMap = Image.ship;
            rocketThruster = Image.rocketThruster;
            spriteFrame = new Rectangle(0, 0, 96, 96);
            Explosion = new Rectangle(0, 0, 128, 128);
            ExplosionFrame = new Rectangle(0, 0, 128, 128);
            Thruster = new Rectangle(0,0,32,32);
            ThrusterPosition = new Rectangle(0,0,32,32);
            collisionBox.Width = 40;
            collisionBox.Height = 40;
            RotatedCollisionBox.CollisionRectangle.Height = 30;
            RotatedCollisionBox.CollisionRectangle.Width = 50;
        }

        public override void UnloadContent()
        {
        }

        Vector2 velocity = new Vector2();
        float rotSpeed = (float)(Math.PI) / .6f;
        float friction = 58f;
        float minVelocity = .001f;
        float maxVelocity = 10f;
        float speed = 30;
        float thrusterOffsetV = -40;

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (!dead)
            {
                if (isInvinsible)
                {
                    InvinsibleElapsed += gameTime.ElapsedGameTime.Milliseconds;
                    if (InvinsibleElapsed > 100)
                    {
                        InvinsibleElapsed = 0;
                        InvinsibleCounter++;
                        if(InvinsibleCounter % 2 == 1)
                        {
                            color = Color.Gray;
                        }
                        else
                        {
                            color = Color.White;
                        }
                        if (InvinsibleCounter > 20)
                        {
                            color = Color.White;
                            InvinsibleCounter = 0;
                            isInvinsible = false;
                        }
                    }
                }
                Collision();

                //rotation = (float)Math.Atan2(Input.mouseYPosition - position.Y, Input.mouseXPosition - position.X);
                //player movement
                if (Input.upPressed)
                {
                    velocity += new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (Input.downPressed)
                {
                    velocity -= new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * (speed / 2) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (Input.leftPressed)
                {
                    rotation -= rotSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (Input.rightPressed)
                {
                    rotation += rotSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (rotation < 0)
                    rotation += 2 * (float)Math.PI;
                if (rotation > 2 * Math.PI)
                    rotation -= 2 * (float)Math.PI;

                //apply friction
                velocity *= friction * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Math.Abs(velocity.X) + Math.Abs(velocity.Y) < minVelocity)
                    velocity = Vector2.Zero;
                else if (velocity.Length() > maxVelocity)
                {
                    velocity.Normalize();
                    velocity *= maxVelocity;
                }

                position += velocity;

                //Debug
                //System.Diagnostics.Debug.WriteLine(rotation);
                UpdateFrame(gameTime);

                TPosition = position + thrusterOffsetV * new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
                ThrusterPosition.X = (int)TPosition.X;
                ThrusterPosition.Y = (int)TPosition.Y;
                Explosion.X = (int)position.X;
                Explosion.Y = (int)position.Y;
                base.Update(gameTime);
            }
            else
            {
                position.X = 8000;
                position.Y = 8000;
                DeadElapsed += gameTime.ElapsedGameTime.TotalSeconds;
                if (explosion == 0)
                {
                    frame = -1;
                    explosion++;
                    Sound.PlaySound("explosion");
                }
                if (DeadElapsed > 2)
                {
                    DeadElapsed = 0;
                    ExplosionFrame.X = 0;
                    explosion = 0;
                    position.X = initialX;
                    position.Y = initialY;
                    dead = false;
                    isInvinsible = true;
                }
                UpdateFrame(gameTime);
            }
        }

        public void UpdateFrame(GameTime gameTime)
        {
            TotalElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if (!dead)
            {
                if (TotalElapsed > .1)
                {
                    frame++;
                    // Keep the Frame between 0 and the total frames, minus one.
                    frame = frame % 8;
                    Thruster.X = frame * 32;
                    TotalElapsed = 0;
                }
            }
            else
            {
                if (TotalElapsed > .1)
                {
                    frame++;
                    ExplosionFrame.X = frame * 128;
                    TotalElapsed = 0;
                }

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!dead && Input.upPressed)
            spriteBatch.Draw(rocketThruster, ThrusterPosition, Thruster, Color.White, rotation, new Vector2(Thruster.Width / 2, Thruster.Height / 2), SpriteEffects.None, 1f);
            if(dead && frame < 8)
                spriteBatch.Draw(Image.Explosion1, Explosion, ExplosionFrame, Color.White, rotation, new Vector2(ExplosionFrame.Width / 2, ExplosionFrame.Height / 2), SpriteEffects.None, 1f);
        }

        private void Collision()
        {
            collisionBox.X = (int)position.X - collisionBox.Width / 2;
            collisionBox.Y = (int)position.Y - collisionBox.Height / 2;

            foreach (var item in GamePlay.GameObjects)
            {
                if (item.GetType() == typeof(TopWall))
                {
                    if (item.collisionBox.Intersects(this.collisionBox))
                    {
                        position.Y = item.collisionBox.Height + item.collisionBox.Y + collisionBox.Height / 2;
                    }
                }
                if (item.GetType() == typeof(BottomWall))
                {
                    if (item.collisionBox.Intersects(this.collisionBox))
                    {
                        position.Y = item.collisionBox.Y - collisionBox.Height / 2;
                    }
                }
                if (item.GetType() == typeof(LeftWall))
                {
                    if (item.collisionBox.Intersects(this.collisionBox))
                    {
                        position.X = item.collisionBox.X + item.collisionBox.Width + collisionBox.Width / 2;
                    }
                }
                if (item.GetType() == typeof(RightWall))
                {
                    if (item.collisionBox.Intersects(this.collisionBox))
                    {
                        position.X = item.collisionBox.X - collisionBox.Width / 2;
                    }
                }
                if (!isInvinsible)
                {
                    if (item.GetType() == typeof(Cholesterol))
                        if (item.RotatedCollisionBox.Intersects(this.RotatedCollisionBox))
                        {
                            dead = true;
                        }
                    if (item.GetType() == typeof(Bacon))
                        if (item.RotatedCollisionBox.Intersects(this.RotatedCollisionBox))
                        {
                            dead = true;
                        }
                    if (item.GetType() == typeof(Pill))
                        if (item.RotatedCollisionBox.Intersects(this.RotatedCollisionBox))
                        {
                            dead = true;
                        }
                    if (item.GetType() == typeof(Bomb))
                        if (item.RotatedCollisionBox.Intersects(this.RotatedCollisionBox))
                        {
                            dead = true;
                            item.dead = true;
                        }
                    if (item.GetType() == typeof(GunSpeedUp))
                        if (item.RotatedCollisionBox.Intersects(this.RotatedCollisionBox))
                        {
                            Gun.bulletSpeedUp = true;
                            item.isAlive = false;
                        }
                    if (item.GetType() == typeof(GunSpeedSlow))
                        if (item.RotatedCollisionBox.Intersects(this.RotatedCollisionBox))
                        {
                            Gun.bulletSpeedDown = true;
                            item.isAlive = false;
                        }
                }
            }
        }
    }
}
