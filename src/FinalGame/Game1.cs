﻿using FinalGame.Managers;
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

        GameManager gameM;

        GridTerrain gT;

        GridManager gridM;

        GardenManager gardenM;

        PlayableCharacter player;

        ShopManager shopM;

        StatsManager statsM;

        AnimationManager animationM;

        WinManager winM;

        InputHandler input;
        GameConsole console;

        Rectangle ScreenSize;
        int Margin = 110;

        int maxWidth = 1700;
        int maxHeight = 970;
        public Game1()
        {
            Window.Title = "Perfecting Cultivation";

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            input = new InputHandler(this);
            console = new GameConsole(this);

            this.Components.Add(input);
            this.Components.Add(console);

            int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - Margin * 2;
            int h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Margin;

            if (w > maxWidth)//locking width and hight if bounds are too large
            {
                w = maxWidth;
                h = maxHeight;
            }

            this._graphics.PreferredBackBufferWidth = w;
            this._graphics.PreferredBackBufferHeight = h;

            ScreenSize = new Rectangle(0, 0, w, h);

            

            gT = new GridTerrain(this);
            this.Components.Add(gT);

            gridM = new GridManager(this, ScreenSize, gT, input);
            this.Components.Add(gridM);

            gardenM = new GardenManager(this);
            this.Components.Add(gardenM);

            player = new PlayableCharacter(this);
            this.Components.Add(player);

            shopM = new ShopManager(this, player, input);
            this.Components.Add(shopM);

            statsM = new StatsManager(this, player, input);
            this.Components.Add(statsM);

            animationM = new AnimationManager(this);
            this.Components.Add(animationM);

            winM = new WinManager(this);
            this.Components.Add(winM);

            gameM = new GameManager(this, input, player, gridM, gardenM, shopM, statsM, animationM, winM);
            this.Components.Add(gameM);

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
            GraphicsDevice.Clear(Color.DimGray);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}