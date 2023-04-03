using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalGame.Crops;

namespace FinalGame.Interfaces
{
    internal interface IWaterable
    {
        bool Watered { get; set; }
        int DaysUnwatered { get; set; }

        void Water(Plant plant);
    }
}
