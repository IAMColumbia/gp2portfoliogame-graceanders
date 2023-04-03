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
    //enum TerrainType { Grass, Water, Soil, Sand }

    internal class Terrain
    {
        internal Texture2D Texture { get; set; }
        public TerrainType Type { get; set; }

        public Terrain() { }
        public Terrain(Texture2D texture, TerrainType type) 
        { 
            this.Texture = texture;
            this.Type = type;
        }

        //int Speed;//do I want to have an int or float that will adjust float speed depending on terrain?

    }
}
