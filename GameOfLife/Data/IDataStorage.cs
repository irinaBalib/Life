using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
   public interface IDataStorage
    {
        void Save(string playername);
        void Restore(string playername);
    }
}
