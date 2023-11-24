using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class Player
    {
        public string Name { get; }
        public int Health { get; set; }

        public Player(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public void ExecuteCommands(List<string> commands, List<Wall> walls, Player opponent)
        {
            // Implement the logic to execute the commands for the player or AI player
            // You'll need to handle movement, turning, firing, and damage calculations here
            // For simplicity, let's just print the commands for now
            Console.WriteLine($"{Name} executes commands: {string.Join(", ", commands)}");
        }
    }
}
