using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public enum SquareState { Occupied, Free, Terrain }
    //square

    public class Square
    {
        public SquareState SquareState { get; set; }

        public Square() 
        {
            this.SquareState = SquareState.Free;
        }

        public virtual void Occupied() { this.SquareState = SquareState.Occupied; }
        public virtual void Free() { this.SquareState = SquareState.Free; }

        public virtual void Terrain() { this.SquareState = SquareState.Terrain; }

        public virtual void UpdateBlockState()
        {
            switch(this.SquareState) 
            {
                case SquareState.Occupied:
                    break;
                case SquareState.Free:
                    break;
                case SquareState.Terrain:
                    break;
            }
        }
    }
}
