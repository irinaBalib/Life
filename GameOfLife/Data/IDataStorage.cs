using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
   public interface IDataStorage
    {
        void Save(string playername, Field field);
        Field Restore(string playername);
    }
}
