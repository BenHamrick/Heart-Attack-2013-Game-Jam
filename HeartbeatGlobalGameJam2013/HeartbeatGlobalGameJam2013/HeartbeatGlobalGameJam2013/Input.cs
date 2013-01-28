using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;

namespace HeartbeatGlobalGameJam2013
{
    public static class Input
    {
        public static MouseState MS, MSold;
        public static KeyboardState KS, KSold;

        public static void Update()
        {
            updateOldState();
            updateCurrentState();
        }

        private static void updateCurrentState()
        {
            MS = Mouse.GetState();
            KS = Keyboard.GetState();
        }
        private static void updateOldState()
        {
            MSold = MS;
            KSold = KS;
        }

        //Mouse Position
        public static Point mousePosition
        { get { return new Point(MS.X, MS.Y); } }

        public static int mouseXPosition
        { get { return MS.X; } }

        public static int mouseYPosition
        { get { return MS.Y; } }

        //Old Mouse Position
        public static Point oldMousePosition
        { get { return new Point(MSold.X, MSold.Y); } }

        public static int oldMouseXPosition
        { get { return MSold.X; } }

        public static int oldMouseYPosition
        { get { return MSold.Y; } }

        //Left Mouse Button
        public static bool isLMBPressed
        { get { return MS.LeftButton == ButtonState.Pressed; } }

        public static bool wasLMBPressed
        { get { return MSold.LeftButton == ButtonState.Pressed; } }

        public static bool lmbClicked
        { get { return isLMBPressed && !wasLMBPressed; } }

        //Right Mouse Button
        public static bool isRMBPressed
        { get { return MS.RightButton == ButtonState.Pressed; } }

        public static bool wasRMBPressed
        { get { return MSold.RightButton == ButtonState.Pressed; } }

        public static bool rmbClicked
        { get { return isRMBPressed && !wasRMBPressed; } }

        //Keys
        public static bool upPressed
        { get { return KS.IsKeyDown(Keys.W); } }

        public static bool downPressed
        { get { return KS.IsKeyDown(Keys.S); } }

        public static bool leftPressed
        { get { return KS.IsKeyDown(Keys.A); } }

        public static bool rightPressed
        { get { return KS.IsKeyDown(Keys.D); } }

        public static bool reloadHit
        { get { return KS.IsKeyDown(Keys.R) && !KSold.IsKeyDown(Keys.R); } }
    }
}
