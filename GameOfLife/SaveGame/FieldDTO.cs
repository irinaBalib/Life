using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.SaveGame
{
    public class FieldDTO
    {
        public bool[,] Cells { get; set; }
        public int Index { get; set; }
    }
}
