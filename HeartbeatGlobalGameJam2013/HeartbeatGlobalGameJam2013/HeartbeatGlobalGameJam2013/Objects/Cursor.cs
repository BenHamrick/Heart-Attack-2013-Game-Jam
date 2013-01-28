using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeartbeatGlobalGameJam2013.Objects
{
    class Cursor : RenderableObject
    {
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            textureMap = Image.mouse;
            spriteFrame = new Rectangle(0, 0, 32, 32);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            position.X = Input.mouseXPosition;
            position.Y = Input.mouseYPosition;
        }
    }
}
