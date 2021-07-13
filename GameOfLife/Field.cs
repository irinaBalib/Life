﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Field
    {
        public int Height { get; private set; }  //do I need H & W
        public int Width { get; private set; }
        public Cell[,] Cells { get; set; }
        public int Generation { get; set; }
        public Field(int dimension)
        {
            Height = dimension;
            Width = dimension;
        }
        public Field() { }

        public void FillField()
        {
            Cells = new Cell[Height, Width];
           
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
           List<Cell> neighbours = GetNeighbours(r, c);
            int count = neighbours.Where(n => n.IsAlive == true).Count();
           return count;
        }

        public List<Cell> GetNeighbours(int r, int c)
        {
            List<Cell> allCells = Cells.Cast<Cell>().ToList(); //object from array to list
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
            foreach (Cell cell in Cells)
            {
                if (cell.IsAlive != cell.WillLive)
                {
                    cell.UpdateCurrentState();
                }
            }
            Generation ++;
        }

        
        public int CountAliveCells()
        {
            int liveCellCount = 0;
            foreach (var cell in Cells)
            {
                if (cell.IsAlive)
                {
                    liveCellCount++;
                }
            }
            return liveCellCount;
        }

        public void SetRandomInitField()
        {
            var random = new Random();

            for (int r = 0; r < Cells.GetLength(0); r++)
            {
                for (int c = 0; c < Cells.GetLength(1); c++)
                {
                    Cells[r, c].IsAlive = random.Next(2) == 1;
                }
            }
        }

        public void SetPredefinedInitField()
        {
            //Cells[10, 10].IsAlive = true; //"0+"
            //Cells[11, 9].IsAlive = true;
            //Cells[11, 10].IsAlive = true;
            //Cells[11, 11].IsAlive = true;

            Cells[0, 0].IsAlive = true; // "Blinker"
            Cells[0, 1].IsAlive = true;
            Cells[0, 2].IsAlive = true;

            //Cells[2, 2].IsAlive = true; // "Blinker"
            //Cells[2, 3].IsAlive = true;
            //Cells[2, 4].IsAlive = true;

            //Cells[5, 0].IsAlive = true; // "Blinker" at the edge
            //Cells[6, 0].IsAlive = true;
            //Cells[7, 0].IsAlive = true;

            //Cells[0, 10].IsAlive = true; // "Glider"
            //Cells[1, 8].IsAlive = true;
            //Cells[1, 10].IsAlive = true;
            //Cells[2, 9].IsAlive = true;
            //Cells[2, 10].IsAlive = true;
        }

        public void WriteToFile(string playersName) //separate class
        {
            string path = @$"C:\Users\irina.baliberdina\Documents\LifeSaved\{playersName}.dat"; //directory
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                {
                    writer.Write(Height);
                    writer.Write(Width);
                    writer.Write(Generation);
                    foreach (Cell c in Cells)
                    {
                        writer.Write(c.Id);
                        writer.Write(c.IsAlive);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RestoreFieldFromFile(string playersName)
        {
            try
            {
                string path = @$"C:\Users\irina.baliberdina\Documents\LifeSaved\{playersName}.dat";
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    
                    Height = reader.ReadInt32();
                    Width = reader.ReadInt32();
                    Generation = reader.ReadInt32();
                    FillField();
                    while (reader.PeekChar() > -1)
                    {
                        string cellId = reader.ReadString();
                        foreach (Cell c in Cells)
                        {
                            if (c.Id == cellId)
                            {
                                c.IsAlive = reader.ReadBoolean();
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }
    }

}