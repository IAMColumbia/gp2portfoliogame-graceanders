﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Crops
{
    internal class Radish : Plant
    {
        public Radish(Game game) : base(game)
        {
            this.Name = "Radish";
            this.Worth = 90;
            this.Scale = 2;
            SetTextureNames();

            Initialize();
        }

        internal void SetTextureNames()
        {
            this.DayOneTextureName = "Crops/Radish_Stage_1";
            this.DayTwoTextureName = "Crops/Radish_Stage_2";
            this.DayThreeTextureName = "Crops/Radish_Stage_3";
            this.DayFourTextureName = "Crops/Radish_Stage_3";
            this.DayFiveTextureName = "Crops/Radish_Stage_4";
            this.DaySixTextureName = "Crops/Radish_Stage_5";
        }
    }
}
