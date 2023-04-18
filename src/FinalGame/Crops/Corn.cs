using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Crops
{
    internal class Corn : Plant
    {
        int Yloc = 420;
        public Corn(Game game, int Xloc) : base(game)
        {
            this.Name = "Corn";
            this.Worth = 50;
            this.Location = new Vector2(Xloc, Yloc);
            this.Scale = 2;
            SetTextureNames();

            Initialize();
        }

        internal void SetTextureNames()
        {
            this.DayOneTextureName = "Crops/Corn_Stage_1";
            this.DayTwoTextureName = "Crops/Corn_Stage_2";
            this.DayThreeTextureName = "Crops/Corn_Stage_3";
            this.DayFourTextureName = "Crops/Corn_Stage_4";
            this.DayFiveTextureName = "Crops/Corn_Stage_5";
            this.DaySixTextureName = "Crops/Corn_Stage_6";
        }

        protected override void LoadContent()
        {
            this.DayOneTexture = this.Game.Content.Load<Texture2D>(this.DayOneTextureName);
            this.DayTwoTexture = this.Game.Content.Load<Texture2D>(this.DayTwoTextureName);
            this.DayThreeTexture = this.Game.Content.Load<Texture2D>(this.DayThreeTextureName);
            this.DayFourTexture = this.Game.Content.Load<Texture2D>(this.DayFourTextureName);
            this.DayFiveTexture = this.Game.Content.Load<Texture2D>(this.DayFiveTextureName);
            this.DaySixTexture = this.Game.Content.Load<Texture2D>(this.DaySixTextureName);
            base.LoadContent();

            this.UpdatePlantDay();
        }

    }
}
