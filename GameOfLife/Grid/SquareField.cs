using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class SquareField : IField
    {
        public int Dimension { get; set; }
        public bool[,] CurrentCells { get; set; }
        private bool[,] FutureCells { get; set; }
        public int Generation { get; set; }
        IApplication _application; //sep
     
        public SquareField(IApplication application)
        {
            _application = application;
        }

        public void Create(int size)
        {
            Dimension = size;
            Generation = 0;
            CurrentCells = new bool[Dimension, Dimension];
            FutureCells = new bool[Dimension, Dimension];
        }
        public void Create(int size, bool[,] cells, int generation)
        {
             Dimension = size;
            CurrentCells = cells;
            FutureCells = new bool[Dimension, Dimension];
            Generation = generation;
        }

        public void ViewField()  //TODO: naming
        {
            for (int r = 0; r < CurrentCells.GetLength(0); r++)
            {
                for (int c = 0; c < CurrentCells.GetLength(1); c++)
                {
                    bool isEndOfRow = c == Dimension - 1;
                    _application.DrawCell(CurrentCells[r, c], isEndOfRow);
                     SetFutureState(r,c);
                }
            }
        }

        public void SetFutureState( int row, int column)  
        {
            int aliveNeigbours = CountAliveNeighbours(row, column);
           if (aliveNeigbours == 3 || (CurrentCells[row,column] && aliveNeigbours == 2))
            {
                FutureCells[row, column] = true;
            }
            else
            {
                FutureCells[row, column] = false;
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
                int neighbourRow = neighbourCoordinates[i,0];
                int neighbourColumn = neighbourCoordinates[i, 1];

                if (neighbourRow >= 0 && neighbourRow < Dimension && neighbourColumn >= 0 && neighbourColumn < Dimension)
                {
                    neighbours.Add(CurrentCells[neighbourRow, neighbourColumn]);
                }
            }
            return neighbours;
        }

        public void UpdateFieldData()
        {
            for (int r = 0; r < CurrentCells.GetLength(0); r++)
            {
                for (int c = 0; c < CurrentCells.GetLength(1); c++)
                {
                    CurrentCells[r, c] = FutureCells[r, c];
                }
            }
           
            Generation++;
        }


        public int CountAliveCells()
        {
            int liveCellCount = 0;
            for (int r = 0; r < CurrentCells.GetLength(0); r++)
            {
                for (int c = 0; c < CurrentCells.GetLength(1); c++)
                {
                    if (CurrentCells[r, c])
                    {
                        liveCellCount++;
                    }
                }
            }
            return liveCellCount;
        }

        public void SetRandomInitField()
        {
            var random = new Random();

            for (int r = 0; r < CurrentCells.GetLength(0); r++)
            {
                for (int c = 0; c < CurrentCells.GetLength(1); c++)
                {
                    CurrentCells[r, c] = random.Next(2) == 1;
                }
            }
        }

        public void SetPredefinedInitField()
        {

            CurrentCells[0, 10] = true; // "Glider"
            CurrentCells[1, 8] = true;
            CurrentCells[1, 10] = true;
            CurrentCells[2, 9] = true;
            CurrentCells[2, 10] = true;

            CurrentCells[5, 5] = true; //"0+"
            CurrentCells[6, 4] = true;
            CurrentCells[6, 5] = true;
            CurrentCells[6, 6] = true;

            CurrentCells[1, 0] = true; // "Blinker" at the edge
            CurrentCells[2, 0] = true;
            CurrentCells[3, 0] = true;

            #region  shapes not used
            //CurrentCells[10, 10] = true; //"0+"
            //CurrentCells[11, 9] = true;
            //CurrentCells[11, 10] = true;
            //CurrentCells[11, 11] = true;

            //CurrentCells[0, 0] = true; // "Blinker"
            //CurrentCells[0, 1] = true;
            //CurrentCells[0, 2] = true;

            //CurrentCells[2, 2] = true; // "Blinker"
            //CurrentCells[2, 3] = true;
            //CurrentCells[2, 4] = true;

            //CurrentCells[0, 10] = true; // "Glider"
            //CurrentCells[1, 8] = true;
            //CurrentCells[1, 10] = true;
            //CurrentCells[2, 9] = true;
            //CurrentCells[2, 10] = true;
            #endregion
        }

    }

}