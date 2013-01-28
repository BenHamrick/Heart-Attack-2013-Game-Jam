using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace HeartbeatGlobalGameJam2013
{
    class Image
    {
        public static ObjTexture textCursor, textButton;
        public static Texture2D ship, gun, bullet;
        public static Texture2D heart;
        public static Texture2D mouse;
        public static Texture2D redBloodCell;
        public static Texture2D pill;
        public static Texture2D topWall;
        public static Texture2D bacon;
        public static Texture2D cholesterol;
        public static Texture2D whiteBloodCell;
        public static Texture2D background;
        public static Texture2D checkMark, checkMarkBox, titleScreen;
        public static Texture2D rocketThruster;
        public static Texture2D pauseMenu;
        public static Texture2D howToPlayMenu;
        public static Texture2D heartMeter;
        public static Texture2D highScore;
        public static Texture2D splashScreen;
        public static Texture2D splashStart;
        public static Texture2D bomb;
        public static Texture2D nuke;
        public static Texture2D SpeedUp;
        public static Texture2D SpeedDown;
        public static Texture2D Explosion1;
        public static Texture2D Explosion2;

        public static void LoadImages(ContentManager content)
        {
            rocketThruster = content.Load<Texture2D>("Images/Rocket/rocket_thruster");
            topWall = content.Load<Texture2D>("Images/vessel_top");
            ship = content.Load<Texture2D>("Images/Rocket/player");
            gun = content.Load<Texture2D>("Images/Rocket/gun");
            bullet = content.Load<Texture2D>("Images/bullet");
            heart = content.Load<Texture2D>("Images/heart");
            mouse = content.Load<Texture2D>("Images/cursor");
            redBloodCell = content.Load<Texture2D>("Images/red_blood_cell");
            pill = content.Load<Texture2D>("Images/pill");
            bacon = content.Load<Texture2D>("Images/bacon");
            cholesterol = content.Load<Texture2D>("Images/cholesterol");
            background = content.Load<Texture2D>("Images/background_complete");
            whiteBloodCell = content.Load<Texture2D>("Images/white_blood_cell");
            checkMark = content.Load<Texture2D>("Images/check");
            checkMarkBox = content.Load<Texture2D>("Images/check_box");
            titleScreen = content.Load<Texture2D>("Images/title");
            whiteBloodCell = content.Load<Texture2D>("Images/white_blood_cell");
            pauseMenu = content.Load<Texture2D>("Images/pauseMenuBackground");
            howToPlayMenu = content.Load<Texture2D>("Images/how_to_play");
            heartMeter = content.Load<Texture2D>("Images/heart_monitor");
            bomb = content.Load<Texture2D>("Images/bomb");
            nuke = content.Load<Texture2D>("Images/nuke");
            highScore = content.Load<Texture2D>("Images/high_scores");
            splashScreen = content.Load<Texture2D>("Images/splashScreen");
            splashStart = content.Load<Texture2D>("Images/splashStart");
            SpeedUp = content.Load<Texture2D>("Images/pup_fast_gun");
            SpeedDown = content.Load<Texture2D>("Images/pdown_slow_gun");

            textButton = new ObjTexture(content.Load<Texture2D>("Images/ButtonOutline"), 32, 32);
            textCursor = new ObjTexture(content.Load<Texture2D>("Images/textCursor"), 8, 16);
            Explosion1 = content.Load<Texture2D>("Images/explosion_01");
            Explosion2 = content.Load<Texture2D>("Images/explosion_02");
        }
    }
}
