using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HeartbeatGlobalGameJam2013
{
    public interface IRenderableObject
    {
        Texture2D textureMap { get; set; }

        int height { get; set; }
        int width { get; set; }

        void drawTexture(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);

        void LoadContent(ContentManager content);

        void UnloadContent();

        void Update(GameTime gameTime);
    }
}
