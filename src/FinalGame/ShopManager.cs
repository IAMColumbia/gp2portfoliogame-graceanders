using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalGame
{
    public class ShopManager : DrawableGameComponent
    {
        private Dictionary<Item, int> ShopInventory = new Dictionary<Item, int>();
        private List<Item> BuyableItems = new List<Item>();
        private Random random = new Random();

        Item BeetSeeds, CornSeeds, GarlicSeeds, GrapeSeeds, GreenBeanSeeds, MelonSeeds, PotatoSeeds, RadishSeeds, StrawberrySeeds, TomatoSeeds;

        public ShopManager(Game game) : base(game)
        {
            BeetSeeds = new Item(game, "Beet Seeds", 20);
            CornSeeds = new Item(game, "Corn Seeds", 150);//may adjust price
            GarlicSeeds = new Item(game, "Garlic Seeds", 40);
            GrapeSeeds = new Item(game, "Grape Seeds", 60);
            GreenBeanSeeds = new Item(game, "Green Bean Seeds", 60);
            MelonSeeds = new Item(game, "Melon Seeds", 80);
            PotatoSeeds = new Item(game, "Potato Seeds", 50);
            RadishSeeds = new Item(game, "Radish Seeds", 40);
            StrawberrySeeds = new Item(game, "Strawberry Seeds", 100);
            TomatoSeeds = new Item(game, "Tomato Seeds", 50);

            ShopInventory.Add(BeetSeeds, 10);
            ShopInventory.Add(CornSeeds, 10);
            ShopInventory.Add(GarlicSeeds, 10);
            ShopInventory.Add(GrapeSeeds, 10);
            ShopInventory.Add(GreenBeanSeeds, 10);
            ShopInventory.Add(MelonSeeds, 10);
            ShopInventory.Add(PotatoSeeds, 10);
            ShopInventory.Add(RadishSeeds, 10);
            ShopInventory.Add(StrawberrySeeds, 10);
            ShopInventory.Add(TomatoSeeds, 10);

            // Pick 5 random items to be buyable
            var items = ShopInventory.Keys.ToList();
            for (int i = 0; i < 5; i++)
            {
                int index = random.Next(items.Count);
                Item item = items[index];
                BuyableItems.Add(item);
                items.RemoveAt(index);
            }
        }

        internal void BuyItem(Item item, Player player)
        {
            int cost = item.Worth;
            if (player.gold >= cost && ShopInventory.ContainsKey(item) && ShopInventory[item] > 0)
            {
                player.gold -= cost;
                player.AddItem(item);
                ShopInventory[item]--;
            }
        }

        internal void SellItem(Item item, Player player)
        {
            int cost = item.Worth / 2;
            if (player.HasItem(item) && ShopInventory.ContainsKey(item))
            {
                player.gold += cost;
                player.RemoveItem(item);
                ShopInventory[item]++;
            }
        }
        
        internal int GetInventoryCount(Item item)
        {
            if (ShopInventory.ContainsKey(item))
            {
                return ShopInventory[item];
            }
            else
            {
                return 0;
            }
        }
    }
}
