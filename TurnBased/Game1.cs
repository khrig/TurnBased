#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using TurnBased.States;
#endregion

namespace TurnBased {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        StateManager stateManager = new StateManager();
        Renderer renderer;

        private Queue<string> actions = new Queue<string>();

        public Game1()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 640;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            stateManager.Add("player", new PlayerState());
            stateManager.Add("computer", new AIState());

            stateManager.Push("player");

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            renderer = new Renderer(TextureManager.CreateTexture(GraphicsDevice, 20, 20, Color.Green), 
                TextureManager.CreateTexture(GraphicsDevice, 20, 20, Color.Red), 
                Content.Load<Texture2D>("spaceship32x32"),
                Content.Load<SpriteFont>("monolight12"));
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UIInput();
            stateManager.Act(actions);
            stateManager.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            renderer.DrawStateModel(spriteBatch, stateManager);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private KeyboardState lastKeyBoardState;
        private MouseState lastMouseState;
        private void UIInput() {
            KeyboardState currentKeyBoardState = Keyboard.GetState();
            MouseState currentMouseState = Mouse.GetState();
            if (lastKeyBoardState.IsKeyDown(Keys.S) && currentKeyBoardState.IsKeyUp(Keys.S)) {
                actions.Enqueue("shoot");
            }
            if (lastKeyBoardState.IsKeyDown(Keys.Space) && currentKeyBoardState.IsKeyUp(Keys.Space)) {
                actions.Enqueue("weapon");
            }
            if (lastKeyBoardState.IsKeyDown(Keys.G) && currentKeyBoardState.IsKeyUp(Keys.G)) {
                actions.Enqueue("changeCharacter");
            }
            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released) {
                actions.Enqueue("move;" + Mouse.GetState().X + "," + Mouse.GetState().Y);
            }
            lastKeyBoardState = currentKeyBoardState;
            lastMouseState = currentMouseState;
        }
    }
}
