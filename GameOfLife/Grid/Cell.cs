using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class Cell : ICell
    {
        public string Id { get; }
        public bool IsAlive { get; set; }
        public bool WillLive { get; set; }
        IApplication _application;
        public Cell(int r, int c, IApplication application)
        {
            Id = $"{r}-{c}";   // for List of monitored cells
            IsAlive = false;
            WillLive = false;
            _application = application;
        }
        public Cell(IApplication application)
        {
            _application = application;
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

        public void UpdateCurrentState()
        {
            IsAlive = WillLive;
        }

        public void DisplayCell()
        {
            _application.DrawCell(IsAlive);
        }
    }
}
