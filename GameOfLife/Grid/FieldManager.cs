using GameOfLife.Enums;
using GameOfLife.SaveGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife.Grid
{  
    public class FieldManager : IFieldManager
    {
       
        private IField field;
        IFieldFactory _factory;
        IGameStorage _storage;
        IApplication _application;
        public FieldManager(IFieldFactory factory, IGameStorage storage, IApplication application)
        {
             _factory = factory;
            _storage = storage;
            _application = application;
        }

        public void SetUpField(Option option, int fieldSize, string playerName)
        {
            switch (option)
            {
                case Option.Random:
                    {
                        CreateFromInput(fieldSize);
                        SetRandomInitField();
                        break;
                    }
                case Option.Preset:
                    {
                        CreateFromInput(fieldSize);
                        SetPredefinedInitField();
                        break;
                    }
                case Option.Restore:
                    {
                        CreateFromSaved(playerName);
                        break;
                    }
            }
        }
        public void CreateFromInput(int size)
        {
            field = _factory.Create(size);
        }
        public void CreateFromSaved(string playerName)
        {
            IField restoredField = _storage.Restore(playerName);
            field = _factory.Create(restoredField.Dimension, restoredField.CurrentCells, restoredField.Generation);
        }
        public void ViewField()  //TODO: naming
        {
            for (int r = 0; r < field.CurrentCells.GetLength(0); r++)
            {
                for (int c = 0; c < field.CurrentCells.GetLength(1); c++)
                {
                    bool isEndOfRow = c == field.Dimension - 1;
                    _application.DrawCell(field.CurrentCells[r, c], isEndOfRow);
                    SetFutureState(r, c);
                }
            }
        }

        public void SetFutureState(int row, int column)
        {
            int aliveNeigbours = CountAliveNeighbours(row, column);
            if (aliveNeigbours == 3 || (field.CurrentCells[row, column] && aliveNeigbours == 2))
            {
                field.FutureCells[row, column] = true;
            }
            else
            {
                field.FutureCells[row, column] = false;
            }
        }
        public int CountAliveNeighbours(int r, int c)
        {
            List<bool> neighbours = GetNeighbours(r, c);

            int count = neighbours.Where(n => n == true).Count();
            return count;
        }

        public List<bool> GetNeighbours(int r, int c)
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
                    neighbours.Add(field.CurrentCells[neighbourRow, neighbourColumn]);
                }
            }
            return neighbours;
        }

        public void UpdateFieldData()
        {
            for (int r = 0; r < field.CurrentCells.GetLength(0); r++)
            {
                for (int c = 0; c < field.CurrentCells.GetLength(1); c++)
                {
                    field.CurrentCells[r, c] = field.FutureCells[r, c];
                }
            }

            field.Generation++;
        }


        public int CountAliveCells()
        {
            int liveCellCount = 0;
            for (int r = 0; r < field.CurrentCells.GetLength(0); r++)
            {
                for (int c = 0; c < field.CurrentCells.GetLength(1); c++)
                {
                    if (field.CurrentCells[r, c])
                    {
                        liveCellCount++;
                    }
                }
            }
            return liveCellCount;
        }

        private void SetRandomInitField()
        {
            var random = new Random();

            for (int r = 0; r < field.CurrentCells.GetLength(0); r++)
            {
                for (int c = 0; c < field.CurrentCells.GetLength(1); c++)
                {
                    field.CurrentCells[r, c] = random.Next(2) == 1;
                }
            }
        }

        private void SetPredefinedInitField()
        {

            field.CurrentCells[0, 10] = true; // "Glider"
            field.CurrentCells[1, 8] = true;
            field.CurrentCells[1, 10] = true;
            field.CurrentCells[2, 9] = true;
            field.CurrentCells[2, 10] = true;

            field.CurrentCells[5, 5] = true; //"0+"
            field.CurrentCells[6, 4] = true;
            field.CurrentCells[6, 5] = true;
            field.CurrentCells[6, 6] = true;

            field.CurrentCells[1, 0] = true; // "Blinker" at the edge
            field.CurrentCells[2, 0] = true;
            field.CurrentCells[3, 0] = true;
        }

        public int GetGeneration()
        {
            return field.Generation;
        }

        public IField GetField()
        {
            return field;
        }
    }
}
