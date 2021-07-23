using System.Collections.Generic;

namespace GameOfLife
{
    public interface IField
    {
       // ICell[,] Cells { get; set; }
       bool[,] CurrentCells { get; set; }
        int Generation { get; set; }
        int Dimension { get; set; }

        int CountAliveCells();
        //bool GetFutureState(bool isAliveNow, int aliveNeigbours);
        void SetFutureState( int row, int column);
        int CountAliveNeighbours(int r, int c);
       void Create(int dimension);
        // List<Cell> GetNeighbours(int r, int c);

        List<bool> GetNeighbours(int r, int c);
        void SetPredefinedInitField();
        void SetRandomInitField();
        void UpdateFieldData();
        void ViewField();
    }
}