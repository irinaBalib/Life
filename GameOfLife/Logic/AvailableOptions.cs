using GameOfLife.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife.Logic
{
    public static class AvailableOptions
    {
        public static List<Option> ListOfOptions = Enum.GetValues(typeof(Option)).Cast<Option>().ToList();
        public static void ExcludeOption(Option optionToExclude) 
        {
            //ListOfOptions.Remove(option => option == optionToExclude);
        }

        
    }
}
