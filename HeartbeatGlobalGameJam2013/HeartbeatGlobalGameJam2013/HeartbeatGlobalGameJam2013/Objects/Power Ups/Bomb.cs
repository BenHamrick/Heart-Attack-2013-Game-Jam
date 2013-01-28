using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeartbeatGlobalGameJam2013.Screens;
using Microsoft.Xna.Framework.Graphics;

namespace HeartbeatGlobalGameJam2013.Objects.Power_Ups
{
    public class Bomb : RenderableObject
    {
        bool isNuke;
        int frame;
        Rectangle Explosion;
        Rectangle ExplosionFrame;
        double TotalElapsed;

        public Bomb(int X, int Y, int Speed, bool isNuke)
            : base()
        {
            Explosion = new Rectangle(0, 0, 64, 64);
            ExplosionFrame = new Rectangle(0, 0, 64, 64);
            this.isNuke = isNuke;
            RotatedCollisionBox.CollisionRectangle.Width = 50;
            RotatedCollisionBox.CollisionRectangle.Height = 20;
            this.position.X = X;
            this.position.Y = Y;
            this.speed = Speed;
            this.direction = new Vector2(-1, 0);
            direction.Normalize();
            if (isNuke)
                textureMap = Image.nuke;
            else
                textureMap = Image.bomb;

            spriteFrame = new Rectangle(0, 0, 64, 64);
            isGood = true;
        }

        public override void Update(GameTime gameTime)
        {
            position += direction * (Heart.Pulse * this.speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (dead)
            {
                if (isNuke)
                {
                    for (int i = GamePlay.GameObjects.Count; i-- > 0; )
                    {
                        if (GamePlay.GameObjects[i].GetType() == typeof(Bacon) || GamePlay.GameObjects[i].GetType() == typeof(Cholesterol) || GamePlay.GameObjects[i].GetType() == typeof(Pill) || GamePlay.GameObjects[i].GetType() == typeof(RedBloodCell) || GamePlay.GameObjects[i].GetType() == typeof(WhiteBloodCell))
                        {
                            GamePlay.GameObjects[i].Health = 0;
                        }
                    }
                    
                }
                else
                {
                    for (int i = GamePlay.GameObjects.Count; i-- > 0; )
                    {
                        if (GamePlay.GameObjects[i].GetType() == typeof(Bacon) || GamePlay.GameObjects[i].GetType() == typeof(Cholesterol) || GamePlay.GameObjects[i].GetType() == typeof(Pill))
                        {
                            if (Vector2.Distance(GamePlay.GameObjects[i].position, position) <= 200)
                            {
                                GamePlay.GameObjects[i].Health = 0;
                            }
                        }
                    }
                }
                Explosion.X = (int)position.X;
                Explosion.Y = (int)position.Y;
                UpdateFrame(gameTime);
                if (frame >= 8)
                {
                    this.isAlive = false;
                }
            }
            base.Update(gameTime);
        }

        public void UpdateFrame(GameTime gameTime)
        {
            TotalElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if(dead)
            {
                if (TotalElapsed > .1)
                {
                    frame++;
                    ExplosionFrame.X = frame * 128;
                    TotalElapsed = 0;
                }

            }

        }

        public override void drawTexture(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            if (dead && frame < 8)
                spriteBatch.Draw(Image.Explosion2, Explosion, ExplosionFrame, Color.White, rotation, new Vector2(ExplosionFrame.Width / 2, ExplosionFrame.Height / 2), SpriteEffects.None, 1f);
            else
                base.drawTexture(spriteBatch, graphics);
        }
    }
}
