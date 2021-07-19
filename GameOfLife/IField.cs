using System.Collections.Generic;

namespace GameOfLife
{
    public interface IField
    {
        Cell[,] Cells { get; set; }
        int Generation { get; set; }
        int Height { get; set; }
        int Width { get; set; }

        int CountAliveCells();
        int CountAliveNeighbours(int r, int c);
        void FillField();
        List<Cell> GetNeighbours(int r, int c);
        void SetPredefinedInitField();
        void SetRandomInitField();
        void UpdateFieldData();
        void ViewField();
    }
}