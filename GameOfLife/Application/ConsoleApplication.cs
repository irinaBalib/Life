using GameOfLife.Application;
using GameOfLife.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class ConsoleApplication : IApplication
    {
        public void WriteText(string text)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.WriteLine(text);
        }

        public void Write(string text)
        {
            ClearLine();
            Console.Write(text);
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

        public void ShowFieldInfoBar(int generation, int liveCellCount, int liveFieldCount, string message)
        {
            string firstLine = TextMessages.InfoBar1Line;
            string secondLine = $" Generation {generation} \t Live cells count: {liveCellCount} \t Live field count: {liveFieldCount}";
               

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

        public void ClearScreen()
        {
            Console.Clear();
        }
        public void ShowPreExitScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 2);
            Console.WriteLine(TextMessages.GameOver);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 9, Console.WindowHeight / 2);
            Console.WriteLine(TextMessages.NewGame);
        }

        public void PrintFields(List<IField> fields)
        {
            List<IField> fieldsTemp = new List<IField>();
            fields.ForEach(field => fieldsTemp.Add(field));
            
            do
            {
                for (int row = 0; row < fieldsTemp.Count / NumericData.ColumnCount; row++)
                {
                    for (int column = 0; column < NumericData.ColumnCount; column++)
                    {
                        var fieldHeight = fieldsTemp.FirstOrDefault().Dimension;
                        var cursorLeft = 0;
                        var cursorTop = Console.CursorTop;
                       
                        if (column > 0)
                        {
                            cursorLeft = Console.WindowWidth / NumericData.ColumnCount * column;
                            cursorTop = Console.CursorTop - fieldHeight;
                        }
                        if (row > 0)
                        {
                            cursorTop = Console.CursorTop + (fieldHeight * row)+1;
                        }

                        PrintField(fieldsTemp[0], cursorTop, cursorLeft);
                        fieldsTemp.RemoveAt(0);
                    }
                    Console.WriteLine();
                }   
            } while (fieldsTemp.Count>0);
            
        }

        private void PrintField(IField field, int cursorTop, int cursorLeft) 
        {
            for (int r = 0; r < field.Cells.GetLength(0); r++)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);
                if (r==0)
                {
                    Console.WriteLine("Field #{0}", field.Index); 
                }
                for (int c = 0; c < field.Cells.GetLength(1); c++)
                {
                  // PrintCell(field.Cells[r, c]);  //do I need a separate method

                    if (field.Cells[r, c] == true)
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
                }
                Console.WriteLine();
                cursorTop++;
            }
        }
        //private void PrintCell(bool isAlive)
        //{
        //    if (isAlive)
        //    {
        //        Console.BackgroundColor = ConsoleColor.DarkCyan;
        //        Console.ForegroundColor = ConsoleColor.Black;
        //        Console.Write("[_]");
        //        Console.ResetColor();
        //    }
        //    else
        //    {
        //        Console.BackgroundColor = ConsoleColor.Gray;
        //        Console.ForegroundColor = ConsoleColor.Black;
        //        Console.Write("[_]");
        //        Console.ResetColor();
        //    }
        //}
        
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
