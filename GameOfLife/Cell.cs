using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Cell
    {
        public string Id { get; }
        public bool IsAlive { get; set; }
        public bool WillLive { get; set; }

        //public int Row { get; set; }
        //public int Column { get; set; }

        public Cell(int r, int c)
        {
            Id = $"{r}-{c}"; // for List of monitored cells
            IsAlive = false;
            WillLive = false;
        }
        public void SetFutureState(int neighbours)
        {
            if (IsAlive)
            {
                if (neighbours < 2 || neighbours > 3)
                {
                    WillLive = false;
                }
                else 
                {
                    WillLive = true;
                }
            }
            else if (!IsAlive)
            {
                if ( neighbours == 3)
                {
                    WillLive = true;
                }
                else
                {
                    WillLive = false;
                }
            }
           
        }

        public void UpdateCurrentState()
        {
            if (!WillLive)
            {
                IsAlive = false;
            }
            else if (WillLive)
            {
                IsAlive = true;
            }
        }

        public void DisplayCell()
        {
            if (IsAlive)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("__");
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("__");
                Console.ResetColor();
              
            }
        }

        
    }
}
