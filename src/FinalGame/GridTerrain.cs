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
    enum TerrainType { Grass, Water, Soil, Sand }

    internal class GridTerrain : Sprite
    {
        //protected Terrain terrain;
        protected Square square;

        public List<Terrain> Terrains = new List<Terrain>();

        protected string GrassTextureName, WaterTextureName, SoilTextureName, SandTextureName;
        protected Texture2D GrassTexture, WaterTexture, SoilTexture, SandTexture;

        Terrain Grass, Water, Soil, Sand;

        private TerrainType terrainType;
        public TerrainType TerrainType
        {
            get { return this.terrainType; }
            set { this.terrainType = value; }
        }

        //read this from text file
        //Grass = 0, Water = 1, Soil = 2, Sand = 3
        public int[,] TerrainGuide = new int[9, 17] {
            {0,3,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0},
            {3,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,3,3,0,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,3,0,0,2,2,0,2,2,0,2,2,0,2,2,0},
            {1,3,0,0,0,2,2,0,2,2,0,2,2,0,2,2,0},
            {1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };
        //for this too work I need the grid to always be 17 x 9 the grid builds dynamically on screen size, so how will that affect this?

        public GridTerrain(Game game) : base(game)
        {
            this.square = new Square();
            //Grass = new Terrain();
            GrassTextureName = "Terrain_Grass";
            //Water = new Terrain();
            WaterTextureName = "Terrain_Water";
            //Soil = new Terrain();
            SoilTextureName = "Terrain_Soil";
            //Sand = new Terrain();
            SandTextureName = "Terrain_Sand";
        }

        public virtual void GetTerrainTexture()
        {
            //this.terrainType = this.terrain.Type;
            switch(terrainType)
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

        public Texture2D ReturnTexture(int i)
        {
            if (i == 0) { return GrassTexture; }
            if (i == 1) { return WaterTexture; }
            if (i == 2) { return SoilTexture; }
            if (i == 3) { return SandTexture; }
            return null;
        }

        protected override void LoadContent()
        {
            this.GrassTexture = this.Game.Content.Load<Texture2D>(GrassTextureName);
            Grass = new Terrain(GrassTexture, TerrainType.Grass);
            //Terrains.Add(Grass);
            this.WaterTexture = this.Game.Content.Load<Texture2D>(WaterTextureName);
            Water = new Terrain(WaterTexture, TerrainType.Water);
            Terrains.Add(Water);
            this.SoilTexture = this.Game.Content.Load<Texture2D>(SoilTextureName);
            Soil = new Terrain(SoilTexture, TerrainType.Soil);
            Terrains.Add(Soil);
            this.SandTexture = this.Game.Content.Load<Texture2D>(SandTextureName);
            Sand = new Terrain(SandTexture, TerrainType.Sand);
            Terrains.Add(Sand);

            GetTerrainTexture();
            Terrains.Add(Grass);

            base.LoadContent();
        }
    }

}
