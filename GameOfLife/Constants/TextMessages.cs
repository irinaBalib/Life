using GameOfLife.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Constants
{
   public class TextMessages
    {
        public const string Welcome = "Welcome to the Game of Life!\n\n" +
                "PLAYER'S SETUP\n";
        public const string AskName = "Player's name: ";
        
         public readonly string AskFieldSize = $"Please input the size of the field({NumericData.FieldMinSize} - {NumericData.FieldMaxSize} cells): ";

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

        public string AskStartOption(bool hasSavedGame)   // TODO: move to  other cs
        {
            string output = "Please choose game field set up for 0.Generation ";
            if (hasSavedGame)
            {
                output += $"({ (int)Option.Random} - for randomly filled, { (int)Option.Preset} -pre-set, { (int)Option.Restore} - restore saved game): ";
            }
            else
            {
                output += $"({ (int)Option.Random} - for randomly filled, { (int)Option.Preset} -pre-set): ";
            }
            return output;
        }
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
