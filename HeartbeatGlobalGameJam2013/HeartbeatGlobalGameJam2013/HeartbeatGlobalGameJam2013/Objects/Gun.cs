using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeartbeatGlobalGameJam2013.Screens;
using System.Diagnostics;

namespace HeartbeatGlobalGameJam2013.Objects
{
    class Gun : RenderableObject
    {
        double time;
        SpaceShip parent;
        bool rightGun;

        public static bool bulletSpeedUp;
        double gameTimeSpeedUp;

        public static bool bulletSpeedDown;
        double gameTimeSpeedDown;
        public static double bulletDelay = 200;

        public Gun(SpaceShip parent, bool rightGun)
            : base()
        {
            this.parent = parent;
            this.rightGun = rightGun;
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            textureMap = Image.gun;
            spriteFrame = new Rectangle(0, 0, 64, 16);
        }

        float gunOffsetH = 28;
        float gunOffsetV = 6;
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            


            time += gameTime.ElapsedGameTime.TotalMilliseconds;

            rotation = (float)Math.Atan2(Input.mouseYPosition - position.Y, Input.mouseXPosition - position.X);

            if (rotation < 0)
                rotation += 2 * (float)Math.PI;
            if (rotation > 2 * Math.PI)
                rotation -= 2 * (float)Math.PI;

            this.position = parent.position + gunOffsetV * new Vector2((float)Math.Cos(parent.rotation), (float)Math.Sin(parent.rotation));

            if(rightGun)
                this.position += gunOffsetH * new Vector2((float)Math.Cos(parent.rotation + Math.PI / 2), (float)Math.Sin(parent.rotation + Math.PI / 2));
            else
                this.position -= gunOffsetH * new Vector2((float)Math.Cos(parent.rotation + Math.PI / 2), (float)Math.Sin(parent.rotation + Math.PI / 2));

            if (time >= bulletDelay)
            {
                time = 0;
                if (Input.isLMBPressed && ! parent.dead)
                {
                    float rightBound, leftBound;

                    if (rightGun)
                    {
                        leftBound = (parent.rotation) - (float)Math.PI / 6;
                        rightBound = (parent.rotation) + (float)Math.PI;
                    }
                    else
                    {
                        leftBound = (parent.rotation) + (float)Math.PI / 6;
                        rightBound = (parent.rotation) - (float)Math.PI;
                    }
                    if (rightGun)
                    {
                        if (rightBound > Math.PI * 2 && rotation < Math.PI)
                        {
                            rotation += (float)Math.PI * 2;
                        }

                        if (leftBound < 0 && rotation > Math.PI)
                        {
                            leftBound += (float)Math.PI * 2;
                            rightBound += (float)Math.PI * 2;
                        }
                    }
                    else
                    {
                        if (leftBound > Math.PI * 2 && rotation < Math.PI)
                        {
                            rotation += (float)Math.PI * 2;
                        }

                        if (rightBound < 0 && rotation > Math.PI)
                        {
                            leftBound += (float)Math.PI * 2;
                            rightBound += (float)Math.PI * 2;
                        }


                        //System.Diagnostics.Debug.WriteLine("r " + rotation);
                        //System.Diagnostics.Debug.WriteLine("l " + leftBound);
                        //System.Diagnostics.Debug.WriteLine("r " + rightBound);
                    }

                    if (((rightGun) && (leftBound < (rotation) && (rotation) < rightBound))
                        || ((!rightGun) && (rightBound < (rotation) && (rotation) < leftBound)))
                        GamePlay.GameObjects.Add(new Bullet(position.X, position.Y, rotation + (float)(GamePlay.random.NextDouble() - .5) / 20, 500));
                }
            }
            //Debug
        }
    }
}
