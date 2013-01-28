using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeartbeatGlobalGameJam2013.Screens;

namespace HeartbeatGlobalGameJam2013.Objects.Power_Ups
{
    public class Bomb : RenderableObject
    {
        bool isNuke;
        public Bomb(int X, int Y, int Speed, bool isNuke)
            : base()
        {
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
                this.isAlive = false;
            }
            base.Update(gameTime);
        }
    }
}
