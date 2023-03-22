using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalGame.Interfaces;

namespace FinalGame.Crops
{
    internal class GardenManager
    {
        List<Plants> Garden = new List<Plants>();

        public void AddPlant(Plants plant)
        {
            Garden.Add(plant);
        }

        public void GrowPlants()
        {
            foreach (Plants plant in Garden)
            {
                if (plant.Watered)
                {
                    plant.Grow();
                }
                else
                {
                    if (plant.DaysUnwatered >= 2) { plant.PS = PlantState.Dead; }
                    else { plant.DaysUnwatered++; }
                }
            }
        }
    }
}
