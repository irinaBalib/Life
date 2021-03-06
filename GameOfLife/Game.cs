using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
      public class Game : IGame
    {
        IGameManager _gameManager;

        public Game(IGameManager gameManager)
        {
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }

        public void Run()
        {
            _gameManager.RunTheGame(); // x1k
        }
    }
}
