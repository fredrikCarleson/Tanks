using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tanks
{
    internal class AITank
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        public AITank(Texture2D texture, Vector2 position, int width, int height)
        {
            Texture = texture;
            Position = position;
            Width = width;
            Height = height;
        }
    }
}

