using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalGame.Interfaces;

namespace FinalGame.Crops
{
    enum Quality
    {
        Poor, Acceptable, Decent, Excellent
    }

    enum PlantState
    {
        Unplanted, Alive, Dead, Harvested
    }

    internal class Plants : Item, IHarvestable, IWaterable, IGrowable
    {
        Quality PlantQuality;
        internal PlantState PS;
        int Count;
        bool AchievedExelence;

        public Plants(string name, int worth) : base(name, worth) 
        { 
            this.Name = name;
            this.Worth = worth;
        }

        public bool Watered { get; set; }
        public int DaysUnwatered { get; set; }
        public int DaysToGrow { get; set; }
        public int DaysGrowing { get; set; }
        public bool Harvested { get; set; }

        public void Grow()
        {
            throw new NotImplementedException();
        }

        public void Harvest(Plants plant)
        {
            throw new NotImplementedException();
        }

        public void Water(Plants plant)
        {
            throw new NotImplementedException();
        }
    }    
}
