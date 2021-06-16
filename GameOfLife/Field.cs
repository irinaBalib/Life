using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Field
    { 
        public int Height { get; set; }
        public int Width { get; set; }
        public Cell[,] Cells { get; set; }
        public Field(int d)
        {
            Height = d;
            Width = d;
        }

     
    }
}
