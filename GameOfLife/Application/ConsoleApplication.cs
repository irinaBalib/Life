using GameOfLife.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class ConsoleApplication : IApplication
    {
        public Message Message { get; set; }
        public void WriteText(string text)
        {
            Console.WriteLine(text);
        }

        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void ShowErrorMessage(string message)
        {
            ClearLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.Write(new string(' ', Console.WindowWidth-message.Length));
            ReturnCursor();
            Console.ResetColor();
        }

        public void ShowFieldInfoBar(int generation, int liveCellCount, string message)
        {
            Message = new Message();
            string firstLine = Message.InfoBar1Line;
            string secondLine = Message.InfoBar2Line(generation, liveCellCount); 

            if (!string.IsNullOrEmpty(message))
            {
                firstLine = message;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(firstLine);
            Console.WriteLine(new string(' ', Console.WindowWidth - firstLine.Length));
            Console.ResetColor();
            Console.SetCursorPosition(0, 1);
            Console.Write(secondLine);
            Console.WriteLine(new string(' ', Console.WindowWidth - secondLine.Length));
            Console.ResetColor();
            Console.SetCursorPosition(0, 3);
        }

        public void ShowPreExitScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 2);
            Console.WriteLine(Message.GameOver);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 9, Console.WindowHeight / 2);
            Console.WriteLine(Message.NewGame);
        }
        public void DrawCell(bool isAlive, bool isEndOfRow)
        {
           if (isAlive)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[_]");
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[_]");
                Console.ResetColor();
            }

            if (isEndOfRow)
            {
                Console.WriteLine();
            }
        }

        public void ClearScreen()
        {
            Console.Clear();
        }
        private void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
        private void ReturnCursor()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }



    }
}
