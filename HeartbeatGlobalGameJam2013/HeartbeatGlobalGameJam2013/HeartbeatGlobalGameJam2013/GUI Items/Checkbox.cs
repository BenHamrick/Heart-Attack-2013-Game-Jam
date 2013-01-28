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
    class Checkbox
    {
        Rectangle detection = new Rectangle();
        public bool Checked = false;
        Texture2D checkMark, checkMarkBox;

        public Checkbox(Rectangle rect)
        {
            detection = rect;
            checkMark = Image.checkMark;
            checkMarkBox = Image.checkMarkBox;
        }

        public void UpdateEvents()
        {
            Clicked();
            if (Clicked())
            {
                Checked = !Checked;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(checkMarkBox, detection, Color.White);
            if (Checked)
            {
                spriteBatch.Draw(checkMark, detection, Color.White);
            }
        }

        public Boolean Clicked()
        {
            if (detection.Contains(Input.mousePosition) && Input.lmbClicked)
            {
                return true;
            }
            else
                return false;
        }
    }
}
