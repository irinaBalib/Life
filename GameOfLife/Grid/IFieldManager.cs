using GameOfLife.Enums;
using System.Collections.Generic;

namespace GameOfLife.Grid
{
    public interface IFieldManager
    {
        int CountAliveCells();
        int CountAliveNeighbours(int r, int c);
        void CreateFromInput(int size);
        void CreateFromSaved(string playerName);
        List<bool> GetNeighbours(int r, int c);
        void SetFutureState(int row, int column);
        void SetUpField(Option option, int fieldSize, string playerName);
        void UpdateFieldData();
        void PrintCurrentSetFuture();
        int GetGeneration();
        IField GetField();
    }
}