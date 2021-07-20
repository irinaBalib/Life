namespace GameOfLife
{
    public interface ICell
    {
        string Id { get; }
        bool IsAlive { get; set; }
        bool WillLive { get; set; }

        void DisplayCell();
        void SetFutureState(int aliveNeigbours);
        void UpdateCurrentState();
    }
}