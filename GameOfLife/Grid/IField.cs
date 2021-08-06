using System.Collections.Generic;

namespace GameOfLife
{
    public interface IField
    {
       bool[,] CurrentCells { get; set; }
        int Generation { get; set; }
        int Dimension { get; set; }

        int CountAliveCells();
        void SetFutureState( int row, int column);
        int CountAliveNeighbours(int r, int c);
       void Create(int dimension);
         void Create(int size, bool[,] cells, int generation);
        List<bool> GetNeighbours(int r, int c);
        void SetPredefinedInitField();
        void SetRandomInitField();
        void UpdateFieldData();
        void ViewField();
    }
}