using FinalGame.Windows;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Managers
{
    internal class WinManager : DrawableGameComponent
    {
        private bool isWinOpen;
        internal bool IsWinOpen
        {
            get { return isWinOpen; }
        }

        internal WinManager(Game game): base(game) { }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        internal void OpenWinWindow()
        {
            WinWindow winWindow = new WinWindow(Game);
            Game.Components.Add(winWindow);

            isWinOpen = true;

            // Pause the game update loop
            Game.IsFixedTimeStep = false;
            Game.IsMouseVisible = false;
        }

        internal void CloseWinWindow()
        {
            if(isWinOpen)
            {
                WinWindow winWindow = (WinWindow)Game.Components.FirstOrDefault(c => c is WinWindow);
                Game.Components.Remove(winWindow);

                isWinOpen = false;

                // Resume the game update loop
                Game.IsFixedTimeStep = true;
                Game.IsMouseVisible = true;
            }
        }
    }
}
