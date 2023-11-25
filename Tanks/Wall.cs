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

        public Wall(int health, float xPos, float yPos)
        {
            Health = health;            
            XPos = xPos; 
            YPos = yPos;
        }
    }
}
