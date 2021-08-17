using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class SquareField : IField
    {
        public int Dimension { get; set; }
        public bool[,] Cells { get; set; }
        public bool[,] FutureCells { get; set; }
        public int Generation { get; set; }
    }

}