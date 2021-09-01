using GameOfLife.Constants;
using GameOfLife.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Grid
{
    public class FieldFactory : IFieldFactory
    {
        public IField BuildRandomField(int fieldSize)
        {
            IField randomField = Create(fieldSize);
            FillRandomly(randomField);
            return randomField;
        }
        public IField BuildPresetField(int fieldSize)
        {
            IField presetField = Create(fieldSize);
            SetPredefined(presetField);
            return presetField;
        }
        public IField Create(int size)
        {
            if (size < NumericData.FieldMinSize || size > NumericData.FieldMaxSize)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            IField field = new SquareField()
                {
                    Dimension = size,
                    Generation = 0,
                    Index = 1,
                    Cells = new bool[size, size]
                }; 

            return field;
        }
        private void FillRandomly(IField field)
        {
            var random = new Random();

            for (int r = 0; r < field.Cells.GetLength(0); r++)
            {
                for (int c = 0; c < field.Cells.GetLength(1); c++)
                {
                    field.Cells[r, c] = random.Next(2) == 1;
                }
            }
        }
        private void SetPredefined(IField field)
        {
            field.Cells[0, 10] = true; // "Glider"
            field.Cells[1, 8] = true;
            field.Cells[1, 10] = true;
            field.Cells[2, 9] = true;
            field.Cells[2, 10] = true;

            field.Cells[5, 5] = true; //"0+"
            field.Cells[6, 4] = true;
            field.Cells[6, 5] = true;
            field.Cells[6, 6] = true;

            field.Cells[1, 0] = true; // "Blinker" at the edge
            field.Cells[2, 0] = true;
            field.Cells[3, 0] = true;
        }

    }
}
