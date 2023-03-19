using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public enum PlayerState { Front, Back, Right, Left }

    internal class Player
    {
        protected PlayerState _state;

        public PlayerState State 
        {   
            get { return _state; }
            set
            {
                if(_state != value)
                {
                    //Add if Debug
                    this.Log(string.Format($"{this.ToString()} was: {_state} now {value}"));
                    _state = value;
                }
            }
        
        }

        List<Item> Inventory = new List<Item>();
        int Money;

        public Player() { }

        public Player(List<Item> inventory, int money)
        {
            Inventory = inventory;
            Money = money;
        }

        public virtual void Log(string s) 
        { 
            Console.WriteLine(s);
        }
    }
}
