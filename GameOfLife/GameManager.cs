using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class GameManager
    {
        public Field Generation { get; set; }

        //public Field NextGeneration { get; set; }

        public GameManager()
        {
        }

        public Field CreateField(int d)
        {
            Generation = new Field(d);
            FillField();
            return Generation;
        }

        public void FillField()
        {
            Generation.Cells = new Cell[Generation.Height, Generation.Width];
            for (int r = 0; r < Generation.Cells.GetLength(0); r++)
            {
                for (int c = 0; c < Generation.Cells.GetLength(1); c++)
                {
                    Generation.Cells[r, c] = new Cell();
                }
            }
        }

        public void SetInitState()
        {

        }
        public void ViewField()
        {
            for (int r = 0; r < Generation.Cells.GetLength(0); r++)
            {
                for (int c = 0; c < Generation.Cells.GetLength(1); c++)
                {
                    if (!Generation.Cells[r, c].IsAlive)
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write("O");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }

        public void UpdateField()
        {

            throw new NotImplementedException();
        }
    }
}
