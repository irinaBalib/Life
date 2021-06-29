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

            GameField.Cells[5,0].IsAlive = true;
            GameField.Cells[6,0].IsAlive = true;
            GameField.Cells[7,0].IsAlive = true;
        }

        public void SetRandomInitField()
        {
            var random = new Random();
            foreach (Cell c in GameField.Cells)
            {
                var randomBool = random.Next(2) == 1; // Next(2) gives 1 or 0
                c.IsAlive = randomBool;
            }

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
                    Console.SetCursorPosition(0, 0);
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
