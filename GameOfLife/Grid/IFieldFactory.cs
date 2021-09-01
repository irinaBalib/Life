using GameOfLife.Enums;

namespace GameOfLife.Grid
{
    public interface IFieldFactory
    {
        IField BuildRandomField(int fieldSize);
        IField BuildPresetField(int fieldSize);
        IField Create(int fieldSize);
    }
}