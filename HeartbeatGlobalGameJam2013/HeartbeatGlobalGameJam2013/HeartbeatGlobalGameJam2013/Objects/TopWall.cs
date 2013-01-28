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

namespace HeartbeatGlobalGameJam2013.Objects
{
    class TopWall : RenderableObject
    {
        public TopWall(int X, int Y)
            : base()
        {
            this.position.X = X;
            this.position.Y = Y;
        }

        public override void LoadContent(ContentManager content)
        {
            RotatedCollisionBox.CollisionRectangle.Width = 1280;
            RotatedCollisionBox.CollisionRectangle.Width = 720;
            //textureMap = Image.topWall;
            //spriteFrame = new Rectangle(0, 0, 1024, 128);
            collisionBox.Width = 1280;
            collisionBox.Height = 100;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void Beat()
        {
            
        }

    }
}
