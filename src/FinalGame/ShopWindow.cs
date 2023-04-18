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
    enum ShopMode 
    {
        Buy, Sell
    }
    public class ShopWindow : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font, title;
        private Texture2D background;
        private Rectangle windowBounds;
        private Vector2 titlePosition;
        private Vector2 itemsPosition;
        private Vector2 moneyPosition;

        private List<Item> displayItems;
        private List<Item> buyableItems;
        private List<Item> playerItems;

        private int selectedItemIndex;
        private int Money;

        ShopMode SM;

        internal event EventHandler<Item> BuyItemSelected;

        internal event EventHandler<Item> SellItemSelected;

        InputHandler Input;

        internal ShopWindow(Game game, List<Item> items, PlayableCharacter p, InputHandler input) : base(game)
        {
            buyableItems = items;
            Money = p.Player.gold;
            playerItems = p.Player.Inventory;
            Input = input;

            SM = ShopMode.Buy;
        }

        public override void Initialize()
        {
            // Set up the sprite batch and load content
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("Arial");
            title = Game.Content.Load<SpriteFont>("Title");
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
            ShopModeUpdated();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ShopModeUpdated();

            // Check for input to change the selected item
            if (Input.WasKeyPressed(Keys.Up))
            {
                selectedItemIndex = (selectedItemIndex + displayItems.Count - 1) % displayItems.Count;
            }
            else if (Input.WasKeyPressed(Keys.Down))
            {
                selectedItemIndex = (selectedItemIndex + 1) % displayItems.Count;
            }

            // Check for input to buy or sell the selected item
            if (Input.WasKeyPressed(Keys.Enter))
            {
                Item selectedItem = displayItems[selectedItemIndex];
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

            if (Input.WasKeyPressed(Keys.OemMinus))
            {
                SM = ShopMode.Sell; 
            }

            if (Input.WasKeyPressed(Keys.OemPlus))
            {
                SM = ShopMode.Buy;
            }

                base.Update(gameTime);
        }

        internal void ShopModeUpdated()
        {
            switch (SM)
            {
                case ShopMode.Buy:
                    displayItems = buyableItems;
                    break;
                case ShopMode.Sell:
                    displayItems = playerItems; 
                    break;
            }

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            // Draw the background
            spriteBatch.Draw(background, windowBounds, Color.White);

            // Draw the title
            spriteBatch.DrawString(title, $"Shop: {SM.ToString()}", titlePosition, Color.Black);

            // Draw the display items
            for (int i = 0; i < displayItems.Count; i++)
            {
                Color color = (i == selectedItemIndex) ? Color.Red : Color.Black;
                spriteBatch.DrawString(font, displayItems[i].Name + " (" + displayItems[i].Worth + ")", itemsPosition + new Vector2(0, i * 40), color);
            }

            // Draw the money
            spriteBatch.DrawString(font, "Money: " + Money, moneyPosition, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
