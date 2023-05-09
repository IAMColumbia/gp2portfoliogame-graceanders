using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Interfaces
{
    enum FertilizerGrade
    {
        NonFertilized, Basic, Quality, Deluxe
    }

    internal interface IFertilize
    {
        bool Fertilized { get; set; }
        void Fertilize(Item SelctedItem);

        FertilizerGrade FertilizerGrade { get; set; }
    }
}
