using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Tanks
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Player aiPlayer;
        private List<Wall> walls;

        Texture2D ballTexture;
        Texture2D brickWallTexture;
        Texture2D greenTankTexture;
        Texture2D greyTankTexture;
        Texture2D yellowTankTexture;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Initialize player, AI player, and walls
            player = new Player("Player", 100);
            aiPlayer = new Player("AI Player", 100);
            walls = GenerateRandomWalls();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("ball");
            brickWallTexture = Content.Load<Texture2D>("BrickWall");
            greenTankTexture = Content.Load<Texture2D>("GreenTank");
            greyTankTexture = Content.Load<Texture2D>("GreyTank");
            yellowTankTexture = Content.Load<Texture2D>("YellowTank");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(ballTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(brickWallTexture, new Vector2(50, 0), Color.White);
            _spriteBatch.Draw(greenTankTexture, new Vector2(100, 0), Color.White);
            _spriteBatch.Draw(greyTankTexture, new Vector2(150, 0), Color.White);
            _spriteBatch.Draw(yellowTankTexture, new Vector2(200, 0), Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }


        public void PlayerTurn()
        {
            // Get player's commands and execute them
            List<string> playerCommands = GetRandomCommands();
            player.ExecuteCommands(playerCommands, walls, aiPlayer);
        }

        public void AIPlayerTurn()
        {
            // Get AI player's commands and execute them
            List<string> aiPlayerCommands = GetRandomCommands();
            aiPlayer.ExecuteCommands(aiPlayerCommands, walls, player);
        }

        public void DisplayGameState()
        {
            // Display the current state of the game (e.g., player health, AI player health, etc.)
            Console.WriteLine($"Player Health: {player.Health} | AI Player Health: {aiPlayer.Health}");
        }

        public bool IsGameOver()
        {
            // Check if the game is over (one of the players has 0 health)
            return player.Health <= 0 || aiPlayer.Health <= 0;
        }

        public string GetWinner()
        {
            // Determine the winner based on remaining health points
            if (player.Health <= 0) return aiPlayer.Name;
            else return player.Name;
        }

        private List<Wall> GenerateRandomWalls()
        {
            // Generate and return a list of random walls on the game board
            // You can implement the logic for generating random walls here
            // For simplicity, let's create a list with a fixed number of walls for now
            List<Wall> generatedWalls = new List<Wall>();
            for (int i = 0; i < 5; i++)
            {
                generatedWalls.Add(new Wall(20)); // Each wall has 20 health points
            }
            return generatedWalls;
        }

        private List<string> GetRandomCommands()
        {
            // Generate and return a list of random commands for the player or AI player
            List<string> commands = new List<string>();
            Random random = new Random();

            // For simplicity, let's randomly choose four commands from the available eight
            List<string> availableCommands = new List<string> { "Forward", "Back", "TurnLeft", "TurnRight", "Fire" };
            for (int i = 0; i < 4; i++)
            {
                int index = random.Next(availableCommands.Count);
                commands.Add(availableCommands[index]);
            }

            return commands;
        }

    }
}
