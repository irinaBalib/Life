namespace GameOfLife
{
    public interface IApplication
    {
        void WriteText(string text);
        string ReadInput();
        void ShowErrorMessage(string message);
        void SetCursor(int fieldIndex, int fieldSize);
        void UpdateCursor(int rowOfField);
        void DrawCell(bool isAlive, bool isEndOfRow);
        void ShowFieldInfoBar(int generation, int liveCellCount, int liveFieldCount, string message = "");
        void ShowPreExitScreen();
        void ClearScreen();
        void NextLine();
    }
}