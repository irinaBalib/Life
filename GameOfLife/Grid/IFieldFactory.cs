using GameOfLife.Enums;

namespace GameOfLife.Grid
{
    public interface IFieldFactory
    {
        IField Build(Option option, int fieldSize);
        IField BuildFromRestored(IField restoredField);
    }
}