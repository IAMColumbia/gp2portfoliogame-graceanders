﻿using Microsoft.Xna.Framework;
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
        public Corn(Game game) : base(game)
        {
            this.Name = "Corn";
            this.Worth = 50;
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

    }
}
