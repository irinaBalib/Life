using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Cell
    {
        public bool IsAlive { get; set; }
        public bool WillSurvive { get; set; }

        //public int Row { get; set; }
        //public int Column { get; set; }

        public Cell()
        {
            IsAlive = false;
            WillSurvive = false;
        }
    }
}
