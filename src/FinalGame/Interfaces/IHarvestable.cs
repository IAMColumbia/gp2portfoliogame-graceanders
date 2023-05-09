﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Interfaces
{
    internal interface IHarvestable
    {
        bool Harvestable { get; set; }
        void Harvest();
    }
}
