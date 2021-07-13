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

        public Cell(int r, int c)  
        {
            Id = $"{r}-{c}";   // for List of monitored cells
            IsAlive = false;
            WillLive = false;
        }
        
        public void SetFutureState(int aliveNeigbours) 
        {
            if (IsAlive && (aliveNeigbours == 2 || aliveNeigbours == 3))
            {
                WillLive = true;
            }
            else if (!IsAlive && aliveNeigbours == 3)
            {
                WillLive = true;
            }
            else
            {
                WillLive = false;
            }
        }

        public void UpdateCurrentState()   // is Alive = willLive
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
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[_]");
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[_]");
                Console.ResetColor();
              
            }
        }
    }
}
