﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalGame.Crops
{
    enum Quality
    {
        Poor, Acceptable, Decent, Excellent
    }

    enum PlantState//Unplanted and Harvest may not be nessacary
    {
        Unplanted, Alive, Dead, Harvested
    }

    enum PlantDay 
    { 
        DayOne, DayTwo, DayThree, DayFour, DayFive, Harvest
    }

    enum PlantType
    {
        Potato, Melon, GreenBean, Strawberry, Corn, Radish, Tomato, Grapes, Pumpkin, Beet
    }



    internal class Plant : Item, IHarvestable, IWaterable, IGrowable
    {
        internal string TextureName { get; set; }
        Quality PlantQuality;
        internal PlantState PS;
        internal PlantDay Day;

        private PlantType plantType;
        public PlantType PlantType
        {
            get { return this.plantType; }
            set { this.plantType = value; }
        }
        //int Count;
        bool AchievedExelence;

        public Plant(Game game) : base(game) { }

        public Plant(Game game, string name, int worth, PlantType PT) : base(game,name, worth) 
        { 
            this.Name = name;
            this.Worth = worth;
            this.PlantType = PT;

            this.Day = PlantDay.DayOne;
            this.PS = PlantState.Alive;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public bool Watered { get; set; }
        public int DaysUnwatered { get; set; }
        public int DaysToGrow { get; set; }
        public int DaysGrowing { get; set; }
        public bool Harvested { get; set; }

        int i;
        public void Grow()
        {
            i = (int)this.Day;
        }

        public void Harvest(Plant plant)
        {
            throw new NotImplementedException();
        }

        public void Water(Plant plant)
        {
            throw new NotImplementedException();
        }
    }    
}
