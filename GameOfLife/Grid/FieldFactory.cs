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

        public IField Create(int size)
        {
            _field.Dimension = size;
            _field.Generation = 0;
            _field.CurrentCells = new bool[_field.Dimension, _field.Dimension];
            _field.FutureCells = new bool[_field.Dimension, _field.Dimension];

            return _field;
        }

        public IField Create(int size, bool[,] cells, int generation)
        {
            _field.Dimension = size;
            _field.Generation = generation;
            _field.CurrentCells = cells;
            _field.FutureCells = new bool[_field.Dimension, _field.Dimension];

            return _field;
        }

    }
}
