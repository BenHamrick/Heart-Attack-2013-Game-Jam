using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace HeartbeatGlobalGameJam2013.ScreenLoader
{
    public class Screen : IScreen
    {

        public virtual void Initialize()
        {
            //throw new NotImplementedException();
        }

        public virtual void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, GraphicsDeviceManager graphics)
        {
            //throw new NotImplementedException();
        }

        public virtual void UnloadContent()
        {
            //throw new NotImplementedException();
        }

        public virtual void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public virtual void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            //throw new NotImplementedException();
        }
    }
}
