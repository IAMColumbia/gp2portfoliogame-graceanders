using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Crops
{
    internal class Strawberry : Plant
    {
        public Strawberry(Game game) : base(game)
        {
            this.Name = "Strawberry";
            this.Worth = 120;
            this.Scale = 2;
            SetTextureNames();

            Initialize();
        }

        internal void SetTextureNames()
        {
            this.DayOneTextureName = "Crops/Strawberry_Stage_1";
            this.DayTwoTextureName = "Crops/Strawberry_Stage_2";
            this.DayThreeTextureName = "Crops/Strawberry_Stage_3";
            this.DayFourTextureName = "Crops/Strawberry_Stage_4";
            this.DayFiveTextureName = "Crops/Strawberry_Stage_5";
            this.DaySixTextureName = "Crops/Strawberry_Stage_6";
        }
    }
}
