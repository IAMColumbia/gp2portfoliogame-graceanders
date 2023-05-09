using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Crops
{
    internal class Grapes : Plant
    {
        public Grapes(Game game) : base(game)
        {
            this.Name = "Grapes";
            this.Worth = 80;
            this.Scale = 2;
            SetTextureNames();

            Initialize();
        }

        internal void SetTextureNames()
        {
            this.DayOneTextureName = "Crops/Grape_Stage_1";
            this.DayTwoTextureName = "Crops/Grape_Stage_2";
            this.DayThreeTextureName = "Crops/Grape_Stage_3";
            this.DayFourTextureName = "Crops/Grape_Stage_4";
            this.DayFiveTextureName = "Crops/Grape_Stage_5";
            this.DaySixTextureName = "Crops/Grape_Stage_6";
        }

    }
}
