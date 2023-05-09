using FinalGame.Crops;
using FinalGame.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Managers
{
    public class StatsManager : DrawableGameComponent
    {
        PlayableCharacter PC;
        InputHandler input;

        internal List<Plant> AllPlants;

        private bool isStatsOpen;
        internal bool IsStatsOpen
        {
            get { return isStatsOpen; }
        }

        internal StatsManager(Game game, PlayableCharacter p, InputHandler IH) : base(game)
        {
            PC = p;
            input = IH;

        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        internal void OpenStatsWindow(ref GardenManager gm)
        {

            if (!isStatsOpen)
            {
                StatsWindow statsWindow = new StatsWindow(Game, gm);
                Game.Components.Add(statsWindow);

                isStatsOpen = true;

                // Pause the game update loop
                Game.IsFixedTimeStep = false;
                Game.IsMouseVisible = false;
            }
        }

        public void CloseStatsWindow()
        {
            if (isStatsOpen)
            {
                StatsWindow statsWindow = (StatsWindow)Game.Components.FirstOrDefault(c => c is StatsWindow);
                Game.Components.Remove(statsWindow);

                isStatsOpen = false;

                // Resume the game update loop
                Game.IsFixedTimeStep = true;
                Game.IsMouseVisible = true;
            }
        }
    }
}



