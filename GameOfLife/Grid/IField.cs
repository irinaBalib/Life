using System.Collections.Generic;

namespace GameOfLife
{
    public interface IField
    {
        bool[,] CurrentCells { get; set; }
        bool[,] FutureCells { get; set; }
        int Generation { get; set; }
        int Dimension { get; set; }

    }
}