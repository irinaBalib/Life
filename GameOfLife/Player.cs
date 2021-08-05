using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        IDataStorage _data;

        public Player(IDataStorage data)
        {
            _data = data;
        }
        public bool HasSavedGame()
        {
            
            return _data.DataExists(Name);
        }
    }
}
