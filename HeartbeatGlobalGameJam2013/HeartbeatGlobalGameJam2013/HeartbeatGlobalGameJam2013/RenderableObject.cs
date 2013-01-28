using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using HeartbeatGlobalGameJam2013.Objects;

namespace HeartbeatGlobalGameJam2013
{
    public class RenderableObject : IRenderableObject
    {
        public bool dead;
        public RotatedRectangle RotatedCollisionBox;
        public bool reGen = false;
        public bool isGood;
        public int Health = 100;
        /// <summary>
        /// "New Smaller Hit box" of the object
        /// </summary>
        public Rectangle collisionBox;
        /// <summary>
        /// "Old grid sized Hit box" of the object
        /// </summary>
        public Rectangle rectangle;
        /// <summary>
        /// Frame which is to be selected from the spritesheet
        /// </summary>
        public Rectangle spriteFrame;
        /// <summary>
        /// Top left of the object
        /// </summary>
        public Vector2 position;
        /// <summary>
        /// Center of the object
        /// </summary>
        public Vector2 origin;
        /// <summary>
        /// Velocity of the current object
        /// </summary>
        public Vector2 direction;
        /// <summary>
        /// Speed of the object
        /// </summary>
        public float speed;
        /// <summary>
        /// Stores the color of the object
        /// </summary>
        public Color color = Color.White;
        /// <summary>
        /// Rotation factor of the sprite
        /// </summary>
        public float rotation = 0f;
        /// <summary>
        /// Value with which the object is to be scaled
        /// </summary>
        public float scale = 1.0f;
        /// <summary>
        /// Only objects that are alive will be drawn and collided against
        /// </summary>
        public bool isAlive = true;
        public string nextLevel;

        public Texture2D textureMap { get; set; }

        public int height { get; set; }
        public int width { get; set; }

        public RenderableObject()
        {
            spriteFrame = new Rectangle();
            rectangle = new Rectangle();
            collisionBox = new Rectangle(0,0,2,2);
            RotatedCollisionBox = new RotatedRectangle(collisionBox, 0);
            position = new Vector2();
            origin = new Vector2();
            direction = new Vector2(0, 0);
        }

        public virtual void drawTexture(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (this.isAlive)
            {
                if(textureMap != null)
                spriteBatch.Draw(textureMap, position, spriteFrame, color, rotation, new Vector2(spriteFrame.Width/2,spriteFrame.Height/2), scale, SpriteEffects.None, 0);
#if DEBUG
                //debug code do not erase
                Rectangle aPositionAdjusted = new Rectangle(RotatedCollisionBox.X + (RotatedCollisionBox.Width / 2), RotatedCollisionBox.Y + (RotatedCollisionBox.Height / 2), RotatedCollisionBox.Width, RotatedCollisionBox.Height);
                spriteBatch.Draw(Image.topWall, aPositionAdjusted, new Rectangle(0, 0, 2, 6), Color.White, RotatedCollisionBox.Rotation, new Vector2(2 / 2, 6 / 2), SpriteEffects.None, 0);
                spriteBatch.Draw(Image.topWall, collisionBox, new Rectangle(0, 0, 2, 6), Color.Blue, 0f, Vector2.Zero, SpriteEffects.None, 0);
#endif
            }
        }

        public virtual void LoadContent(ContentManager content)
        {
        }

        public virtual void UnloadContent()
        {
            //throw new NotImplementedException();
        }

        public virtual void Update(GameTime gameTime)
        {
            RotatedCollisionBox.CollisionRectangle.X = (int)(position.X - RotatedCollisionBox.CollisionRectangle.Width / 2);
            RotatedCollisionBox.CollisionRectangle.Y = (int)(position.Y - RotatedCollisionBox.CollisionRectangle.Height/2);

            RotatedCollisionBox.Rotation = rotation;
            if (this.GetType() != typeof(SpaceShip))
            {
                if (isGood)
                {
                    color = new Color(255 * Health / 100, 255, 255 * Health / 100);
                }
                else
                {
                    color = new Color(255 * Health / 100, 255 * Health / 100, 255);
                }
            }

            if (Health <= 0)
            {
                isAlive = false;
                Random rand = new Random(gameTime.TotalGameTime.Milliseconds);
                int randomNumber = rand.Next(1, 13);
                Sound.PlaySound("injury" + randomNumber);
            }
            if (reGen)
            {
                if (Health < 100)
                {
                    Health += (int)(.1f * gameTime.ElapsedGameTime.Milliseconds);
                }
                else
                {
                    Health = 100;
                }
            }
            collisionBox.X = (int)position.X - collisionBox.Width / 2;
            collisionBox.Y = (int)position.Y - collisionBox.Height / 2;
            rectangle.X = (int)Math.Ceiling(position.X);
            rectangle.Y = (int)Math.Ceiling(position.Y);
            
            if (this.GetType() != typeof(SpaceShip) && position.X < Heart.inletLine)
            {
                direction = Heart.GetInletDirection(position);
            }
        }        
    }
}
