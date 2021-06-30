using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class GameManager
    {
        public Field GameField { get; set; }

        public GameManager()
        {
        }

        public Field CreateField(int d)
        {
            GameField = new Field(d);
            GameField.FillField();
            return GameField;
        }

        public void SetInitState(int optionInput)
        {
            if (optionInput == 1)
            {
                GameField.SetRandomInitField();
            }
            else if (optionInput == 2)
            {
                GameField.SetPredefinedInitField();
            }
            else
            {
                Console.WriteLine("Option not found!");
            }
        }

        public void PutGameOn()
        {
            int g = 0;
            
            do
                {
                while (!Console.KeyAvailable)
                {
                    Console.WriteLine("Generation {0}", g);
                    GameField.ViewField();

                    Thread.Sleep(1000);
                    Console.ReadLine();
                    GameField.UpdateFieldData();
                    Console.SetCursorPosition(0, 1);
                    g++;
                }

                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    break;
                }
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
