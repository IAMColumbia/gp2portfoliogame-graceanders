using System;
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
        Unknown, Poor, Acceptable, Decent, Excellent
    }

    enum PlantState
    {
        Alive, Dead, Harvested
    }

    enum PlantDay 
    { 
        DayOne, DayTwo, DayThree, DayFour, DayFive, DaySix
    }

    internal class Plant : Item, IHarvestable, IWaterable, IGrowable
    {
        private Quality plantQuality;
        internal Quality PlantQuality
        {
            get { return this.plantQuality; }
            set { this.plantQuality = value; }
        }

        internal PlantState PS;
        private PlantDay plantDay;
        public PlantDay PlantDay
        {
            get { return this.plantDay; }
            set { this.plantDay = value; }
        }

        internal Texture2D DayOneTexture, DayTwoTexture, DayThreeTexture, DayFourTexture, DayFiveTexture, DaySixTexture;
        internal string DayOneTextureName, DayTwoTextureName, DayThreeTextureName, DayFourTextureName, DayFiveTextureName, DaySixTextureName;

        internal bool AchievedExcellence;

        public Plant(Game game) : base(game) 
        {
            this.plantDay = PlantDay.DayOne;
            this.PS = PlantState.Alive;
            this.Harvestable = false;

            ItemType = ItemType.Plant;
        }

        protected override void LoadContent()
        {
            this.DayOneTexture = this.Game.Content.Load<Texture2D>(this.DayOneTextureName);
            this.DayTwoTexture = this.Game.Content.Load<Texture2D>(this.DayTwoTextureName);
            this.DayThreeTexture = this.Game.Content.Load<Texture2D>(this.DayThreeTextureName);
            this.DayFourTexture = this.Game.Content.Load<Texture2D>(this.DayFourTextureName);
            this.DayFiveTexture = this.Game.Content.Load<Texture2D>(this.DayFiveTextureName);
            this.DaySixTexture = this.Game.Content.Load<Texture2D>(this.DaySixTextureName);

            this.ItemTexture = this.Game.Content.Load<Texture2D>(this.DaySixTextureName);

            base.LoadContent();

            this.UpdatePlantDay();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        internal void UpdatePlantDay()
        {
            this.plantDay = this.PlantDay;
            switch (this.PlantDay)
            {
                case PlantDay.DayOne:
                    this.spriteTexture = this.DayOneTexture;
                    break;
                case PlantDay.DayTwo:
                    this.spriteTexture = this.DayTwoTexture;
                    break;
                case PlantDay.DayThree:
                    this.spriteTexture = this.DayThreeTexture;
                    break;
                case PlantDay.DayFour:
                    this.spriteTexture = this.DayFourTexture;
                    break;
                case PlantDay.DayFive:
                    this.spriteTexture = this.DayFiveTexture;
                    break;
                case PlantDay.DaySix:
                    this.spriteTexture = this.DaySixTexture;
                    this.Harvestable = true;
                    if(this.PlantQuality == Quality.Unknown) { CalculateQuality();}
                    break;
            }
        }

        public bool Watered { get; set; }
        public int DaysUnwatered { get; set; }
        public bool Harvestable { get; set; }

        int i;
        public void Grow()
        {
            this.plantDay++;
        }

        public Plant Harvest()
        {
            return this;
        }

        public void Water()
        {
            this.Watered = true;
        }

        internal void CalculateQuality()
        {
            int qualityRoll = new Random().Next(1, 101);

            if (qualityRoll <= 60) // 60% chance of Poor quality
            {
                this.PlantQuality = Quality.Poor;
            }
            else if (qualityRoll <= 85) // 25% chance of Acceptable quality
            {
                this.PlantQuality = Quality.Acceptable;
                this.Worth += 20;
            }
            else if (qualityRoll <= 95) // 10% chance of Decent quality
            {
                this.PlantQuality = Quality.Decent;
                this.Worth += 20;
            }
            else // 5% chance of Excellent quality
            {
                this.PlantQuality = Quality.Excellent;
                this.Worth += 20;
                this.AchievedExcellence = true;
            }
        }

        //internal Quality ReturnQuality()
        //{
        //    return this.PlantQuality;
        //}
    }    
}
