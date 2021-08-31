using GameOfLife;
using GameOfLife.Grid;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.GridTests
{
    public class FieldFactoryTests
    {
        private readonly FieldFactory _factory;
        public FieldFactoryTests()
        {
            _factory = new FieldFactory();
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void BuildPresetField_ValidParamPassedShouldReturnField(IField expected, int fieldSize)
        {
            var output = _factory.BuildPresetField(fieldSize);
            Assert.Equal(expected, output);
        }
        public static IEnumerable<object[]> TestData()
        {
            int sizeMin = 15;
            IField fieldMin = new SquareField()
            {
                Dimension = sizeMin,
                Generation = 0,
                Index = 1,
                Cells = new bool[sizeMin, sizeMin]
            };

            int sizeMax = 40;
            IField fieldMax = new SquareField()
            {
                Dimension = sizeMax,
                Generation = 0,
                Index = 1,
                Cells = new bool[sizeMax, sizeMax]
            };

            #region fill
            fieldMin.Cells[0, 10] = true; // "Glider"
            fieldMin.Cells[1, 8] = true;
            fieldMin.Cells[1, 10] = true;
            fieldMin.Cells[2, 9] = true;
            fieldMin.Cells[2, 10] = true;

            fieldMin.Cells[5, 5] = true; //"0+"
            fieldMin.Cells[6, 4] = true;
            fieldMin.Cells[6, 5] = true;
            fieldMin.Cells[6, 6] = true;

            fieldMin.Cells[1, 0] = true; // "Blinker" at the edge
            fieldMin.Cells[2, 0] = true;
            fieldMin.Cells[3, 0] = true;

            fieldMax.Cells[0, 10] = true; // "Glider"
            fieldMax.Cells[1, 8] = true;
            fieldMax.Cells[1, 10] = true;
            fieldMax.Cells[2, 9] = true;
            fieldMax.Cells[2, 10] = true;

            fieldMax.Cells[5, 5] = true; //"0+"
            fieldMax.Cells[6, 4] = true;
            fieldMax.Cells[6, 5] = true;
            fieldMax.Cells[6, 6] = true;

            fieldMax.Cells[1, 0] = true; // "Blinker" at the edge
            fieldMax.Cells[2, 0] = true;
            fieldMax.Cells[3, 0] = true;
            #endregion
            yield return new object[] { fieldMin, sizeMin };
         //   yield return new object[] { fieldMax, sizeMax };
        }

        [Theory]
        [MemberData(nameof(InvalidTestData))]
        public void BuildPresetField_InvalidParamPassedShouldFail(IField expected, int fieldSize)
        {
           var output = _factory.BuildPresetField(fieldSize);
           Assert.Equal(expected, output);
        }
        public static IEnumerable<object[]> InvalidTestData()
        {
            yield return new object[] {null, 0 };
            yield return new object[] {null, 100 };
            yield return new object[] {null, -10 };

        }
    }
}
