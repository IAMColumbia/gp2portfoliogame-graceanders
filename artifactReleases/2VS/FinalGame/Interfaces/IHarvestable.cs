using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalGame.Crops;

namespace FinalGame.Interfaces
{
    internal interface IHarvestable
    {
        bool Harvested { get; set; }
        void Harvest(Plants plant);
    }
}
