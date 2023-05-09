using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame.Managers
{
    internal class PlantManager
    {
        public List<GridSquare> GridBoard;

        public PlantManager(List<GridSquare> gridBoard)
        {
            //Needs to be able to know quards, there are both terrian blocks, and blocks that go on top of those
            GridBoard = gridBoard;
        }
    }
}
