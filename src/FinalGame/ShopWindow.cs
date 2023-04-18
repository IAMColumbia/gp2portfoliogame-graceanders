using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Util;
using Microsoft.Xna.Framework.Input;

namespace FinalGame
{
    public class ShopWindow : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Texture2D background;
        private Rectangle windowBounds;
        private Vector2 titlePosition;
        private Vector2 itemsPosition;
        private Vector2 moneyPosition;
        private List<Item> buyableItems;
        private int selectedItemIndex;
        private int Money;

        internal event EventHandler<Item> BuyItemSelected;

        internal event EventHandler<Item> SellItemSelected;

        InputHandler Input;

        internal ShopWindow(Game game, List<Item> items, int playerMoney, InputHandler input) : base(game)
        {
            buyableItems = items;
            Money = playerMoney;
            Input = input;
        }

        public override void Initialize()
        {
            // Set up the sprite batch and load content
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("Arial");
            background = Game.Content.Load<Texture2D>("ShopBackground");

            // Calculate the bounds of the window
            windowBounds = new Rectangle((GraphicsDevice.Viewport.Width - background.Width) / 2,
                                         (GraphicsDevice.Viewport.Height - background.Height) / 2,
                                         background.Width, background.Height);

            // Calculate the positions of the various elements
            titlePosition = new Vector2(windowBounds.X + 20, windowBounds.Y + 20);
            itemsPosition = new Vector2(windowBounds.X + 20, windowBounds.Y + 80);
            moneyPosition = new Vector2(windowBounds.X + 20, windowBounds.Y + windowBounds.Height - 60);

            // Set the selected item to the first item in the list
            selectedItemIndex = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Check for input to change the selected item
            if (Input.WasKeyPressed(Keys.Up))
            {
                selectedItemIndex = (selectedItemIndex + buyableItems.Count - 1) % buyableItems.Count;
            }
            else if (Input.WasKeyPressed(Keys.Down))
            {
                selectedItemIndex = (selectedItemIndex + 1) % buyableItems.Count;
            }

            // Check for input to buy or sell the selected item
            if (Input.WasKeyPressed(Keys.Enter))
            {
                Item selectedItem = buyableItems[selectedItemIndex];
                if (BuyItemSelected != null && selectedItem.Worth <= Money)
                {
                    BuyItemSelected(this, selectedItem);
                    Money -= selectedItem.Worth;
                }
                else if (SellItemSelected != null)
                {
                    SellItemSelected(this, selectedItem);
                    Money += selectedItem.Worth / 2;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            // Draw the background
            spriteBatch.Draw(background, windowBounds, Color.White);

            // Draw the title
            spriteBatch.DrawString(font, "Shop", titlePosition, Color.Black);

            // Draw the buyable items
            for (int i = 0; i < buyableItems.Count; i++)
            {
                Color color = (i == selectedItemIndex) ? Color.Red : Color.Black;
                spriteBatch.DrawString(font, buyableItems[i].Name + " (" + buyableItems[i].Worth + ")\n", itemsPosition + new Vector2(0, i * 20), color);
            }

            // Draw the money
            spriteBatch.DrawString(font, "Money: " + Money, moneyPosition, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
