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
    public enum TerrainType { Grass, Water, Soil, Sand }

    public class GridTerrain : Sprite
    {
        protected string GrassTextureName, WaterTextureName, SoilTextureName, SandTextureName;
        internal Texture2D GrassTexture, WaterTexture, SoilTexture, SandTexture;

        private TerrainType terrainType;
        public TerrainType TerrainType
        {
            get { return terrainType; }
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
            GrassTextureName = "Terrain_Grass";
            WaterTextureName = "Terrain_Water";
            SoilTextureName = "Terrain_Soil";
            SandTextureName = "Terrain_Sand";

            this.TerrainType = TerrainType.Grass;
        }

        public virtual void GetTerrainTexture()
        {
            this.terrainType = this.TerrainType;
            switch (terrainType)
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

        public virtual Texture2D ReturnTexture(int i)
        {
            if (i == 0) { return GrassTexture; }
            if (i == 1) { return WaterTexture; }
            if (i == 2) { return SoilTexture; }
            if (i == 3) { return SandTexture; }
            return null;
        }

        public virtual TerrainType ReturnTerrainType(int i)
        {
            if (i == 0) { return TerrainType.Grass; }
            if (i == 1) { return TerrainType.Water; }
            if (i == 2) { return TerrainType.Soil; }
            if (i == 3) { return TerrainType.Sand; }
            return TerrainType.Grass;
        }

        protected override void LoadContent()
        {
            this.GrassTexture = this.Game.Content.Load<Texture2D>(GrassTextureName);
            this.WaterTexture = this.Game.Content.Load<Texture2D>(WaterTextureName);
            this.SoilTexture = this.Game.Content.Load<Texture2D>(SoilTextureName);
            this.SandTexture = this.Game.Content.Load<Texture2D>(SandTextureName);

            GetTerrainTexture();

            base.LoadContent();
        }
    }

}
