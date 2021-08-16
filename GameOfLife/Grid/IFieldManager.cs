using GameOfLife.Enums;
using System.Collections.Generic;

namespace GameOfLife.Grid
{
    public interface IFieldManager
    {
        int CountAliveCells(IField field);
        IField GetField(Option option, int fieldSize, string playerName);
        void UpdateFieldData(IField field);
        void PrintCurrentSetFuture(IField field);
        int GetGeneration(IField field);
        void SaveField(string playerName, IField field);
    }
}