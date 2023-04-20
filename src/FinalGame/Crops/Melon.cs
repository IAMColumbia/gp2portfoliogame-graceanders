using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Crops
{
    internal class Melon : Plant
    {
        int Yloc = 480;
        public Melon(Game game, int Xloc) : base(game)
        {
            this.Name = "Melon";
            this.Worth = 150;
            this.Location = new Vector2(Xloc, Yloc);
            this.Scale = 2;
            SetTextureNames();

            Initialize();
        }

        internal void SetTextureNames()
        {
            this.DayOneTextureName = "Crops/Melon_Stage_1";
            this.DayTwoTextureName = "Crops/Melon_Stage_2";
            this.DayThreeTextureName = "Crops/Melon_Stage_3";
            this.DayFourTextureName = "Crops/Melon_Stage_4";
            this.DayFiveTextureName = "Crops/Melon_Stage_5";
            this.DaySixTextureName = "Crops/Melon_Stage_6";
        }

    }
}
