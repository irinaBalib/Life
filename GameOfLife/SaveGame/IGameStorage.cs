using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.SaveGame
{
   public interface IGameStorage
    {
        void Save(string playername, IField field);
        IField Restore(string playername);
        bool DataExists(string playername);
    }
}
