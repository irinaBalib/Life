using GameOfLife.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Input
{
    public class PlayerInput
    {
        public string PlayerName { get; set; }
        public int FieldSize { get; set; }
        public Option StartOption { get; set; }
    }
}
