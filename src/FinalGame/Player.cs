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
        public int gold;
        private int Gold
        {
            get { return this.gold; }
            set { this.gold = value; }
        }

        public Player() { }

        public Player(List<Item> inventory, int gold)
        {
            Inventory = inventory;
            Gold = gold;
        }

        public virtual void Log(string s) 
        { 
            Console.WriteLine(s);
        }

        public void AddItem(Item item)
        {
            Inventory.Add(item);
        }

        public bool HasItem(Item item, int quantity = 1)
        {
            return Inventory.Count(i => i == item) >= quantity;
        }

        public void RemoveItem(Item item) 
        {
            Inventory.Remove(item);
        }
    }
}
