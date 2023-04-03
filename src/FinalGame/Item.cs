using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    internal class Item : DrawableSprite
    {
        private string name;
        private int worth;

        public string Name 
        { get { return name; } set { name = value; } }
        public int Worth
        { get { return worth; } set { worth = value; } }

        public Item(Game game) : base(game) { }

        //public Item(string name, int worth)
        //{
        //    Name = name;
        //    Worth = worth;
        //}

        public Item(Game game, string name, int worth) : base(game)
        {
            Name = name;
            Worth = worth;
        }
    }
}
