using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class ConsoleApplication : IApplication
    {
        public void WriteText(string text)
        {
            Console.WriteLine(text);
        }

        public string ReadInput()
        {
            var input = Console.ReadLine();
            return input;
        }

        public void ShowErrorMessage(string message)
        {
            ClearLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
          //  Console.Write(new string(' ', Console.WindowWidth-message.Length));
            ReturnCursor();
            Console.ResetColor();
        }
        private static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
        private static void ReturnCursor()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

    }
}
