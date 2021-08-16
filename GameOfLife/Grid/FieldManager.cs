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
       
        private IField field;
        private List<IField> ListOfFields;
        IFieldFactory _factory;
        IGameStorage _storage;
        IApplication _application;
        public FieldManager(IFieldFactory factory, IGameStorage storage, IApplication application)
        {
             _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public void SetUpField(Option option, int fieldSize, string playerName)
        {
            if (option == Option.Restore)
            {
                field = _factory.BuildFromRestored(GetRestoredField(playerName));
            }
            else if (option == Option.Multiple)
            {
                for (int i = 0; i < NumericData.FieldCount; i++)
                {
                    field = _factory.Build(option, fieldSize);
                    ListOfFields.Add(field);
                }
            }
            else
            {
                field = _factory.Build(option, fieldSize);
            }
        }
       
        public void PrintCurrentSetFuture()  
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

        public async void PrintCurrentSetFutureAsync()
        {
            ParallelLoopResult result = Parallel.ForEach<IField>(ListOfFields, PrintCurrentSetFuture);
            
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

        public int GetGeneration()
        {
            return field.Generation;
        }

        public IField GetField()
        {
            return field;
        }

        private IField GetRestoredField(string playerName)
        {
            return _storage.Restore(playerName);
        }

        private void SetFutureState(int row, int column)
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

        private int CountAliveNeighbours(int r, int c)
        {
            List<bool> neighbours = GetNeighbours(r, c);

            int count = neighbours.Where(n => n == true).Count();
            return count;
        }

        private List<bool> GetNeighbours(int r, int c)
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

    }
}
