﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    internal class StatsWindow : Window
    {
        private Vector2 titlePosition;
        internal Texture2D itemSquare;

        public StatsWindow(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            // Set up the sprite batch and load content
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("Arial");
            title = Game.Content.Load<SpriteFont>("Title");
            background = Game.Content.Load<Texture2D>("ShopBackground");
            itemSquare = Game.Content.Load<Texture2D>("InventorySprite");

            // Set up the sprite batch and load content
            windowBounds = new Rectangle((GraphicsDevice.Viewport.Width - background.Width) / 2,
                                         (GraphicsDevice.Viewport.Height - background.Height) / 2,
                                         background.Width, background.Height);

            // Calculate the positions of the various elements
            titlePosition = new Vector2(windowBounds.X + 40, windowBounds.Y + 40);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            // Draw the background
            spriteBatch.Draw(background, windowBounds, Color.White);

            // Draw the title
            spriteBatch.DrawString(title, "Status", titlePosition, Color.Black);

            //Draw display squares

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
