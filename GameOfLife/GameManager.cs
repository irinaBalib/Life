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
            GameField.Cells[2,10].IsAlive = true;
            GameField.Cells[3,9].IsAlive = true;
            GameField.Cells[3,10].IsAlive = true;
            GameField.Cells[3,11].IsAlive = true;
        }
        public void PutGameOn()
        {
            
            do
            {
              GameField.ViewField();
                Thread.Sleep(1000);
                GameField.UpdateFieldData();
                //Console.Clear();
                Console.WriteLine();
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
