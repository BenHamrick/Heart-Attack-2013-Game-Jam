using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace HeartbeatGlobalGameJam2013
{
    class Font
    {
        public static SpriteFont rbcStats, cooper32, cooper48, heartHealth;

        public static void LoadFonts(ContentManager content)
        {
            rbcStats = content.Load<SpriteFont>("Font/rbcStatFont");
            heartHealth = content.Load<SpriteFont>("Font/heartHealth");

            cooper32 = content.Load<SpriteFont>("Font/Cooper32");
            cooper48 = content.Load<SpriteFont>("Font/Cooper48");
        }
    }
}
