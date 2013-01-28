using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeartbeatGlobalGameJam2013.Objects;
using HeartbeatGlobalGameJam2013.ScreenLoader;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using HeartbeatGlobalGameJam2013.Objects.Power_Ups;

namespace HeartbeatGlobalGameJam2013.Screens
{
    public class GamePlay : Screen
    {
        double gameTimeSpeedUp;
        double gameTimeSpeedDown;
        public static string currentSong;
        string prevSong;
        public static List<RenderableObject> GameObjects = new List<RenderableObject>();
        public static Random random = new Random(Environment.TickCount);
        private List<Type> Enemies = new List<Type>();
        private List<Type> Friendlies = new List<Type>();
        private List<Type> PowerUps = new List<Type>();
        private int enemySpawnTime = 2000;
        Stopwatch enemyTimer;
        private int friendlySpawnTime = 4000;
        private int powerUpSpawnTime = 15000;
        Stopwatch friendlyTimer;
        Stopwatch scoreTimer;
        Stopwatch powerUpTimer;
        Label lblScore;
        public static int rbcCreated = 0;
        public static int rbcDestroyed = 0;
        public static float saturation = 100;

        double TotalElapsed;
        public Rectangle spriteFrame;
        public Rectangle position;
        int frame;

        SpaceShip spaceShip;
        public GamePlay()
        {

            enemyTimer = new Stopwatch();
            friendlyTimer = new Stopwatch();
            scoreTimer = new Stopwatch();
            powerUpTimer = new Stopwatch();
            GameObjects.Add(new Background(720, 360));

            
            //TODO: the Y axis position must be randomized
            //GameObjects.Add(new RedBloodCell(publicStatics.screenSize.Width, publicStatics.screenSize.Height / 2, 10));
            //GameObjects.Add(new Pill(publicStatics.screenSize.Width, publicStatics.screenSize.Height / 3, 5));


            //this must be in the end

            GameObjects.Add(new Cursor());
            GameObjects.Add(new TopWall(1280 / 2, 50));
            GameObjects.Add(new BottomWall(1280 / 2, 670));
            GameObjects.Add(new LeftWall(100, 720 / 2));
            GameObjects.Add(new RightWall(1280, 100));
            GameObjects.Add(new Heart(0, (publicStatics.screenSize.Height / 2) + 60));

            spaceShip = new SpaceShip(publicStatics.screenSize.Width / 2, publicStatics.screenSize.Height / 2);

            publicStatics.curScore = 0;

        }

        public override void Initialize()
        {
            spriteFrame = new Rectangle(0,0,128,64);
            position = new Rectangle(150, 50, 128, 64);
            //spawn priority ratio
            Enemies.Add(typeof(Pill));
            Enemies.Add(typeof(Pill));
            
            Enemies.Add(typeof(Bacon));
            Enemies.Add(typeof(Cholesterol));
            Enemies.Add(typeof(Cholesterol));
            Enemies.Add(typeof(Cholesterol));
            Enemies.Add(typeof(Cholesterol));

            Friendlies.Add(typeof(RedBloodCell));
            Friendlies.Add(typeof(RedBloodCell));
            Friendlies.Add(typeof(RedBloodCell));
            Friendlies.Add(typeof(WhiteBloodCell));

            PowerUps.Add(typeof(Bomb));
            PowerUps.Add(typeof(Bomb));
            PowerUps.Add(typeof(GunSpeedUp));
            PowerUps.Add(typeof(GunSpeedSlow));

            foreach (var item in GameObjects)
            {
            }
            GamePlay.rbcCreated = 0;
            GamePlay.rbcDestroyed = 0;
            GamePlay.saturation = 100f;
        }

        public override void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            spaceShip.LoadContent(content);
            foreach (var item in GameObjects)
            {
                item.LoadContent(content);
            }
            enemyTimer.Start();
            friendlyTimer.Start();
            scoreTimer.Start();
            lblScore = new Label("Score: " + publicStatics.curScore, new Vector2(15 + (Font.cooper32.MeasureString("Score: " + publicStatics.curScore).X / 2), publicStatics.screenSize.Y - 15 - (Font.cooper32.MeasureString("Score: " + publicStatics.curScore).X / 2)), Font.cooper32);
            powerUpTimer.Start();
        }

