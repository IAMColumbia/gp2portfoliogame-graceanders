using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Crops
{
    internal class GreenBean : Plant
    {
        int Yloc = 450;
        public GreenBean(Game game, int Xloc) : base(game)
        {
            this.Name = "Green Bean";
            this.Worth = 40; //Calculate worth later
            this.Location = new Vector2(Xloc, Yloc);
            this.Scale = 2;
            SetTextureNames();

            Initialize();
        }

        internal void SetTextureNames()
        {
            this.DayOneTextureName = "Crops/Green_Bean_Stage_1";
            this.DayTwoTextureName = "Crops/Green_Bean_Stage_2";
            this.DayThreeTextureName = "Crops/Green_Bean_Stage_3";
            this.DayFourTextureName = "Crops/Green_Bean_Stage_4";
            this.DayFiveTextureName = "Crops/Green_Bean_Stage_5";
            this.DaySixTextureName = "Crops/Green_Bean_Stage_6";

            this.SeedTextureName = "Crops/Green_Bean_Seeds";
        }
    }
}
