using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Crops
{
    internal class FreePlot : Plant
    {
        public FreePlot(Game game) : base(game) 
        {
            this.Name = "FreePlot";
            this.Scale = 2;
            this.DayOneTextureName = "Crops/Free_Plot";
        }
    }
}
