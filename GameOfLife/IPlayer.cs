namespace GameOfLife
{
    public interface IPlayer
    {
        string Name { get; set; }

        bool HasSavedGame();
    }
}