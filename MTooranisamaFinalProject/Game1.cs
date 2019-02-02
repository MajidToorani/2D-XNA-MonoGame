using AllInOneMono;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// Final Project
//    
// Majid Tooranisama
// 10/12/2018
//
// Revision history
//  28/11/2018    Created
//  28/11/2018    UI Designed
//  05/12/2018    Bug Fixed
//  09/12/2018    Comments Added

namespace MTooranisamaFinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        SpriteFont defaultFont1;
        SpriteFont defaultFont2;
        Song playSong;
        Song backgroundSong;

        //declare all the scenes here  from AllInOneMono
        private StartScene startScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        private InstructionScene instructionScene;
        private AboutScene aboutScene;
        // scene declaration ends

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;

            IsMouseVisible = true;
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
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth,
                            graphics.PreferredBackBufferHeight);

            base.Initialize();
        }
        private void hideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            defaultFont1 = Content.Load<SpriteFont>("Fonts/defaultFont1");
            defaultFont2 = Content.Load<SpriteFont>("Fonts/defaultFont2");

            Texture2D backgroundTexture = Content.Load<Texture2D>("Images/background0");
            Background b = new Background(this, spriteBatch, defaultFont1, defaultFont2, backgroundTexture);
            this.Components.Add(b);            

            // instantiate all scenes here
            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);
            startScene.show(); //make only startscene active

            actionScene = new ActionScene(this, spriteBatch);
            this.Components.Add(actionScene);

            instructionScene = new InstructionScene(this, spriteBatch);
            this.Components.Add(instructionScene);

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);

            aboutScene = new AboutScene(this, spriteBatch);
            this.Components.Add(aboutScene);

            //instantiation ends

            backgroundSong = Content.Load<Song>("HUD/DungeonTheme");
            MediaPlayer.Volume = 0.1f; //0=silent, 1=full
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundSong);

            playSong = Content.Load<Song>("HUD/Forest");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            int selectedIndex = 0;

            KeyboardState ks = Keyboard.GetState();

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    actionScene.show();
                    MediaPlayer.Play(playSong);
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    instructionScene.show();
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.show();
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    aboutScene.show();
                }
                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            if (actionScene.Enabled || instructionScene.Enabled || helpScene.Enabled || aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                    MediaPlayer.Play(backgroundSong);
                    LoadContent();
                }
            }
            if (ks.IsKeyDown(Keys.R))
            {
                LoadContent();
                actionScene.show();               
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

            base.Draw(gameTime);
        }
    }
}
