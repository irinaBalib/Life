using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
   public interface IDataStorage
    {
        void Save(string playername, IField field);
        IField Restore(string playername);
        bool DataExists(string playername);
    }
}
