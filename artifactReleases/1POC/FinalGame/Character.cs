using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    internal class Character : DrawableSprite
    {
        string Name { get; set; }

        public Character(Game game) : base(game) { }

        public Character(Game game, string name) : base(game) 
        {
            this.Name = name;
        }
    }
}
