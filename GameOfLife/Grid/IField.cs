using System.Collections.Generic;

namespace GameOfLife
{
    public interface IField
    {
        bool[,] Cells { get; set; }
        bool[,] FutureCells { get; set; }
        int Generation { get; set; }
        int Dimension { get; set; }
        int Index { get; set; }
        bool IsPrinted { get; set; }

    }
}