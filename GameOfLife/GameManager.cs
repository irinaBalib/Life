using GameOfLife.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    public class GameManager  //public or private
    {
        public SquareField GameField { get; set; }
        public PlayerSetup PlayersSetup { get;  set; }
        public IDataStorage DataStorage { get; set; }

        public GameManager(IDataStorage dataStorage)
        {
            DataStorage = dataStorage;
        }

        public void RunTheGame()
        {
            CreatePlayersSetup();

            if (PlayersSetup.PlayerStartOption == 3)// enum?
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
            PlayersSetup = new PlayerSetup();
            PlayersSetup.SetPlayersInput();

            Console.Clear();
        }

        public void RestoreSavedGame()
        {
            GameField = DataStorage.Restore(PlayersSetup.PlayerName);
        }

        public void CreateField()
        {
            GameField = new SquareField(PlayersSetup.PlayerFieldSize);
            GameField.FillField();
        }

        public void SetInitState()
        {
            int optionInput = PlayersSetup.PlayerStartOption;
            if (optionInput == 1)   //enum
            {
                GameField.SetRandomInitField();
            }
            else if (optionInput == 2)
            {
                GameField.SetPredefinedInitField();
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
                DataStorage.Save(PlayersSetup.PlayerName, GameField);
                ShowPreExitScreen();
            }
        }
        public void ShowPreExitScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth/2, Console.WindowHeight/2-2);
            Console.WriteLine("GAME OVER\n");

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
