using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class GameManager
    {
        public Field GameField { get; set; }

        //public Field NextGeneration { get; set; }

        public GameManager()
        {
        }

        public Field CreateField(int d)
        {
            GameField = new Field(d);
            GameField.FillField();
            return GameField;
        }

        
        public void SetInitState()
        {
            GameField.Cells[10,10].IsAlive = true;
            GameField.Cells[11,9].IsAlive = true;
            GameField.Cells[11,10].IsAlive = true;
            GameField.Cells[11,11].IsAlive = true;

            GameField.Cells[2,2].IsAlive = true;
            GameField.Cells[2,3].IsAlive = true;
            GameField.Cells[2,4].IsAlive = true;
        }
        public void PutGameOn()
        {
            int g = 0;
            do
            {
                Console.WriteLine("Generation {0}", g);
              GameField.ViewField();
                Thread.Sleep(1000);
                GameField.UpdateFieldData();
                Console.Clear();
                g++;
            } while (!IsGameOver());
        }
        public bool IsGameOver()
        {
            if (GameField.HasAliveCells())
            {
                return false;
            }
            return true;
        }
    }
}
