using FinalGame.Crops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        string BeetSeedTextureName, CornSeedTextureName, GarlicSeedTextureName, GrapeSeedTextureName, GreenBeanSeedTextureName, MelonSeedTextureName, PotatoSeedTextureName, RadishSeedTextureName, StrawberrySeedTextureName, TomatoSeedTextureName;

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

            BeetSeeds = new Item(game, "Beet", 20);
            CornSeeds = new Item(game, "Corn", 50);
            GarlicSeeds = new Item(game, "Garlic", 40);
            GrapeSeeds = new Item(game, "Grape", 60);
            GreenBeanSeeds = new Item(game, "Green Bean", 60);
            MelonSeeds = new Item(game, "Melon", 80);
            PotatoSeeds = new Item(game, "Potato", 50);
            RadishSeeds = new Item(game, "Radish", 40);
            StrawberrySeeds = new Item(game, "Strawberry", 100);
            TomatoSeeds = new Item(game, "Tomato", 50);

            PopulateShopInventory();

            // Pick 5 random items to be buyable
            RandomItems();
        }

        protected override void LoadContent()
        {
            BeetSeedTextureName = "Crops/Beet_Seeds";
            CornSeedTextureName = "Crops/Corn_Seeds";
            GarlicSeedTextureName = "Crops/Garlic_Seeds";
            GrapeSeedTextureName = "Crops/Grape_Seeds";
            GreenBeanSeedTextureName = "Crops/Green_Bean_Seeds";
            MelonSeedTextureName = "Crops/Melon_Seeds";
            PotatoSeedTextureName = "Crops/Potato_Seeds";
            RadishSeedTextureName = "Crops/Radish_Seeds";
            StrawberrySeedTextureName = "Crops/Strawberry_Seeds";
            TomatoSeedTextureName = "Crops/Tomato_Seeds";

            BeetSeeds.spriteTexture = this.Game.Content.Load<Texture2D>(BeetSeedTextureName);
            CornSeeds.spriteTexture = this.Game.Content.Load<Texture2D>(CornSeedTextureName);
            GarlicSeeds.spriteTexture = this.Game.Content.Load<Texture2D>(GarlicSeedTextureName);
            GrapeSeeds.spriteTexture = this.Game.Content.Load<Texture2D>(GrapeSeedTextureName);
            GreenBeanSeeds.spriteTexture = this.Game.Content.Load<Texture2D>(GreenBeanSeedTextureName);
            MelonSeeds.spriteTexture = this.Game.Content.Load<Texture2D>(MelonSeedTextureName);
            PotatoSeeds.spriteTexture = this.Game.Content.Load<Texture2D>(PotatoSeedTextureName);
            RadishSeeds.spriteTexture = this.Game.Content.Load<Texture2D>(RadishSeedTextureName);
            StrawberrySeeds.spriteTexture = this.Game.Content.Load<Texture2D>(StrawberrySeedTextureName);
            TomatoSeeds.spriteTexture = this.Game.Content.Load<Texture2D>(TomatoSeedTextureName);

            base.LoadContent();
        }

        internal void PopulateShopInventory()
        {
            ShopInventory = new List<Item>() { BeetSeeds, CornSeeds, GarlicSeeds, GrapeSeeds, GreenBeanSeeds, MelonSeeds, PotatoSeeds, RadishSeeds, StrawberrySeeds, TomatoSeeds };

        }

        internal void RandomItems()
        {
            BuyableItems.Clear();

            for (int i = 0; i < 5; i++)
            {
                int index = random.Next(ShopInventory.Count);
                Item item = ShopInventory[index];
                BuyableItems.Add(item);
                ShopInventory.RemoveAt(index);
            }
            PopulateShopInventory();
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
                ShopWindow shopWindow = new ShopWindow(Game,BuyableItems,PC ,input);
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
