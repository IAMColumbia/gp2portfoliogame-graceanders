using MonoGameLibrary.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    internal class GridTerrain : Sprite
    {
        protected Terrain terrain;

        public List<Terrain> Terrains = new List<Terrain>();

        protected string GrassTextureName, WaterTextureName, SoilTextureName, SandTextureName;
        protected Texture2D GrassTexture, WaterTexture, SoilTexture, SandTexture;

        Terrain Grass, Water, Soil, Sand;

        private TerrainType terrainType;
        public TerrainType TerrainType
        {
            get { return this.terrain.Type; }
            set { this.terrain.Type = value; }
        }


        public GridTerrain(Game game) : base(game)
        {
            this.terrain = new Terrain();
            Grass = new Terrain();
            GrassTextureName = "Terrain_Grass";
            Water = new Terrain();
            WaterTextureName = "Terrain_Water";
            Soil = new Terrain();
            SoilTextureName = "Terrain_Soil";
            Sand = new Terrain();
            SandTextureName = "Terrain_Sand";
        }

        protected virtual void GetTerrainTexture()
        {
            this.terrainType = this.terrain.Type;
            switch(terrain.Type)
            {
                case TerrainType.Grass:
                    this.spriteTexture = GrassTexture;
                    break; 
                case TerrainType.Water:
                    this.spriteTexture = WaterTexture;
                    break;
                case TerrainType.Soil:
                    this.spriteTexture = SoilTexture;
                    break;
                case TerrainType.Sand:
                    this.spriteTexture = SandTexture;
                    break;

            }
        }

        protected override void LoadContent()
        {
            this.GrassTexture = this.Game.Content.Load<Texture2D>(GrassTextureName);
            Grass.Type = TerrainType.Grass;
            Terrains.Add(Grass);
            this.WaterTexture = this.Game.Content.Load<Texture2D>(WaterTextureName);
            Water.Type = TerrainType.Water;
            Terrains.Add(Water);
            this.SoilTexture = this.Game.Content.Load<Texture2D>(SoilTextureName);
            Soil.Type = TerrainType.Soil;
            Terrains.Add(Soil);
            this.SandTexture = this.Game.Content.Load<Texture2D>(SandTextureName);
            Sand.Type = TerrainType.Sand;
            Terrains.Add(Sand);

            GetTerrainTexture();

            base.LoadContent();
        }
    }

}
