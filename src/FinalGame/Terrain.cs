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
    enum TerrainType { Grass, Water, Soil, Sand }

    internal class Terrain
    {
        
        public List<Terrain> Terrains = new List<Terrain>();

        internal Texture2D Texture { get; set; }
        public TerrainType Type { get; set; }

        Terrain Grass, Water, Soil, Sand;

        //int Speed;//do I want to have an int or float that will adjust float speed depending on terrain?


        protected override void LoadContent() //Load the content once and each time one of the textures is drawn it is utilizes the same terrain
        {
            Grass.Texture = this.Game.Content.Load<Texture2D>("Terrain_Grass");
            Grass.Type = TerrainType.Grass;
            Terrains.Add(Grass);
            Water.Texture = this.Game.Content.Load<Texture2D>("Terrain_Water");
            Water.Type = TerrainType.Water;
            Terrains.Add(Water);
            Soil.Texture = this.Game.Content.Load<Texture2D>("Terrain_Soil");
            Soil.Type = TerrainType.Soil;
            Terrains.Add(Soil);
            Sand.Texture = this.Game.Content.Load<Texture2D>("Terrain_Sand");
            Sand.Type = TerrainType.Sand;
            Terrains.Add(Sand);
        }
    }
}
