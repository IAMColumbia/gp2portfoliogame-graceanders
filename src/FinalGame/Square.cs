using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public enum SquareState { Grid, Terrain }

    public class Square
    {
        public Square() { }


        public void PlayerOnGrid(GridSquare square) { square.Occupied(); }
        public void PlayerOffGrid(GridSquare square) { square.Free(); }
        public void GridClicked(GridSquare square) { square.Interacted(); }
    }
}
