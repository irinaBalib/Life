using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Field
    { 
        public int Height { get;}  
        public int Width { get; }
        public Cell[,] Cells { get; set; }
        public Field(int d)
        {
            Height = d;
            Width = d;
        }

        public void FillField()
        {
            Cells = new Cell[Height,Width];
            for (int r = 0; r < Cells.GetLength(0); r++)
            {
                for (int c = 0; c < Cells.GetLength(1); c++)
                {
                    Cells[r, c] = new Cell(r, c);
                }
            }
        }

        public void ViewField()
        {
            for (int r = 0; r < Cells.GetLength(0); r++)
            {
                for (int c = 0; c < Cells.GetLength(1); c++)
                {
                    Cells[r, c].DisplayCell();
                    Cells[r, c].SetFutureState(CountAliveNeighbours(r, c));
                }
                Console.WriteLine();
            }
        }

        public int CountAliveNeighbours(int r, int c)
        {
            int count = 0;
            List<Cell> neighbours = GetNeighbours(r, c);
            foreach (Cell n in neighbours) 
            {
                if (n.IsAlive)
                {
                    count++;
                }
            }

            return count;
        }

        public List<Cell> GetNeighbours(int r, int c)
        {
            List<Cell> allCells = Cells.Cast<Cell>().ToList();
            List<Cell> neighbours = new List<Cell>();
            List<string> neighboursIDs = new List<string>();

            neighboursIDs.Add($"{r}-{c - 1}"); // left
            neighboursIDs.Add($"{r}-{c + 1}"); //right
            neighboursIDs.Add($"{r - 1}-{c - 1}"); // top left
            neighboursIDs.Add($"{r - 1}-{c}");  // top center
            neighboursIDs.Add($"{r - 1}-{c + 1}"); //top right
            neighboursIDs.Add($"{r + 1}-{c - 1}"); // bottom left
            neighboursIDs.Add($"{r + 1}-{c}");  // bottom center
            neighboursIDs.Add($"{r + 1}-{c + 1}"); //bottom right
 
            foreach (string id in neighboursIDs)
            {
                Cell neighbour = allCells.FirstOrDefault(c => c.Id == id);
                if (neighbour != null)
                {
                    neighbours.Add(neighbour);
                }
            }

            if (neighbours.Count == 0)
            {
                Console.WriteLine("Error: couldn't locate neighbours");
            }
          
            return neighbours;
        }

        public void UpdateFieldData()
        {
            foreach (Cell c in Cells)
            {
                if (c.IsAlive != c.WillLive)
                {
                    c.UpdateCurrentState();
                    c.DisplayCell();
                }
            }
        }
        public bool HasAliveCells()
        { 
            foreach (var cell in Cells)
            {
                if (cell.IsAlive)
                {
                    return true;
                }
            }
            return false;
        }
     
    }
}
