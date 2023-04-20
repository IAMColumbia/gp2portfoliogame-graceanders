using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    internal class Hotbar
    {
        internal Vector2 Loc;
        internal string Name;
        internal bool Selected;

        public Hotbar(Vector2 loc, string name) 
        { 
            this.Loc = loc;
            this.Name = name;
        }
    }
}
