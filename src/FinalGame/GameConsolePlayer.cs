using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    internal class GameConsolePlayer : Player
    {
        GameConsole console;

        public GameConsolePlayer()
        {
            this.console = null;
        }

        public GameConsolePlayer(GameConsole console)
        {
            this.console = console;
        }

        public override void Log(string s)
        {
            if(console != null)
            {
                console.GameConsoleWrite(s);
            }
            else
            {
                base.Log(s);
            }
        }

        public virtual void Log(string DebugKey, string DebugValue)
        {
            //if (console != null)
            //{
            //    console.Log(DebugKey, DebugValue);
            //}
            //else
            //{
            //    base.Log(DebugKey + ":" + DebugValue);
            //}
        }
    }
}
