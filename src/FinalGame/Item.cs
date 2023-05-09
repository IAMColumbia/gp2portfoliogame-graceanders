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
        Plant, Seed, Fertalizer, FreePlot
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

        internal int Count;

        internal Texture2D ItemTexture;

        private ItemType itemType;
        public ItemType ItemType
        {
            get { return this.itemType; }
            set { this.itemType = value; }
        }

        public Item(Game game) : base(game) { }

        public Item(Game game, string name, int worth, ItemType IT) : base(game) 
        {
            this.Name = $"{name}";

            if (IT == ItemType.Seed)
            {
                this.Name += " Seed";
                this.PlantIndex = CalculatePlantIndex(name);
            }
            
            this.Worth = worth;
            this.ItemType = IT;
        }

        internal int ReturnPlantIndex() { return this.PlantIndex; }

        int val;

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
