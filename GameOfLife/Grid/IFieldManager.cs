using GameOfLife.Enums;
using System.Collections.Generic;

namespace GameOfLife.Grid
{
    public interface IFieldManager
    {
        int CountAliveCells();
        void SetUpField(Option option, int fieldSize, string playerName);
        void UpdateFieldData();
        void PrintCurrentSetFuture();
        int GetGeneration();
        IField GetField();
    }
}