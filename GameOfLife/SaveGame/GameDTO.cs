using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.SaveGame
{
    public class GameDTO
    {
        public int Generation { get; set; }
        public int Dimension { get; set; }
        public List<FieldDTO> FieldDTOs {get;set;}
        public List<int> PrintedFieldIndexes { get; set; }
    }
}
