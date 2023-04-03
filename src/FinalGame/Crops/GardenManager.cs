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
        SpriteBatch sb;

        List<Plant> Garden;
        List<Plant> AllPlants;

        Plant Plant,Potato;

        bool testing;

        public GardenManager(Game game) : base(game)
        {
            Garden = new List<Plant> { };
            AllPlants = new List<Plant> { };
            Plant = new Plant(game);

            testing = true;
        }

        public override void Initialize()
        {
            sb = new SpriteBatch(this.Game.GraphicsDevice);
            LoadPlants();
            base.Initialize();
        }

        private void LoadPlants()
        {
            Plant.TextureName = "Crops/Potato_Stage_1";
            Plant.Texture = this.Game.Content.Load<Texture2D>(Plant.TextureName);
            Potato = new Plant(this.Game,"Potato", 0, Plant.Texture);
            Potato.Location = new Vector2 (100,100);
            AllPlants.Add(Potato);

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
            sb.Begin();

            if(testing) 
            {
            
            }

            sb.End();
        }
    }
}
