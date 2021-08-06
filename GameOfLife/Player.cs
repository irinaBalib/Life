using GameOfLife.SaveGame;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        IGameStorage _data;

        public Player(IGameStorage data)
        {
            _data = data;
        }
        public bool HasSavedGame()
        {
            
            return _data.DataExists(Name);
        }
    }
}
