using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace FinalGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        GameManager gm;

        GridTerrain gT;

        GridManager gridM;

        PlayableCharacter player;

        InputHandler input;
        GameConsole console;

        Rectangle ScreenSize;
        int Margin = 110;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            input = new InputHandler(this);
            console = new GameConsole(this);

            this.Components.Add(input);
            this.Components.Add(console);

            int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - Margin * 2;
            int h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Margin;

            ScreenSize = new Rectangle(0, 0, w, h);

            this._graphics.PreferredBackBufferWidth = w;
            this._graphics.PreferredBackBufferHeight = h;

            gT = new GridTerrain(this);
            this.Components.Add(gT);

            gridM = new GridManager(this, ScreenSize,gT);
            this.Components.Add(gridM);

            player = new PlayableCharacter(this);
            this.Components.Add(player);

            gm = new GameManager(this, player, gridM);
            this.Components.Add(gm);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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
            GraphicsDevice.Clear(Color.LightBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}