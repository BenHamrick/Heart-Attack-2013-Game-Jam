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

namespace HeartbeatGlobalGameJam2013.Objects
{
    public class HeartToBackground : RenderableObject
    {



        public HeartToBackground(int X, int Y)
            : base()
        {
            position.X = X;
            position.Y = Y;
        }

        public override void LoadContent(ContentManager content)
        {
            textureMap = Image.heartToBackground;
            spriteFrame = new Rectangle(0, 0, 1024, 1024);
            scale = 0.4f;
        }


        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

    }
}
