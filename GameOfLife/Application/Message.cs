using GameOfLife.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Application
{
   public class Message
    {
        public const string Welcome = "Welcome to the Game of Life!\n\n" +
                "PLAYER'S SETUP\n";
        public const string AskName = "Player's name:";
        
        public readonly string AskStartOption = $"Please choose game field set up for 0.Generation ({(int)Option.RANDOM} - for randomly filled, {(int)Option.PRESET} - pre-set, {(int)Option.RESTORE} - restore saved game): ";

        public readonly string AskFieldSize = $"Please input the size of the field({IField.MinSize} - {IField.MaxSize} cells): ";

        public const string BlankName = "Name is required!";
        public const string InvalidInput = "Invalid input!";
        public const string OutOfRange = "Size is out of range!";
        public const string NoSavedGames = "No saved games found for this Player!";

        public const string InfoBar1Line = "|Controls|  ESC - exit  | SPACEBAR - pause |";
       
        public const string Extinction = "xxxxxxxxxx  TOTAL EXTINCTION  xxxxxxxxxxxxxxxxxxxxxx ";
        public const string GameEnded = " ~~~~~~~~~~~     Game ended by the Player! ~~~~~~~~~~~";
        public const string Paused = "**PAUSED** Press SPACEBAR to resume or F12 to save & exit";
        public const string GameOver = "GAME OVER \n";
        public const string NewGame = "Press ENTER to start a new game";

        public string InfoBar2Line(int generation, int liveCellCount)
        { 
            return $" Generation {generation} \t Live cells count: {liveCellCount}"; 
        }

        public string GameSaved(string name)
        {
            return $"~~~~~~~~~~~     Game for Player {name} saved. ~~~~~~~~~~~";
        }
    }
}
