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
        List<Plant> Garden;// May have all plants in garden drawn, but not visible
        List<Plant> AllPlants;

        Plant Potato;

        bool testing;

        protected string PotatoTextureName, MelonTextureName, GreenBeanTextureName, StrawberryTextureName, CornTextureName, RadishTextureName, TomatoTextureName, GrapesTextureName, PumpkinTextureName, BeetTextureName;
        protected Texture2D PotatoTexture, MelonTexture, GreenBeanTexture, StrawberryTexture, CornTexture, RadishTexture, TomatoTexture, GrapesTexture, PumpkinTexture, BeetTexture;


        public GardenManager(Game game) : base(game)
        {
            Garden = new List<Plant>(16) { };
            AllPlants = new List<Plant> { };

            testing = true;
            SetPlantTextureNames();
        }

        private void SetPlantTextureNames()
        {
           PotatoTextureName = "Crops/Potato_Stage_1";
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.PotatoTexture = this.Game.Content.Load<Texture2D>(PotatoTextureName);
            LoadPlants();

            UpdatePlantTexture(Potato);
        }

        private void LoadPlants()
        {
            Potato = new Plant(this.Game, "Potato", 0, PlantType.Potato);
            Potato.Location = new Vector2(100, 100);

            Potato.Initialize();

            AllPlants.Add(Potato);

            if (testing) { Garden.Add(Potato);}

        }

        //public override void Update(GameTime gameTime)
        //{
        //    base.Update(gameTime);
        //}

        protected void UpdatePlantTexture(Plant plant)
        {
            switch (plant.PlantType)
            {
                case PlantType.Potato:
                    plant.spriteTexture = this.PotatoTexture;
                    break;
                case PlantType.Melon:

                    break;
                case PlantType.GreenBean:

                    break;
                case PlantType.Strawberry:

                    break;
                case PlantType.Corn:

                    break;
                case PlantType.Radish:

                    break;
                case PlantType.Tomato:

                    break;
                case PlantType.Grapes:

                    break;
                case PlantType.Beet:

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
                    if (plant.DaysUnwatered >= 2) { plant.PS = PlantState.Dead; }
                    else { plant.DaysUnwatered++; }
                }
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
