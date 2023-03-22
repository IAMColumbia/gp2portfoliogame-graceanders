using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Interfaces
{
    internal interface IGrowable
    {
        int DaysToGrow { get; set; }
        int DaysGrowing { get; set; }

        void Grow();
    }
}
