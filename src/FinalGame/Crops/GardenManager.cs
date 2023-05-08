using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using FinalGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalGame.Crops
{
    internal class GardenManager : DrawableGameComponent
    {
        internal List<Plant> Garden;// May have all plants in garden drawn, but not visible
        internal List<Plant> AllPlants;
        Plant Beet, Corn, Garlic, Grapes, GreenBean, Melon, Potato, Radish, Strawberry, Tomato, FreePlot;
        

        bool testing;

        internal Vector2 GardenPlotOne, GardenPlotTwo, GardenPlotThree, GardenPlotFour;

        public GardenManager(Game game) : base(game)
        {
            Garden = new List<Plant>(4) { };

            testing = true;
        }

        int Yloc = 480;
        protected override void LoadContent()
        {
            GardenPlotOne = new Vector2(550, Yloc);
            GardenPlotTwo = new Vector2(850, Yloc);
            GardenPlotThree = new Vector2(1160, Yloc);
            GardenPlotFour = new Vector2(1450, Yloc);

            LoadPlants();
            base.LoadContent();
            
        }

        private void LoadPlants()
        {
            Beet = new Beet(this.Game);            //0
            Corn = new Corn(this.Game);            //1
            Garlic = new Garlic(this.Game);        //2
            Grapes = new Grapes(this.Game);        //3
            GreenBean = new GreenBean(this.Game);  //4
            Melon = new Melon(this.Game);          //5
            Potato = new Potato(this.Game);        //6
            Radish = new Radish(this.Game);        //7
            Strawberry = new Strawberry(this.Game);//8
            Tomato = new Tomato(this.Game);        //9

            FreePlot = new FreePlot(this.Game);

            AllPlants = new List<Plant>() { Beet, Corn, Garlic, Grapes, GreenBean, Melon, Potato, Radish, Strawberry , Tomato};

            if (testing) 
            {
                Garden.Add(Potato);
                Garden.Add(Beet);
                Garden.Add(Corn);
                Garden.Add(Garlic);

                SetPlantLocation();
            }

        }

        internal void UpdatePlantState(Plant plant)
        {
            switch (plant.PS)
            {
                case PlantState.Alive:
                    break;
                case PlantState.Dead:
                    plant.DrawColor = Color.Transparent;
                    break;
                case PlantState.Harvested:
                    plant.DrawColor = Color.Transparent;
                    plant = FreePlot;
                    break;
            }

        }

        internal void UpdatePlantQuality()
        {
            foreach(Plant plant in AllPlants)
            {
                foreach(Plant GP in Garden)
                {
                    if(GP == plant)
                    {
                        if(plant.PlantQuality < GP.PlantQuality) { plant.PlantQuality = GP.PlantQuality; }
                        break;
                    }
                }
            }
        }

        internal void SetPlantLocation()
        {
            Garden[0].Location = GardenPlotOne;
            Garden[1].Location = GardenPlotTwo;
            Garden[2].Location = GardenPlotThree;
            Garden[3].Location = GardenPlotFour;

            foreach(Plant plant in Garden)//Tall Plants
            {
                if(plant == Corn || plant == Grapes || plant == GreenBean)
                {
                    plant.Location.Y = 430;
                }
            }
        }


        public void AddPlant(Plant plant)
        {
            Garden.Add(plant);
        }

        public void GrowPlants()
        {
            foreach (Plant plant in Garden)
            {
                if (plant.PS == PlantState.Harvested || plant.Harvestable == true)
                {
                    continue;
                }

                if (plant.Watered)
                {
                    plant.Grow();
                }
                else
                {
                    if (plant.DaysUnwatered >= 2) { 
                        plant.PS = PlantState.Dead;
                        plant.DrawColor = Color.Transparent;
                    }
                    else{ 
                        plant.DaysUnwatered++;
                        plant.DrawColor = Color.Olive;
                    }
                }
                plant.UpdatePlantDay();
            }
        }

        public List<Plant> ReturnAllPlants()
        {
            if( AllPlants == null ) { return null; }
            return AllPlants;
        }

        public void ResetPlant(Plant plant)
        {
            plant.PlantDay = 0;
            plant.PS = PlantState.Alive;
            plant.DrawColor = Color.White;
            plant.Harvestable = false;
            plant.DaysUnwatered = 0;
            this.SetPlantLocation();

            plant.UpdatePlantDay();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Plant plant in this.Garden)
            {
                plant.Update(gameTime);
                plant.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
