using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.SaveGame
{
    public class DTO
    {
        public int Generation { get; set; }
        public int Dimension { get; set; }
        public List<FieldDTO> FieldDTOs {get;set;}
    }
}