        public override void UnloadContent()
        {
            foreach (var item in GameObjects)
            {
                item.UnloadContent();
            }
            GameObjects.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            Music();
            PowerUp(gameTime);

            spaceShip.Update(gameTime);
            //foreach (var item in GameObjects)
            //{
            //    item.Update(gameTime);
            //}
            for (int x = 0; x < GameObjects.Count; x++)
            {
                GameObjects[x].Update(gameTime);
            }
            if (enemyTimer.ElapsedMilliseconds > enemySpawnTime)
            {
                AddEnemy();
                enemyTimer.Restart();
            }
            if (friendlyTimer.ElapsedMilliseconds > friendlySpawnTime)
            {
                AddFriendly();
                friendlyTimer.Restart();
            }
            if (powerUpTimer.ElapsedMilliseconds > powerUpSpawnTime)
            {
                AddPowerUp();
                powerUpTimer.Restart();
            }
            if (scoreTimer.ElapsedMilliseconds > 20)
            {
                publicStatics.curScore++;
                scoreTimer.Restart();
            }
            lblScore = new Label("Score: " + (int)(publicStatics.curScore * .1), new Vector2(15 + (Font.cooper32.MeasureString("Score: " + publicStatics.curScore).X / 2), publicStatics.screenSize.Height - 15 - (Font.cooper32.MeasureString("Score: " + publicStatics.curScore).Y / 2)), Font.cooper32, Color.White);

            for (int i = GameObjects.Count; i-- > 0; )
            {
                if (!GameObjects[i].isAlive)
                    GameObjects.RemoveAt(i);
            }

            if (Input.KS.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape) && Input.KSold.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                ScreenLoader.ScreenLoader.PushScreen("PauseMenu");
            }
            if (GamePlay.rbcCreated > 0)
            {
                saturation = ((GamePlay.rbcCreated - GamePlay.rbcDestroyed) / (float)GamePlay.rbcCreated) * 100;
                if (saturation < 0f)
                {
                    saturation = 0f;
                }
            }
            UpdateFrame(gameTime);
        }

        private void PowerUp(GameTime gameTime)
        {
            if (Gun.bulletSpeedUp)
            {
                gameTimeSpeedUp += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (gameTimeSpeedUp > 7000)
                {
                    gameTimeSpeedUp = 0;
                    Gun.bulletSpeedUp = false;
                    Gun.bulletDelay = 200;
                }
                else
                {
                    Gun.bulletDelay = 100;
                }
            }

            if (Gun.bulletSpeedDown)
            {
                gameTimeSpeedDown += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (gameTimeSpeedDown > 7000)
                {
                    gameTimeSpeedDown = 0;
                    Gun.bulletSpeedDown = false;
                    Gun.bulletDelay = 200;
                }
                else
                {
                    Gun.bulletDelay = 400;
                }
            }
        }

        private void Music()
        {
            if (prevSong != currentSong)
            {
                int rand = random.Next(0,2);
                prevSong = currentSong;
                if(rand == 0)
                    Sound.PlaySong(currentSong + "_pt1");
                else
                    Sound.PlaySong(currentSong + "_pt2");
            }
        }

        public void UpdateFrame(GameTime gameTime)
        {
            TotalElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            float delay = (Heart._pulseTime/1000f) / 4f;
            if (TotalElapsed > delay)
            {
                frame++;
                // Keep the Frame between 0 and the total frames, minus one.
                frame = frame % 8;
                spriteFrame.X = frame * 128;
                TotalElapsed = 0;
            }
        }

        
        public override void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, publicStatics.spriteScale);

            
            foreach (var item in GameObjects)
            {
                item.drawTexture(spriteBatch, graphicsDevice);
            }
            spaceShip.Draw(spriteBatch);
            spaceShip.drawTexture(spriteBatch, graphicsDevice);
            spriteBatch.Draw(Image.redBloodCell, new Rectangle(100, 110, 48, 24), Color.White);
            if (saturation < publicStatics.SaturationThreshold)
            {
                spriteBatch.DrawString(Font.rbcStats, String.Format ("{0:0.00}", saturation) + "%", new Vector2(145, 100), Color.Red);
            }
            else
            {
                spriteBatch.DrawString(Font.rbcStats, String.Format("{0:0.00}", saturation) + "%", new Vector2(145, 100), Color.Green);
            }
            lblScore.Draw(spriteBatch);
            spriteBatch.Draw(Image.heartMeter, position, spriteFrame, Color.White, 0, new Vector2(spriteFrame.Width / 2, spriteFrame.Height / 2), SpriteEffects.None, 0);
            spriteBatch.End();
        }

        Random rand = new Random();
        private void AddEnemy()
        {
            var type = Enemies[rand.Next(0, Enemies.Count)];
            int yLocation = (publicStatics.screenSize.Height / 30) * rand.Next(4, 26);
            var myObject = Activator.CreateInstance(type, publicStatics.screenSize.Width, yLocation, rand.Next(7, 12));
            RenderableObject rObj = (RenderableObject)myObject;
            GameObjects.Add(rObj);
        }

        private void AddFriendly()
        {
            var type = Friendlies[rand.Next(0, Friendlies.Count)];
            int yLocation = (publicStatics.screenSize.Height / 30) * rand.Next(4, 26);
            var myObject = Activator.CreateInstance(type, publicStatics.screenSize.Width, yLocation, rand.Next(5, 10));
            RenderableObject rObj = (RenderableObject)myObject;
            GameObjects.Add(rObj);
            if (rObj.GetType() == typeof(RedBloodCell))
            {
                rbcCreated++;
            }
            //add more rbc no matter what

            GameObjects.Add(new RedBloodCell(publicStatics.screenSize.Width, (publicStatics.screenSize.Height / 30) * rand.Next(4, 26), rand.Next(1, 10)));
            rbcCreated++;

        }
        private bool isNuke = false;
        private void AddPowerUp()
        {
            var type = PowerUps[rand.Next(0, PowerUps.Count)];
            int yLocation = (publicStatics.screenSize.Height / 30) * rand.Next(4, 26);
            if (type == typeof(Bomb))
            {
                if (isNuke)
                    isNuke = false;
                else
                    isNuke = true;
                var myObject = Activator.CreateInstance(type, publicStatics.screenSize.Width, yLocation, rand.Next(1, 10), isNuke);
                RenderableObject rObj = (RenderableObject)myObject;
                GameObjects.Add(rObj);
            }
            else
            {
                var myObject = Activator.CreateInstance(type, publicStatics.screenSize.Width, yLocation, rand.Next(1, 10));
                RenderableObject rObj = (RenderableObject)myObject;
                GameObjects.Add(rObj);
            }
            

        }
    }
}
