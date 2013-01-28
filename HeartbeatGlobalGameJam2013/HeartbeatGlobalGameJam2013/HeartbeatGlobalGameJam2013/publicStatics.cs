using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HeartbeatGlobalGameJam2013
{
    public static class publicStatics
    {
        public static Rectangle screenSize = new Rectangle(0, 0, 1280, 720);
        public static bool exitGame = false;
        public static bool fullScreen = false;
        public static bool soundsEnabled = true;

        public static Matrix spriteScale;

        public static int curScore = 0;
        public static float SaturationThreshold = 85.0f;

    }
}
