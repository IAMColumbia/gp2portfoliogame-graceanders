using FinalGame.Crops;
using FinalGame.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalGame.Managers
{
    public class ShopManager : DrawableGameComponent
    {
        PlayableCharacter PC;
        InputHandler input;

        private List<Item> ShopInventorySeeds;
        private List<Item> ShopInventoryFertilizer;
        private List<Item> BuyableItems = new List<Item>();
        private Random random = new Random();

        Item BeetSeeds, CornSeeds, GarlicSeeds, GrapeSeeds, GreenBeanSeeds, MelonSeeds, PotatoSeeds, RadishSeeds, StrawberrySeeds, TomatoSeeds;
        string BeetSeedTextureName, CornSeedTextureName, GarlicSeedTextureName, GrapeSeedTextureName, GreenBeanSeedTextureName, MelonSeedTextureName, PotatoSeedTextureName, RadishSeedTextureName, StrawberrySeedTextureName, TomatoSeedTextureName;

        Item BasicFertilizer, QualityFertilizer, DeluxeFertilizer;
        string BasicFertilizerTextureName, QualityFertilizerTextureName, DeluxeFertilizerTextureName;

        private bool isShopOpen;

        internal bool IsShopOpen
        {
            get { return isShopOpen; }
        }

        internal ShopManager(Game game, PlayableCharacter p, InputHandler IH) : base(game)
        {
            PC = p;
            input = IH;

            BeetSeeds = new Item(game, "Beet", 20, ItemType.Seed);
            CornSeeds = new Item(game, "Corn", 50, ItemType.Seed);
            GarlicSeeds = new Item(game, "Garlic", 40, ItemType.Seed);
            GrapeSeeds = new Item(game, "Grape", 60, ItemType.Seed);
            GreenBeanSeeds = new Item(game, "Green Bean", 60, ItemType.Seed);
            MelonSeeds = new Item(game, "Melon", 80, ItemType.Seed);
            PotatoSeeds = new Item(game, "Potato", 50, ItemType.Seed);
            RadishSeeds = new Item(game, "Radish", 40, ItemType.Seed);
            StrawberrySeeds = new Item(game, "Strawberry", 100, ItemType.Seed);
            TomatoSeeds = new Item(game, "Tomato", 50, ItemType.Seed);

            BasicFertilizer = new Item(game, "Basic Fertilizer", 20, ItemType.Fertalizer);
            QualityFertilizer = new Item(game, "Quality Fertilizer", 40, ItemType.Fertalizer);
            DeluxeFertilizer = new Item(game, "Deluxe Fertilizer", 60, ItemType.Fertalizer);

            PopulateShopInventory();

            // Pick 5 random items to be buyable
            RandomItems();
        }

        protected override void LoadContent()
        {
            #region Seeds
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

            BeetSeeds.spriteTexture = Game.Content.Load<Texture2D>(BeetSeedTextureName);
            CornSeeds.spriteTexture = Game.Content.Load<Texture2D>(CornSeedTextureName);
            GarlicSeeds.spriteTexture = Game.Content.Load<Texture2D>(GarlicSeedTextureName);
            GrapeSeeds.spriteTexture = Game.Content.Load<Texture2D>(GrapeSeedTextureName);
            GreenBeanSeeds.spriteTexture = Game.Content.Load<Texture2D>(GreenBeanSeedTextureName);
            MelonSeeds.spriteTexture = Game.Content.Load<Texture2D>(MelonSeedTextureName);
            PotatoSeeds.spriteTexture = Game.Content.Load<Texture2D>(PotatoSeedTextureName);
            RadishSeeds.spriteTexture = Game.Content.Load<Texture2D>(RadishSeedTextureName);
            StrawberrySeeds.spriteTexture = Game.Content.Load<Texture2D>(StrawberrySeedTextureName);
            TomatoSeeds.spriteTexture = Game.Content.Load<Texture2D>(TomatoSeedTextureName);
            #endregion

            #region Fertilizer
            BasicFertilizerTextureName = "Basic_Fertilizer";
            QualityFertilizerTextureName = "Quality_Fertilizer";
            DeluxeFertilizerTextureName = "Deluxe_Fertilizer";

            BasicFertilizer.spriteTexture = Game.Content.Load<Texture2D>(BasicFertilizerTextureName);
            QualityFertilizer.spriteTexture = Game.Content.Load<Texture2D>(QualityFertilizerTextureName);
            DeluxeFertilizer.spriteTexture = Game.Content.Load<Texture2D>(DeluxeFertilizerTextureName);
            #endregion

            base.LoadContent();
        }

        internal void PopulateShopInventory()
        {
            ShopInventorySeeds = new List<Item>() { BeetSeeds, CornSeeds, GarlicSeeds, GrapeSeeds, GreenBeanSeeds, MelonSeeds, PotatoSeeds, RadishSeeds, StrawberrySeeds, TomatoSeeds };

            ShopInventoryFertilizer = new List<Item> { BasicFertilizer, QualityFertilizer, DeluxeFertilizer };

        }

        int index;
        Item item;
        internal void RandomItems()
        {
            BuyableItems.Clear();

            //Adds 4 Seeds
            for (int i = 0; i < 4; i++)
            {
                index = random.Next(ShopInventorySeeds.Count);
                item = ShopInventorySeeds[index];
                BuyableItems.Add(item);
                ShopInventorySeeds.RemoveAt(index);
            }
            //Adds 1 Fertilizer
            index = random.Next(ShopInventoryFertilizer.Count);
            item = ShopInventoryFertilizer[index];
            BuyableItems.Add(item);

            PopulateShopInventory();
        }

        internal void BuyItem(Item item, Player player)
        {
            int cost = item.Worth;
            if (player.gold >= cost && ShopInventorySeeds.Contains(item))
            {
                player.gold -= cost;
                player.AddItem(item);
                ShopInventorySeeds.Remove(item);
            }
        }

        internal void SellItem(Item item, Player player)
        {
            int cost = item.Worth / 2;
            if (player.HasItem(item))
            {
                player.gold += cost;
                player.RemoveItem(item);
                ShopInventorySeeds.Add(item);
            }
        }

        internal int GetInventoryCount(Item item)
        {
            return ShopInventorySeeds.Count(i => i == item);
        }

        public void OpenShopWindow()
        {
            if (!isShopOpen)
            {
                ShopWindow shopWindow = new ShopWindow(Game, BuyableItems, PC, input);
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
