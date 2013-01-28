using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;
using HeartbeatGlobalGameJam2013;
using HeartbeatGlobalGameJam2013.Screens;
using HeartbeatGlobalGameJam2013.Objects.Power_Ups;

namespace HeartbeatGlobalGameJam2013.Objects
{
    public class Heart : RenderableObject
    {
        int prevPulseTime;
        private readonly int DEFAULT_PULSE_TIME = 300;
        private readonly int SATURATION_TIMEOUT = 3000;
        public static int _pulseTime;
        System.Diagnostics.Stopwatch heartTimer;
        Stopwatch saturationTimer;
        bool beat = true;
        int soundCounter1;
        int soundCounter2;
        private static Vector2 inletPosition = new Vector2(150, 200);
        public static readonly int inletLine = 450;
        public static int Pulse { get; set; }

        public Heart(int X, int Y)
            : base()
        {
            this.position.X = X;
            this.position.Y = Y;
            heartTimer = new Stopwatch();
            saturationTimer = new Stopwatch();
            this.Health = 100;
        }

        public override void LoadContent(ContentManager content)
        {
            textureMap = Image.heart;
            spriteFrame = new Rectangle(0, 0, 500, 500);
            collisionBox = new Rectangle(0, 0, 350, 500);
            RotatedCollisionBox.CollisionRectangle.Width = 350;
            RotatedCollisionBox.CollisionRectangle.Height = 500;
            heartTimer.Start();
        }


        public override void Update(GameTime gameTime)
        {
            Music();
            if (heartTimer.ElapsedMilliseconds > _pulseTime)
            {
                if (soundCounter1 == 0)
                {
                    soundCounter2 = 0;
                    Sound.PlaySound("heartbeatpt1");
                    soundCounter1++;
                }
                beat = true;
                if (heartTimer.ElapsedMilliseconds > (2.15 * _pulseTime))
                {
                    heartTimer.Restart();
                }
            }
            else
            {
                if (soundCounter2 == 0)
                {
                    soundCounter1 = 0;
                    Sound.PlaySound("heartbeatpt2");
                    soundCounter2++;
                }
                beat = false;
            }

            foreach (var item in GamePlay.GameObjects)
            {
                if (item.RotatedCollisionBox.Intersects(this.RotatedCollisionBox))
                {
                    if (item.GetType() == typeof(Cholesterol))
                    {
                        this.Health -= 3;
                        item.isAlive = false;
                    }
                    if (item.GetType() == typeof(Bomb))
                    {
                        item.isAlive = false;
                    }
                    if (item.GetType() == typeof(GunSpeedUp))
                    {
                        item.isAlive = false;
                    }
                    if (item.GetType() == typeof(GunSpeedSlow))
                    {
                        item.isAlive = false;
                    }
                    else if (item.GetType() == typeof(Bacon))
                    {
                        this.Health -= 5;
                        item.isAlive = false;
                    }
                    else if (item.GetType() == typeof(Pill))
                    {
                        this.Health -= 2;
                        item.isAlive = false;
                    }
                    else if (item.GetType() == typeof(RedBloodCell))
                    {
                        this.Health += 2;
                        if (GamePlay.saturation <= publicStatics.SaturationThreshold)
                        {
                            GamePlay.rbcDestroyed--;
                        }
                        item.isAlive = false;
                    }
                    else if (item.GetType() == typeof(WhiteBloodCell))
                    {
                        this.Health += 3;
                        item.isAlive = false;
                    }
                    if (Health > 100)
                    {
                        Health = 100;
                    }
                }
            }

            //if (this.Health > 10)
            //{
            //    _pulseTime = this.Health * 3;
            //}
            //else
            //{
            //    //death stuff here
            //}

            if (GamePlay.saturation <= publicStatics.SaturationThreshold)
            {
                if (saturationTimer.IsRunning != true)
                {
                    saturationTimer.Start();
                }
                else
                {
                    if (saturationTimer.ElapsedMilliseconds > SATURATION_TIMEOUT)
                    {
                        Health -= 2;
                        saturationTimer.Restart();
                    }
                }

                if (GamePlay.saturation <= 0)
                {
                    _pulseTime = 50;
                }
                else
                {
                    _pulseTime = (int)GamePlay.saturation * 3;
                }
            }
            else
            {
                if (saturationTimer.IsRunning == true)
                {
                    Health += 3;
                }
                saturationTimer.Stop();
                saturationTimer.Reset();
                _pulseTime = DEFAULT_PULSE_TIME;
            }
            if (Health <= 0)
                ScreenLoader.ScreenLoader.LoadNextScreen("HighScore");
            Beat();
            base.Update(gameTime);
        }

        private void Music()
        {
                
                if (_pulseTime >= 180)
                {
                        GamePlay.currentSong = "01_Touhou";
                }
                else 
                {
                        GamePlay.currentSong = "03_Touhou";
                }
        }

        public void Beat()
        {
            if (!beat)
            {
                this.scale = 1.5f;
                Heart.Pulse = 25 - (_pulseTime / 50);
            }
            else
            {
                Heart.Pulse = 1;
                this.scale = 1.45f;
            }
        }

        public static Vector2 GetInletDirection(Vector2 Position)
        {
            Vector2 inletDirection = new Vector2();
            inletDirection = inletPosition - Position;
            inletDirection.Normalize();
            return inletDirection;
        }
    }
}
