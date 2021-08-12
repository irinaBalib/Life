using GameOfLife.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Grid
{
    public class FieldFactory : IFieldFactory
    {
        IField _field;

        public FieldFactory(IField field)
        {
            _field = field ?? throw new ArgumentNullException(nameof(field));
        }

        public IField Build(Option option, int fieldSize)
        {
            switch (option)
            {
                case Option.Random:
                    {
                        Create(fieldSize);
                        SetRandomInitField();
                        break;
                    }
                case Option.Preset:
                    {
                        Create(fieldSize);
                        SetPredefinedInitField();
                        break;
                    }
            }
            return _field;
        }

        public IField BuildFromRestored(IField restoredField)
        {
            Create(restoredField.Dimension, restoredField.CurrentCells, restoredField.Generation);
            return _field;
        }

        private void Create(int size)
        {
            _field.Dimension = size;
            _field.Generation = 0;
            _field.CurrentCells = new bool[_field.Dimension, _field.Dimension];
            _field.FutureCells = new bool[_field.Dimension, _field.Dimension];
        }

        private void Create(int size, bool[,] cells, int generation)
        {
            _field.Dimension = size;
            _field.Generation = generation;
            _field.CurrentCells = cells;
            _field.FutureCells = new bool[_field.Dimension, _field.Dimension];
        }

        private void SetRandomInitField()
        {
            var random = new Random();

            for (int r = 0; r < _field.CurrentCells.GetLength(0); r++)
            {
                for (int c = 0; c < _field.CurrentCells.GetLength(1); c++)
                {
                    _field.CurrentCells[r, c] = random.Next(2) == 1;
                }
            }
        }

        private void SetPredefinedInitField()
        {
            _field.CurrentCells[0, 10] = true; // "Glider"
            _field.CurrentCells[1, 8] = true;
            _field.CurrentCells[1, 10] = true;
            _field.CurrentCells[2, 9] = true;
            _field.CurrentCells[2, 10] = true;

            _field.CurrentCells[5, 5] = true; //"0+"
            _field.CurrentCells[6, 4] = true;
            _field.CurrentCells[6, 5] = true;
            _field.CurrentCells[6, 6] = true;

            _field.CurrentCells[1, 0] = true; // "Blinker" at the edge
            _field.CurrentCells[2, 0] = true;
            _field.CurrentCells[3, 0] = true;
        }

    }
}
