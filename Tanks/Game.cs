using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Tanks.Content;

namespace Tanks
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Player aiPlayer;
        private List<Wall> walls;

        Texture2D brickWallTexture;
        Texture2D greenTankTexture;
        Texture2D greyTankTexture;

        // command buttons
        Texture2D rightBtnTexture;
        private ControlButton controlButton;
        static string RIGHT = "right";

        // handle on mouse click evetns
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        // positions of sprites
        private Vector2 playerPos;
        private Vector2 AIPos;


        float ScreenHeight;
        float ScreenWidth;

        PlayerTank playerTank;
        AITank aITank;
        private float tankSpeed = 100;

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
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            player = new Player("Player", 100);
            aiPlayer = new Player("AI Player", 100);

            // Initialize sprite controlbutton
            controlButton = new ControlButton
            (
                rightBtnTexture, // Load your sprite texture
                new Vector2(100, 100),
                70,
                70,
                RIGHT
            );

            playerPos = new Vector2(10, 10);
            AIPos = new Vector2(ScreenWidth-160, ScreenHeight-160);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            brickWallTexture = Content.Load<Texture2D>("BrickWall");
            greenTankTexture = Content.Load<Texture2D>("GreenTank");
            greyTankTexture = Content.Load<Texture2D>("GreyTank");
            rightBtnTexture = Content.Load<Texture2D>("Right");

            //genereate walls
            walls = GenerateRandomWalls(ScreenWidth, ScreenHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            // Check for left-click on the sprite
            if (currentMouseState.LeftButton == ButtonState.Pressed &&
                previousMouseState.LeftButton == ButtonState.Released &&
                controlButton.Bounds.Contains(currentMouseState.Position))
            {
                // The sprite is left-clicked
                Console.WriteLine("Sprite is left-clicked!");
                
            }

            var kstate = Keyboard.GetState();

            if(kstate.IsKeyDown(Keys.Space))
            {

            }


            if (kstate.IsKeyDown(Keys.Up) && !CheckTankWallCollision(playerTank, walls))
            {
                playerPos.Y -= tankSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down) && !CheckTankWallCollision(playerTank, walls))
            {
                playerPos.Y += tankSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Left) && !CheckTankWallCollision(playerTank, walls))
            {
                playerPos.X -= tankSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Right) && !CheckTankWallCollision(playerTank, walls))
            {
                playerPos.X += tankSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            //keep within playing area

            if (playerPos.X > _graphics.PreferredBackBufferWidth - greyTankTexture.Width / 2)
            {
                playerPos.X = _graphics.PreferredBackBufferWidth - greyTankTexture.Width / 2;
            }
            else if (playerPos.X < greyTankTexture.Width / 2)
            {
                playerPos.X = greyTankTexture.Width / 2;
            }

            if (playerPos.Y > _graphics.PreferredBackBufferHeight - greyTankTexture.Height / 2)
            {
                playerPos.Y = _graphics.PreferredBackBufferHeight - greyTankTexture.Height / 2;
            }
            else if (playerPos.Y < greyTankTexture.Height / 2)
            {
                playerPos.Y = greyTankTexture.Height / 2;
            }


            base.Update(gameTime);
        }


        bool CheckTankWallCollision(PlayerTank tank, List<Wall> walls)
        {
            // Check if the tank collides with any wall
            foreach (var wall in walls)
            {
                if (tank.Bounds.Intersects(wall.Bounds))
                {
                    return true; // Collision detected
                }
            }
            return false; // No collision
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();


            playerTank = new PlayerTank
            (
                greyTankTexture, // Load your sprite texture
                playerPos,
                86,
                155
            );

            aITank = new AITank
            (
                greenTankTexture, // Load your sprite texture
                AIPos,
                64,
                64
            );

            _spriteBatch.Draw(playerTank.Texture, playerTank.Position, Color.White);
            _spriteBatch.Draw(aITank.Texture, aITank.Position, Color.White);

            _spriteBatch.Draw(rightBtnTexture, new Vector2(300, 300), Color.White);

            // position walls
            foreach (var wall in walls)
            {
                _spriteBatch.Draw(brickWallTexture, new Vector2(wall.YPos, wall.XPos), Color.White);
            }


            _spriteBatch.End();


            base.Draw(gameTime);
        }


        // **
        // methodsw not overridden below here
        // **

        private List<Wall> GenerateRandomWalls(float screenWidth, float screenHeight)
        {
            // Generate and return a list of random walls on the game board

            // Get the vector they sit at
            float xPos = 0;
            float yPos = 0;
            Random random = new Random();

            // For simplicity, let's create a list with a fixed number of walls for now
            List<Wall> generatedWalls = new List<Wall>();
            for (int i = 0; i < 10; i++)
            {
                // Generate initial position
                xPos = (float)(random.NextDouble() * screenWidth);
                yPos = (float)(random.NextDouble() * screenHeight);

                // Check for collision with existing walls
                while (CheckCollision(generatedWalls, xPos, yPos))
                {
                    // Adjust position if collision occurs
                    xPos = (float)(random.NextDouble() * screenWidth);
                    yPos = (float)(random.NextDouble() * screenHeight);
                }

                generatedWalls.Add(new Wall(20, xPos, yPos)); // Each wall has 20 health points
            }
            return generatedWalls;
        }

        private bool CheckCollision(List<Wall> walls, float x, float y)
        {
            // Check if a collision occurs with any existing wall
            foreach (var wall in walls)
            {
                // Assuming the wall has a fixed width and height (adjust if necessary)
                if (x < wall.XPos + wall.Width &&
                    x + wall.Width > wall.XPos &&
                    y < wall.YPos + wall.Height &&
                    y + wall.Height > wall.YPos)
                {
                    return true; // Collision detected
                }
            }
            return false; // No collision
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
