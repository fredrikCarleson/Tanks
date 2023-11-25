using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class Wall
    {
        public int Health { get; set; }
        public float XPos { get; set; }
        public float YPos { get; set; }

        public int Width = 80;
        public int Height = 71;

        public Rectangle Bounds => new Rectangle((int)XPos, (int)YPos, Width, Height);


        public Wall(int health, float xPos, float yPos)
        {
            Health = health;            
            XPos = xPos; 
            YPos = yPos;
        }
    }
}
