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
            FillField(Generation);
            return Generation;
        }

        public void FillField(Field f)
        {
            f.Cells = new Cell[f.Height, f.Width];
            for (int r = 0; r < f.Cells.GetLength(0); r++)
            {
                for (int c = 0; c < f.Cells.GetLength(1); c++)
                {
                    f.Cells[r, c] = new Cell();
                }
            }
        }

        public void ViewField(Field f)
        {
            for (int r = 0; r < f.Cells.GetLength(0); r++)
            {
                for (int c = 0; c < f.Cells.GetLength(1); c++)
                {
                    Console.Write(f.Cells[r, c].IsAlive);
                }
                Console.WriteLine();
            }
        }

        public void UpdateField(Field field)
        {

            throw new NotImplementedException();
        }
    }
}
