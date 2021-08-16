using GameOfLife.Enums;

namespace GameOfLife.Grid
{
    public interface IGridManager
    {
        void SetGridContent(Option option, int fieldSize, string playerName);
        int CountAliveCells();
        void SaveGridData(string playerName);
        int GetGeneration();
        void LoopGridData();
        void UpdateGridData();
    }
}