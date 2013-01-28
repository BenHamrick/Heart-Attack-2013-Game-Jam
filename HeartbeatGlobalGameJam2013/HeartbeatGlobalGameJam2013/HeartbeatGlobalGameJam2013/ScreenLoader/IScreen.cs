using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HeartbeatGlobalGameJam2013.ScreenLoader
{
    public interface IScreen
    {
        void Initialize();

        void LoadContent(ContentManager content, GraphicsDeviceManager graphics);

        void UnloadContent();

        void Update(GameTime gameTime);

        void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
    }
}
