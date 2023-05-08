using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Crops
{
    internal class Garlic : Plant
    {
        public Garlic(Game game) : base(game)
        {
            this.Name = "Garlic";
            this.Worth = 60;
            this.Scale = 2;
            SetTextureNames();

            Initialize();
        }

        internal void SetTextureNames()
        {
            this.DayOneTextureName = "Crops/Garlic_Stage_1";
            this.DayTwoTextureName = "Crops/Garlic_Stage_2";
            this.DayThreeTextureName = "Crops/Garlic_Stage_3";
            this.DayFourTextureName = "Crops/Garlic_Stage_4";
            this.DayFiveTextureName = "Crops/Garlic_Stage_4";
            this.DaySixTextureName = "Crops/Garlic_Stage_5";
        }
    }
}
