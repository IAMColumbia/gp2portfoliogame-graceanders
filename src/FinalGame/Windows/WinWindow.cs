using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Windows
{
    internal class WinWindow : Window
    {
        private Vector2 titlePosition;
        private Vector2 textPosition;
        internal SpriteFont smallFont;

        public WinWindow(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            // Set up the sprite batch and load content
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("Arial");
            title = Game.Content.Load<SpriteFont>("Title");
            smallFont = Game.Content.Load<SpriteFont>("Small");
            background = Game.Content.Load<Texture2D>("ShopBackground");

            // Set up the sprite batch and load content
            windowBounds = new Rectangle((GraphicsDevice.Viewport.Width - background.Width) / 2,
                                         (GraphicsDevice.Viewport.Height - background.Height) / 2,
                                         background.Width, background.Height);

            // Calculate the positions of the various elements
            titlePosition = new Vector2(windowBounds.X + 40, windowBounds.Y + 40);
            textPosition = new Vector2(windowBounds.X + 40, windowBounds.Y + 100);

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
            spriteBatch.DrawString(title, "You Won!", titlePosition, Color.Black);

            spriteBatch.DrawString(font, "You have gotten all 10 plants to Excelent quality!\nPress [R] to Restart\nor\nPress [E] to Exit\n\nThank you for playing!",textPosition, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
