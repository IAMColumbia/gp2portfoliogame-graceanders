using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    internal class Item
    {
        private string name;
        private int worth;

        public string Name 
        { get { return name; } set { name = value; } }
        public int Worth
        { get { return worth; } set { worth = value; } }

        public Item(string name, int worth)
        {
            Name = name;
            Worth = worth;
        }

    }
}
