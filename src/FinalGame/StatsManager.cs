using FinalGame.Crops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class StatsManager : DrawableGameComponent
    {
        Game Game;
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
            Game = game;
            PC = p;
            input = IH;

        }

        protected override void LoadContent()
        {

            base.LoadContent();
        }

        internal void OpenStatsWindow(ref List<Plant> allPlants)
        {
            AllPlants = allPlants;

            if (!isStatsOpen)
            {
                StatsWindow statsWindow = new StatsWindow(Game, AllPlants);
                //StatsWindow statsWindow = new StatsWindow(Game, PC, input);
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

  

