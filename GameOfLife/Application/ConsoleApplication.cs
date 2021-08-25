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
            Console.Write(text);
        }

        public void Rewrite(string text)
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

        public void PrintFields(List<IField> fieldsToPrint)
        {
            List<IField> fieldsTemp = new List<IField>();    // TODO: to improve
            fieldsToPrint.ForEach(field => fieldsTemp.Add(field));

            var fieldHeight = fieldsTemp.FirstOrDefault().Dimension;
            var columnCount = NumericData.ColumnCount;
            var rowCount = fieldsTemp.Count / NumericData.ColumnCount; 
            if (fieldsTemp.Count % NumericData.ColumnCount > 0) // for 1field, or for fields qty not fully dividable to columns 
            {
                rowCount++;
            }

                for (int row = 0; row < rowCount; row++)
                {
                    for (int column = 0; column < columnCount; column++)
                    {
                        var cursorLeft = 0;
                        var cursorTop = Console.CursorTop;
                       
                        if (column > 0)
                        {
                            cursorLeft = Console.WindowWidth / NumericData.ColumnCount * column;
                            cursorTop = Console.CursorTop - fieldHeight - 1;
                        }

                        PrintField(fieldsTemp[0], cursorTop, cursorLeft);
                        fieldsTemp.RemoveAt(0);

                        if (fieldsTemp.Count == 0)
                        {
                            columnCount = column;
                            break;
                        }
                    }
                    Console.WriteLine();  
                }
        }
        private void PrintField(IField field, int cursorTop, int cursorLeft) 
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            
            Console.WriteLine("Field #{0}", field.Index);
            cursorTop++;
            
            for (int r = 0; r < field.Cells.GetLength(0); r++)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);
                
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
                cursorTop++;
               Console.WriteLine();
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
