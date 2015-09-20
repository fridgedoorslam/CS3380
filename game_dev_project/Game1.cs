using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game_dev_project
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D walkUp, walkDown, walkLeft, walkRight, sprintUp, sprintDown, sprintLeft, sprintRight,
            walkUpEquipped, walkDownEquipped, walkLeftEquipped, walkRightEquipped, sprintUpEquipped, sprintDownEquipped,
            sprintLeftEquipped, sprintRightEquipped, stand, currentAnimation;
        Rectangle destinationRectangle;
        Rectangle sourceRectangle;
        float elapsedTime;
        float delay = 200f;
        int frame = 0;
        KeyboardState ks;
        Vector2 playerPosition = new Vector2();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            destinationRectangle = new Rectangle(0, 0, 36, 33);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Walk Unequipped
            walkUp = Content.Load<Texture2D>("Player/walk/walk-up");
            walkDown = Content.Load<Texture2D>("Player/walk/walk-down");
            walkLeft = Content.Load<Texture2D>("Player/walk/walk-left");
            walkRight = Content.Load<Texture2D>("Player/walk/walk-right");

            // Walk Equipped
            walkUpEquipped = Content.Load<Texture2D>("Player/walk/walk-up-sword");
            walkDownEquipped = Content.Load<Texture2D>("Player/walk/walk-down-sword");
            walkLeftEquipped = Content.Load<Texture2D>("Player/walk/walk-left-sword");
            walkRightEquipped = Content.Load<Texture2D>("Player/walk/walk-right-sword");

            // Sprint Unequipped
            sprintUp = Content.Load<Texture2D>("Player/sprint/sprint-up");
            sprintDown = Content.Load<Texture2D>("Player/sprint/sprint-down");
            sprintLeft = Content.Load<Texture2D>("Player/sprint/sprint-left");
            sprintRight = Content.Load<Texture2D>("Player/sprint/sprint-right");

            // Sprint Equipped
            sprintUpEquipped = Content.Load<Texture2D>("Player/sprint/sprint-up-sword");
            sprintDownEquipped = Content.Load<Texture2D>("Player/sprint/sprint-down-sword");
            sprintLeftEquipped = Content.Load<Texture2D>("Player/sprint/sprint-left-sword");
            sprintRightEquipped = Content.Load<Texture2D>("Player/sprint/sprint-right-sword");

            stand = Content.Load<Texture2D>("Player/stand/stand");

            currentAnimation = walkDownEquipped;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        private void Animate(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime >= delay)
            {
                if (frame >= 3)
                {
                    frame = 0;
                }
                else
                {
                    frame++;
                }
                elapsedTime = 0;
            }

            sourceRectangle = new Rectangle(36 * frame, 0, 36, 33);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            destinationRectangle = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 36, 33);
            ks = Keyboard.GetState();


            // Move Right
            if (ks.IsKeyDown(Keys.D))
            {
                if (ks.IsKeyDown(Keys.LeftShift))
                {
                    playerPosition.X += 3f;
                    currentAnimation = sprintRightEquipped;
                }
                else
                {
                    playerPosition.X += 1.5f;
                    currentAnimation = walkRightEquipped;
                }
                Animate(gameTime);
            }

            // Move Left
            else if (ks.IsKeyDown(Keys.A))
            {
                if (ks.IsKeyDown(Keys.LeftShift))
                {
                    playerPosition.X -= 3;
                    currentAnimation = sprintLeftEquipped;
                }
                else
                {
                    playerPosition.X -= 1.5f;
                    currentAnimation = walkLeftEquipped;
                }
                Animate(gameTime);
            }

            // Move Up
            else if (ks.IsKeyDown(Keys.W))
            {
                if (ks.IsKeyDown(Keys.LeftShift))
                {
                    playerPosition.Y -= 3f;
                    currentAnimation = sprintUpEquipped;
                }
                else
                {
                    playerPosition.Y -= 1.5f;
                    currentAnimation = walkUpEquipped;
                }
                Animate(gameTime);
            }

            // Move Down
            else if (ks.IsKeyDown(Keys.S))
            {
                if (ks.IsKeyDown(Keys.LeftShift))
                {
                    playerPosition.Y += 3f;
                    currentAnimation = sprintDownEquipped;
                }
                else
                {
                    playerPosition.Y += 1.5f;
                    currentAnimation = walkDownEquipped;
                }
                Animate(gameTime);
            }

            // Stand
            else
            {
                sourceRectangle = new Rectangle(0, 0, 36, 33);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(currentAnimation, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
