namespace GameOfLife
{
    public interface IApplication
    {
        void WriteText(string text);
        string ReadInput();
        void ShowErrorMessage(string message);
    }
}