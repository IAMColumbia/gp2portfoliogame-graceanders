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
    internal class GridTerrain : DrawableSprite
    {
        protected Terrain terrain;

        protected string GrassTextureName, WaterTextureName, SoilTextureName, SandTextureName;
        protected Texture2D GrassTexture, WaterTexture, SoilTexture, SandTexture;

        private TerrainType terrainType;
        public TerrainType TerrainType 
        { 
            get { return this.terrain.Type; } 
            set { this.terrain.Type = value; }
        
        }


        public GridTerrain(Game1 game) : base(game)
        {
            this.terrain = new Terrain();
            GrassTextureName = "Terrain_Grass";
            WaterTextureName = "Terrain_Water";
            SoilTextureName = "Terrain_Soil";
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
            this.WaterTexture = this.Game.Content.Load<Texture2D>(WaterTextureName);
            this.SoilTexture = this.Game.Content.Load<Texture2D>(SoilTextureName);
            this.SandTexture = this.Game.Content.Load<Texture2D>(SandTextureName);
        }
    }

}
