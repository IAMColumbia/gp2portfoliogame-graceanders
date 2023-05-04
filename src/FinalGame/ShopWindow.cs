using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Util;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace FinalGame
{
    enum ShopMode 
    {
        Buy, Sell
    }
    public class ShopWindow : Window
    {
        
        private Vector2 titlePosition;
        private Vector2 itemsPosition;
        private Vector2 moneyPosition;

        private List<Item> displayItems;
        private List<Item> buyableItems;
        private List<Item> playerItems;

        private int selectedItemIndex;

        PlayableCharacter PC;

        ShopMode SM;

        InputHandler Input;

        internal ShopWindow(Game game, List<Item> items, PlayableCharacter p, InputHandler input) : base(game)
        {
            buyableItems = items;
            PC = p;
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
            titlePosition = new Vector2(windowBounds.X + 40, windowBounds.Y + 40);
            itemsPosition = new Vector2(windowBounds.X + 40, windowBounds.Y + 90);
            moneyPosition = new Vector2(windowBounds.X + 40, windowBounds.Y + windowBounds.Height - 90);

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
                if(displayItems.Count != 0)
                    selectedItemIndex = (selectedItemIndex + displayItems.Count - 1) % displayItems.Count;
            }
            else if (Input.WasKeyPressed(Keys.Down))
            {
                if (displayItems.Count != 0)
                    selectedItemIndex = (selectedItemIndex + 1) % displayItems.Count;
            }

            // Check for input to buy or sell the selected item
            if (Input.WasKeyPressed(Keys.Enter))
            {
                Item selectedItem = displayItems[selectedItemIndex];
                if (SM == ShopMode.Buy && selectedItem.Worth <= PC.Player.gold)
                {
                    PC.Player.AddItem(selectedItem);
                    PC.Player.gold -= selectedItem.Worth;
                }
                else if (SM == ShopMode.Sell)
                {
                    PC.Player.RemoveItem(selectedItem);
                    PC.Player.gold += selectedItem.Worth;
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
            spriteBatch.DrawString(title, $"Shop: {SM.ToString()}                       " +
                $"| Enter Select | - Sell | + Buy |", titlePosition, Color.Black);

            // Draw the display items
            for (int i = 0; i < displayItems.Count; i++)
            {
                Color color = (i == selectedItemIndex) ? Color.Red : Color.Black;
                spriteBatch.DrawString(font, displayItems[i].Name + " (" + displayItems[i].Worth + ")", itemsPosition + new Vector2(0, i * 40), color);
            }

            // Draw the money
            spriteBatch.DrawString(font, "Money: " + PC.Player.gold, moneyPosition, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
