using GameOfLife.Constants;
using GameOfLife.Enums;
using GameOfLife.SaveGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Grid
{  
    public class FieldManager : IFieldManager
    {
        IApplication _application;
        public FieldManager(IApplication application)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
        }
        public void CheckCellsForSurvival(IField field)
        {
            field.FutureCells = new bool[field.Dimension, field.Dimension];

            for (int r = 0; r < field.Cells.GetLength(0); r++)
            {
                for (int c = 0; c < field.Cells.GetLength(1); c++)
                {
                    SetFutureState(r, c, field);
                }
            }
        }

        public void PrintField(IField field)
        {
             for (int r = 0; r < field.Cells.GetLength(0); r++)
             {
                 for (int c = 0; c < field.Cells.GetLength(1); c++)
                 {
                    bool isEndOfRow = c == field.Dimension - 1;
                    _application.DrawCell(field.Cells[r, c], isEndOfRow);
                 }
             }
            _application.EmptyLine();
        }

        private void SetFutureState(int row, int column, IField field)
        {
            int aliveNeigbours = CountAliveNeighbours(row, column, field);
            if (aliveNeigbours == 3 || (field.Cells[row, column] && aliveNeigbours == 2))
            {
                field.FutureCells[row, column] = true;
            }
            else
            {
                field.FutureCells[row, column] = false;
            }
        }
        public void UpdateFieldData(IField field)
        {
            for (int r = 0; r < field.Cells.GetLength(0); r++)
            {
                for (int c = 0; c < field.Cells.GetLength(1); c++)
                {
                    field.Cells[r, c] = field.FutureCells[r, c];
                }
            }

            field.Generation++;
        }

        public int CountAliveCells(IField field)
        {
            int liveCellCount = 0;
            for (int r = 0; r < field.Cells.GetLength(0); r++)
            {
                for (int c = 0; c < field.Cells.GetLength(1); c++)
                {
                    if (field.Cells[r, c])
                    {
                        liveCellCount++;
                    }
                }
            }
            return liveCellCount;
        }
        private int CountAliveNeighbours(int r, int c, IField field)
        {
            List<bool> neighbours = GetNeighbours(r, c, field);

            int count = neighbours.Where(n => n == true).Count();
            return count;
        }

        private List<bool> GetNeighbours(int r, int c, IField field)
        {
            List<bool> neighbours = new List<bool>();
            int[,] neighbourCoordinates = new int[8, 2] {
                { r, c - 1 }, //left
                { r, c + 1 }, //right
                { r - 1, c - 1 },  //top left
                { r - 1, c },     // top
                { r - 1, c + 1 },  // top right
                { r + 1, c - 1 }, // bottom left
                { r + 1, c },     // bottom center
                { r + 1, c + 1 }  };  // bottom right

            for (int i = 0; i < neighbourCoordinates.GetLength(0); i++)
            {
                int neighbourRow = neighbourCoordinates[i, 0];
                int neighbourColumn = neighbourCoordinates[i, 1];

                if (neighbourRow >= 0 && neighbourRow < field.Dimension && neighbourColumn >= 0 && neighbourColumn < field.Dimension)
                {
                    neighbours.Add(field.Cells[neighbourRow, neighbourColumn]);
                }
            }
            return neighbours;
        }

    }
}
