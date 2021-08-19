using GameOfLife.Enums;
using System.Collections.Generic;

namespace GameOfLife.Grid
{
    public interface IFieldManager
    {
     
        void CheckCellsForSurvival(IField field);
        void PrintCells(IField field, int index);
        void UpdateFieldData(IField field);
        int CountAliveCells(IField field);
        
    }
}