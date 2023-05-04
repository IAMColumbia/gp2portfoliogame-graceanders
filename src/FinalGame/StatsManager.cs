using Microsoft.Xna.Framework;
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

        public void OpenStatsWindow()
        {
            if (!isStatsOpen)
            {
                StatsWindow statsWindow = new StatsWindow(Game);
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
                ShopWindow shopWindow = (ShopWindow)Game.Components.FirstOrDefault(c => c is ShopWindow);
                Game.Components.Remove(shopWindow);

                isStatsOpen = false;

                // Resume the game update loop
                Game.IsFixedTimeStep = true;
                Game.IsMouseVisible = true;
            }
        }
    }
}

  

