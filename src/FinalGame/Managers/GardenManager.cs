using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FinalGame.Crops;
using FinalGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalGame.Managers
{
    internal class GardenManager : DrawableGameComponent
    {
        internal List<Plant> Garden;// May have all plants in garden drawn, but not visible
        internal List<Plant> AllPlants;
        Plant Beet, Corn, Garlic, Grapes, GreenBean, Melon, Potato, Radish, Strawberry, Tomato, FreePlot;

        internal int GardenPlotOneX, GardenPlotTwoX, GardenPlotThreeX, GardenPlotFourX;

        int Y = 480;
        Vector2 GardenPlotOne, GardenPlotTwo, GardenPlotThree, GardenPlotFour;

        public GardenManager(Game game) : base(game)
        {
            Garden = new List<Plant>(4) { };
        }

        protected override void LoadContent()
        {
            GardenPlotOneX = 550;
            GardenPlotTwoX = 850;
            GardenPlotThreeX = 1160;
            GardenPlotFourX = 1450;

            GardenPlotOne = new Vector2(GardenPlotOneX, Y);
            GardenPlotTwo = new Vector2(GardenPlotTwoX, Y);
            GardenPlotThree = new Vector2(GardenPlotThreeX, Y);
            GardenPlotFour = new Vector2(GardenPlotFourX, Y);

            LoadPlants();
            base.LoadContent();

        }

        internal void LoadPlants()
        {
            Beet = new Beet(Game);            //0
            Corn = new Corn(Game);            //1
            Garlic = new Garlic(Game);        //2
            Grapes = new Grapes(Game);        //3
            GreenBean = new GreenBean(Game);  //4
            Melon = new Melon(Game);          //5
            Potato = new Potato(Game);        //6
            Radish = new Radish(Game);        //7
            Strawberry = new Strawberry(Game);//8
            Tomato = new Tomato(Game);        //9

            FreePlot = new FreePlot(Game);

            AllPlants = new List<Plant>() { Beet, Corn, Garlic, Grapes, GreenBean, Melon, Potato, Radish, Strawberry, Tomato };

            StartingGarden();

        }

        internal void StartingGarden()
        {
            Garden.Add(Potato);
            Garden.Add(Beet);
            Garden.Add(Corn);
            Garden.Add(Garlic);

            SetPlantLocation();
        }

        internal void UpdatePlantState(Plant plant)
        {
            switch (plant.PS)
            {
                case PlantState.Alive:
                    break;
                case PlantState.Dead:
                    plant.DrawColor = Color.Transparent;
                    plant = FreePlot;
                    break;
                case PlantState.Harvested:
                    plant.DrawColor = Color.Transparent;
                    plant = FreePlot;
                    break;
            }

        }

        internal void UpdatePlantQuality()
        {
            foreach (Plant plant in AllPlants)
            {
                foreach (Plant GP in Garden)
                {
                    if (GP.Name == plant.Name)
                    {
                        if (plant.PlantQuality < GP.PlantQuality) { plant.PlantQuality = GP.PlantQuality; }
                    }
                }
            }
        }


        internal void SetPlantLocation()
        {

            for (int i = 0; i < 4; i++)//Tall Plants
            {
                if (Garden[i].Name == "Corn" || Garden[i].Name == "Grapes" || Garden[i].Name == "Green Bean") { Y = 430; }
                else { Y = 480; }

                if (i == 0)
                {
                    GardenPlotOne.Y = Y;
                    Garden[i].Location = GardenPlotOne;
                }
                if (i == 1)
                {
                    GardenPlotTwo.Y = Y;
                    Garden[i].Location = GardenPlotTwo;
                }
                if (i == 2)
                {
                    GardenPlotThree.Y = Y;
                    Garden[i].Location = GardenPlotThree;
                }
                if (i == 3)
                {
                    GardenPlotFour.Y = Y;
                    Garden[i].Location = GardenPlotFour;
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
                    if (plant.DaysUnwatered >= 2)
                    {
                        plant.PS = PlantState.Dead;
                        plant.DrawColor = Color.Transparent;
                    }
                    else
                    {
                        plant.DaysUnwatered++;
                        plant.DrawColor = Color.Olive;
                    }
                }
                plant.UpdatePlantDay();
            }
        }

        public List<Plant> ReturnAllPlants()
        {
            if (AllPlants == null) { return null; }
            return AllPlants;
        }

        public void ResetPlant(Plant plant)
        {
            plant.PlantDay = 0;
            plant.PS = PlantState.Alive;
            plant.FertilizerGrade = 0;
            plant.DrawColor = Color.White;
            plant.Harvestable = false;
            plant.DaysUnwatered = 0;
            SetPlantLocation();

            plant.UpdatePlantDay();
        }

        Plant Plant;
        public Plant NewPlant(int index, Game game)
        {
            switch (index)
            {
                case 0:
                    Plant = new Beet(game);
                    break;
                case 1:
                    Plant = new Corn(game);
                    break;
                case 2:
                    Plant = new Garlic(game);
                    break;
                case 3:
                    Plant = new Grapes(game);
                    break;
                case 4:
                    Plant = new GreenBean(game);
                    break;
                case 5:
                    Plant = new Melon(game);
                    break;
                case 6:
                    Plant = new Potato(game);
                    break;
                case 7:
                    Plant = new Radish(game);
                    break;
                case 8:
                    Plant = new Strawberry(game);
                    break;
                case 9:
                    Plant = new Tomato(game);
                    break;
            }
            return Plant;
        }

        int num;
        internal int NumOfExcelentPlants()
        {
            num = 0;
            foreach (Plant plant in AllPlants)
            {
                if (plant.PlantQuality == Quality.Excellent)
                    num++;
            }
            return num;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Plant plant in Garden)
            {
                plant.Update(gameTime);
                plant.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
