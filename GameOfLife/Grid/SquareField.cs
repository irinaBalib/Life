using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class SquareField : IField
    {
        //static int minDimension = 15;
        //static int maxDimension = 40;
        public int Dimension { get; set; }
        // public ICell[,] Cells { get; set; }
        public bool[,] CurrentCells { get; set; }
        private bool[,] FutureCells { get; set; }
        public int Generation { get; set; }
        IApplication _application;
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

            for (int r = 0; r < CurrentCells.GetLength(0); r++)
            {
                for (int c = 0; c < CurrentCells.GetLength(1); c++)
                {
                    CurrentCells[r, c] = false;
                    FutureCells[r, c] = false;
                }
            }
            
            //for (int r = 0; r < Cells.GetLength(0); r++)
            //{
            //    for (int c = 0; c < Cells.GetLength(1); c++)
            //    {
            //       // Cells[r, c] = new Cell(r, c, new ConsoleApplication());  // TO IMPLEMENT !
            //    }
            //}

        }

        public void ViewField()
        {
            for (int r = 0; r < CurrentCells.GetLength(0); r++)
            {
                for (int c = 0; c < CurrentCells.GetLength(1); c++)
                {
                    //CurrentCells[r, c].DisplayCell();
                    //CurrentCells[r, c].SetFutureState(CountAliveNeighbours(r, c));
                    _application.DrawCell(CurrentCells[r, c]);

                    #region testing
                    //if (r == 1 && c == 0)
                    //{
                    //    Console.Write(CurrentCells[r, c]);
                    //    Console.WriteLine(CurrentCells[r - 1, c]);
                    //    Console.WriteLine(CurrentCells[r + 1, c]);
                    //    Console.ReadLine();
                    //}
                    #endregion

                    //  FutureCells[r, c] = GetFutureState(CurrentCells[r, c], CountAliveNeighbours(r,c));  !!slows down
                    SetFutureState(r,c);
                }
                Console.WriteLine();              //how to remove this console method?
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

            #region testin
            //if (row == 1 && column == 0)
            //{
            //    Console.WriteLine(FutureCells[row, column]);
            //    Console.ReadLine();
            //}
            #endregion
        }
        public int CountAliveNeighbours(int r, int c)
        {
            List<bool> neighbours = GetNeighbours(r, c);

            #region testing
            //if (r == 1 && c == 0)
            //{
            //    foreach (var item in neighbours)
            //    {
            //        Console.WriteLine(item);
            //    }
            //}
            //Console.ReadLine();
            #endregion

            int count = neighbours.Where(n => n == true).Count();
            return count;
        }

        public /*List<Cell>*/  List<bool> GetNeighbours(int r, int c)
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
                //try
                //    {
                //    neighbours.Add(CurrentCells[neighbourRow, neighbourColumn]);
                //    }
                //    catch (Exception)
                //    {}
                   
            }
            //neighbours.Add(CurrentCells[r, c - 1]); // left
            //neighbours.Add(CurrentCells[r, c + 1]); //right
            //neighbours.Add(CurrentCells[r - 1, c - 1]); // top left
            //neighbours.Add(CurrentCells[r - 1, c]);  // top center
            //neighbours.Add(CurrentCells[r - 1, c + 1]); //top right
            //neighbours.Add(CurrentCells[r + 1, c - 1]); // bottom left
            //neighbours.Add(CurrentCells[r + 1, c]);  // bottom center
            //neighbours.Add(CurrentCells[r + 1, c + 1]); //bottom right

            //List<Cell> allCells = CurrentCells.Cast<Cell>().ToList(); 
            //List<Cell> neighbours = new List<Cell>();
            //List<string> neighboursIDs = new List<string>();

            //neighboursIDs.Add($"{r}-{c - 1}"); // left
            //neighboursIDs.Add($"{r}-{c + 1}"); //right
            //neighboursIDs.Add($"{r - 1}-{c - 1}"); // top left
            //neighboursIDs.Add($"{r - 1}-{c}");  // top center
            //neighboursIDs.Add($"{r - 1}-{c + 1}"); //top right
            //neighboursIDs.Add($"{r + 1}-{c - 1}"); // bottom left
            //neighboursIDs.Add($"{r + 1}-{c}");  // bottom center
            //neighboursIDs.Add($"{r + 1}-{c + 1}"); //bottom right

            //foreach (string id in neighboursIDs)
            //{
            //    Cell neighbour = allCells.FirstOrDefault(c => c.Id == id);
            //    if (neighbour != null)
            //    {
            //        neighbours.Add(neighbour);
            //    }
            //}

            return neighbours;
        }

        public void UpdateFieldData()
        {
            //for (int r = 0; r < CurrentCells.GetLength(0); r++)
            //{
            //    for (int c = 0; c < CurrentCells.GetLength(1); c++)
            //    {
            //        CurrentCells[r, c]= FutureCells[r, c];
            //    }
            //}
           CurrentCells = FutureCells;
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
                    //CurrentCells[r, c].IsAlive = random.Next(2) == 1;
                    CurrentCells[r, c] = random.Next(2) == 1;
                }
            }
        }

        public void SetPredefinedInitField()
        {

            //CurrentCells[0, 10] = true; // "Glider"
            //CurrentCells[1, 8] = true;
            //CurrentCells[1, 10] = true;
            //CurrentCells[2, 9] = true;
            //CurrentCells[2, 10] = true;

            //CurrentCells[10, 10] = true; //"0+"
            //CurrentCells[11, 9] = true;
            //CurrentCells[11, 10] = true;
            //CurrentCells[11, 11] = true;

            CurrentCells[0, 0] = true; // "Blinker" at the edge
            CurrentCells[1, 0] = true;
            CurrentCells[2, 0] = true;

            //Cells[10, 10].IsAlive = true; //"0+"
            //Cells[11, 9].IsAlive = true;
            //Cells[11, 10].IsAlive = true;
            //Cells[11, 11].IsAlive = true;

            //CurrentCells[0, 0].IsAlive = true; // "Blinker"
            //CurrentCells[0, 1].IsAlive = true;
            //CurrentCells[0, 2].IsAlive = true;

            //Cells[2, 2].IsAlive = true; // "Blinker"
            //Cells[2, 3].IsAlive = true;
            //Cells[2, 4].IsAlive = true;



            //CurrentCells[0, 10].IsAlive = true; // "Glider"
            //CurrentCells[1, 8].IsAlive = true;
            //CurrentCells[1, 10].IsAlive = true;
            //CurrentCells[2, 9].IsAlive = true;
            //CurrentCells[2, 10].IsAlive = true;

            
        }

    }

}