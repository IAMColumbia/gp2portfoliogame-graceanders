using FinalGame.Crops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    enum ItemType
    {
        Plant, Seed, Fertalizer
    }

    internal class Item : DrawableSprite
    {
        private string name;
        public string Name
        { get { return name; } set { name = value; } }

        private int worth;
        public int Worth
        { get { return worth; } set { worth = value; } }

        internal int PlantIndex;

        internal Texture2D ItemTexture;

        private ItemType itemType;
        public ItemType ItemType
        {
            get { return this.itemType; }
            set { this.itemType = value; }
        }

        public Item(Game game) : base(game) { }

        public Item(Game game, string plantName, int worth) : base(game) 
        {
            this.Name = $"{plantName} Seed";
            this.PlantIndex = CalculatePlantIndex(plantName);
            this.Worth = worth;

            itemType = ItemType.Seed;
        }


        internal int ReturnPlantIndex() { return this.PlantIndex; }

        int val;
        private Item item;

        private int CalculatePlantIndex(string name)
        {
            switch (name)
            {
                case "Beet":
                    val = 0;
                    break;
                case "Corn":
                    val = 1;
                    break;
                case "Garlic":
                    val = 2;
                    break;
                case "Grape":
                    val = 3;
                    break;
                case "Green Bean":
                    val = 4;
                    break;
                case "Melon":
                    val = 5;
                    break;
                case "Potato":
                    val = 6;
                    break;
                case "Radish":
                    val = 7;
                    break;
                case "Strawberry":
                    val = 8;
                    break;
                case "Tomato":
                    val = 9;
                    break;
            }
            return val;
        }
    }
}
