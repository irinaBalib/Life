using System.Collections.Generic;

namespace GameOfLife
{
    public interface IApplication
    {
        void WriteText(string text);
        void Write(string text);
        string ReadInput();
        void ShowErrorMessage(string message);
        void PrintFields(List<IField> fields);
        void ShowFieldInfoBar(int generation, int liveCellCount, int liveFieldCount, string message = "");
        void ShowPreExitScreen();
        void ClearScreen();
    }
}