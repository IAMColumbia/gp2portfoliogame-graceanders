using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Crops
{
    internal class Tomato : Plant
    {
        public Tomato(Game game) : base(game)
        {
            this.Name = "Tomato";
            this.Worth = 60;
            this.Scale = 2;
            SetTextureNames();

            Initialize();
        }

        internal void SetTextureNames()
        {
            this.DayOneTextureName = "Crops/Tomato_Stage_1";
            this.DayTwoTextureName = "Crops/Tomato_Stage_2";
            this.DayThreeTextureName = "Crops/Tomato_Stage_3";
            this.DayFourTextureName = "Crops/Tomato_Stage_4";
            this.DayFiveTextureName = "Crops/Tomato_Stage_5";
            this.DaySixTextureName = "Crops/Tomato_Stage_6";
        }
    }
}
