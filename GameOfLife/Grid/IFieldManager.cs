using GameOfLife.Enums;
using System.Collections.Generic;

namespace GameOfLife.Grid
{
    public interface IFieldManager
    {
     
        void CheckCellsForSurvival(IField field);
        void PrintField(IField field);
        void UpdateFieldData(IField field);
        int CountAliveCells(IField field);
        
    }
}