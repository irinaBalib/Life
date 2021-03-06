using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.SaveGame
{
   public interface IGameStorage
    {
        void Save(string playername, List<IField> fields, List<int> printedFieldIndexes);
        (List<IField>, List<int>) Restore(string playername);
        bool DataExists(string playername);
    }
}
