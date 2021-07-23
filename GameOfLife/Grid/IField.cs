﻿using System.Collections.Generic;

namespace GameOfLife
{
    public interface IField
    {
        Cell[,] Cells { get; set; }
        int Generation { get; set; }
        int Dimension { get; set; }

        int CountAliveCells();
        int CountAliveNeighbours(int r, int c);
        void Create(int dimension);
        List<Cell> GetNeighbours(int r, int c);
        void SetPredefinedInitField();
        void SetRandomInitField();
        void UpdateFieldData();
        void ViewField();
    }
}