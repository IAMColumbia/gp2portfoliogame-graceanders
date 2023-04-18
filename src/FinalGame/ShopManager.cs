using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalGame
{
    public class ShopManager : DrawableGameComponent
    {
        Game Game;
        PlayableCharacter PC;
        InputHandler input;

        private List<Item> ShopInventory;
        private List<Item> BuyableItems = new List<Item>();
        private Random random = new Random();

        Item BeetSeeds, CornSeeds, GarlicSeeds, GrapeSeeds, GreenBeanSeeds, MelonSeeds, PotatoSeeds, RadishSeeds, StrawberrySeeds, TomatoSeeds;

        private bool isShopOpen;

        internal bool IsShopOpen
        {
            get { return isShopOpen; }
        }

        internal ShopManager(Game game, PlayableCharacter p, InputHandler IH) : base(game)
        {
            Game = game;
            PC = p;
            input = IH;

            BeetSeeds = new Item(game, "Beet Seeds", 20);
            CornSeeds = new Item(game, "Corn Seeds", 50);
            GarlicSeeds = new Item(game, "Garlic Seeds", 40);
            GrapeSeeds = new Item(game, "Grape Seeds", 60);
            GreenBeanSeeds = new Item(game, "Green Bean Seeds", 60);
            MelonSeeds = new Item(game, "Melon Seeds", 80);
            PotatoSeeds = new Item(game, "Potato Seeds", 50);
            RadishSeeds = new Item(game, "Radish Seeds", 40);
            StrawberrySeeds = new Item(game, "Strawberry Seeds", 100);
            TomatoSeeds = new Item(game, "Tomato Seeds", 50);

            ShopInventory = new List<Item>() { BeetSeeds, CornSeeds, GarlicSeeds, GrapeSeeds, GreenBeanSeeds, MelonSeeds, PotatoSeeds, RadishSeeds, StrawberrySeeds , TomatoSeeds};

            // Pick 5 random items to be buyable
            for (int i = 0; i < 5; i++)
            {
                int index = random.Next(ShopInventory.Count);
                Item item = ShopInventory[index];
                BuyableItems.Add(item);
                ShopInventory.RemoveAt(index);
            }
        }

        internal void BuyItem(Item item, Player player)
        {
            int cost = item.Worth;
            if (player.gold >= cost && ShopInventory.Contains(item))
            {
                player.gold -= cost;
                player.AddItem(item);
                ShopInventory.Remove(item);
            }
        }

        internal void SellItem(Item item, Player player)
        {
            int cost = item.Worth / 2;
            if (player.HasItem(item))
            {
                player.gold += cost;
                player.RemoveItem(item);
                ShopInventory.Add(item);
            }
        }

        internal int GetInventoryCount(Item item)
        {
            return ShopInventory.Count(i => i == item);
        }

        public void OpenShopWindow()
        {
            if (!isShopOpen)
            {
                // TODO: Code to open the shop window
                // For example, you could create a new ShopWindow object and add it to the game components
                ShopWindow shopWindow = new ShopWindow(Game,ShopInventory,PC ,input);
                Game.Components.Add(shopWindow);

                isShopOpen = true;

                // Pause the game update loop
                Game.IsFixedTimeStep = false;
                Game.IsMouseVisible = false;
            }
        }

        public void CloseShopWindow()
        {
            if (isShopOpen)
            {
                // TODO: Code to close the shop window
                // For example, you could remove the ShopWindow object from the game components
                ShopWindow shopWindow = (ShopWindow)Game.Components.FirstOrDefault(c => c is ShopWindow);
                Game.Components.Remove(shopWindow);

                isShopOpen = false;

                // Resume the game update loop
                Game.IsFixedTimeStep = true;
                Game.IsMouseVisible = true;
            }
        }
    }
}
