using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace GameOfLife
{
     class GameManager  //public or private
    {
        public Field GameField { get; set; }

        public PlayersSetup PlayersSetup { get; private set; }
      

        public void RunTheGame()
        {
            CreatePlayersSetup();

            if (PlayersSetup.PlayersStartOption == 3)// enum?
            {
               RestoreSavedGame();
            }
            else 
            { 
                CreateField();
                SetInitState();
            }

            ShiftFieldGenerations();
            ShowPreExitScreen();
            
        }

        public void CreatePlayersSetup()
        {
            Console.WriteLine("PLAYER'S SETUP\n"); //sep class
            PlayersSetup = new PlayersSetup();
            PlayersSetup.SetPlayersInput();

            Console.Clear();
        }

        public void RestoreSavedGame()
        {
            GameField = new Field();
            GameField.RestoreFieldFromFile(PlayersSetup.PlayersName);
        }

        public void CreateField()
        {
            GameField = new Field(PlayersSetup.PlayersFieldSize);
            GameField.FillField();
        }

        public void SetInitState()
        {
            int optionInput = PlayersSetup.PlayersStartOption;
            if (optionInput == 1)   //enum
            {
                GameField.SetRandomInitField();
            }
            else if (optionInput == 2)
            {
                GameField.SetPredefinedInitField();
            }
            else
            {
                Console.WriteLine("Option not found!");  // to sep class
            }
        }

        public void ShiftFieldGenerations()
        {
            bool canContinue = true;

                while (canContinue)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(" |Controls|  ESC - exit  | SPACEBAR - pause |                                  ");
                    ViewFieldInfo();
                    GameField.ViewField();
                    Thread.Sleep(1000);
                    canContinue = !IsActionRequired();
                    
                    GameField.UpdateFieldData();
                }
        }

        public void ViewFieldInfo()   // remove
        {
            Console.WriteLine(" Generation {0}       Live cells count: {1}", GameField.Generation, GameField.CountAliveCells());
        }
        public bool IsActionRequired()
        {
            if (HasNoAliveCells()) // verify if count == 0 in if()
            {
                return true;
            }
            
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyPressed;
                keyPressed = Console.ReadKey(true);
                Console.SetCursorPosition(0, 0);
                
                if (keyPressed.Key == ConsoleKey.Escape)  //switch case
                {
                    EndGame();
                    return true;
                }
                else if (keyPressed.Key == ConsoleKey.Spacebar)
                {
                    PauseGame(keyPressed);
                }
            }
            return false;
        }

        public bool HasNoAliveCells() //message only
        {
            if (GameField.CountAliveCells() == 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("xxxxxxxxxx  TOTAL EXTINCTION  xxxxxxxxxxxxxxxxxxxxxx ");
                Thread.Sleep(3000);
                Console.ResetColor();
                return true;
            }
            return false;
        }

        public void EndGame()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" ~~~~~~~~~~~     Game ended by the Player! ~~~~~~~~~~~                 ");
            Thread.Sleep(3000);
            Console.ResetColor();
        }
        public void PauseGame(ConsoleKeyInfo keyPressed) //naming
        {
            Console.WriteLine("**PAUSED** Press SPACEBAR to resume or ENTER to save & exit"); //sep class
           
            do
            {
                keyPressed = Console.ReadKey(true);

            } while (keyPressed.Key != ConsoleKey.Enter && keyPressed.Key != ConsoleKey.Spacebar);

            if (keyPressed.Key == ConsoleKey.Enter)
            {
                SaveGame();
            }
        }

        public void SaveGame()
        {
            GameField.WriteToFile(PlayersSetup.PlayersName);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(" ~~~~~~~~~~~     Game for Player {0} saved. ~~~~~~~~~~~          ", PlayersSetup.PlayersName);
            Thread.Sleep(3000);
            ShowPreExitScreen();
        }

        public void ShowPreExitScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth/2, Console.WindowHeight/2-2);
            Console.WriteLine("GAME OVER");
            Console.WriteLine();
            Console.SetCursorPosition(Console.WindowWidth/2-9, Console.WindowHeight / 2);
            Console.WriteLine("Press ENTER to start a new game");
            ConsoleKeyInfo keyPressed;
            keyPressed = Console.ReadKey(true);
            if (keyPressed.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                RunTheGame();
            }
            else
            {
                Console.Clear();
                Environment.Exit(0);
            }
           
        }
    }
}
