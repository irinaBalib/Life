namespace GameOfLife.Grid
{
    public interface IFieldFactory
    {
        IField Create(int size);
        IField Create(int size, bool[,] cells, int generation);
    }
}