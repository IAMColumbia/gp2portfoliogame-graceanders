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
    internal class GardenManager : DrawableGameComponent
    {
        internal List<Plant> Garden;// May have all plants in garden drawn, but not visible
        List<Plant> AllPlants;
        Plant Beet, Corn, Garlic, Grapes, GreenBean, Melon, Potato, Radish, Strawberry, Tomato;
        

        bool testing;

        int GardenPlotOneX, GardenPlotTwoX, GardenPlotThreeX, GardenPlotFourX;

        public GardenManager(Game game) : base(game)
        {
            Garden = new List<Plant>(4) { };

            testing = true;
        }

        protected override void LoadContent()
        {
            GardenPlotOneX = 550;
            GardenPlotTwoX = 850;
            GardenPlotThreeX = 1160;
            GardenPlotFourX = 1450;

            base.LoadContent();
            LoadPlants();
        }

        private void LoadPlants()
        {
            Beet = new Beet(this.Game, GardenPlotTwoX); 
            Corn = new Corn(this.Game, GardenPlotThreeX);
            Garlic = new Garlic(this.Game, GardenPlotFourX);
            Grapes = new Grapes(this.Game, GardenPlotOneX);
            GreenBean = new GreenBean(this.Game, GardenPlotOneX);
            Melon = new Melon(this.Game, GardenPlotOneX);
            Potato = new Potato(this.Game, GardenPlotOneX);
            Radish = new Radish(this.Game, GardenPlotOneX);
            Strawberry = new Strawberry(this.Game, GardenPlotOneX);
            Tomato = new Tomato(this.Game, GardenPlotOneX);

            AllPlants = new List<Plant>() { Beet, Corn, Garlic, Grapes, GreenBean, Melon, Potato, Radish, Strawberry , Tomato};

            if (testing) 
            {
                Garden.Add(Potato);
                Garden.Add(Beet);
                Garden.Add(Corn);
                Garden.Add(Garlic);

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
                    break;
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
