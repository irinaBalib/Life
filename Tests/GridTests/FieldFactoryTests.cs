using Autofac.Extras.Moq;
using GameOfLife;
using GameOfLife.Constants;
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

        [Fact]
        public void BuildPresetField_ShouldReturnFieldOfTheDefinedSize()
        {
            int fieldSize = 20;
            var output = _factory.BuildPresetField(fieldSize);
            Assert.NotNull(output);
            Assert.True(output.Dimension == fieldSize);
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void CreateField_ShouldPass(IField expected, int fieldSize)
        {
            var output = _factory.Create(fieldSize);
            Assert.NotNull(output);
            Assert.Equal(expected.Dimension, output.Dimension);
            Assert.Equal(expected.Generation, output.Generation);
            Assert.Equal(expected.Index, output.Index);
        }
        public static IEnumerable<object[]> TestData()
        {
            int size = 25;
            IField field = new SquareField()
            {
                Dimension = size,
                Generation = 0,
                Index = 1,
                Cells = new bool[size, size]
            };

            int sizeMin = NumericData.FieldMinSize;
            IField fieldMin = new SquareField()
            {
                Dimension = sizeMin,
                Generation = 0,
                Index = 1,
                Cells = new bool[sizeMin, sizeMin]
            };

            int sizeMax = NumericData.FieldMaxSize;
            IField fieldMax = new SquareField()
            {
                Dimension = sizeMax,
                Generation = 0,
                Index = 1,
                Cells = new bool[sizeMax, sizeMax]
            };

            yield return new object[] { field, size};
            yield return new object[] { fieldMin, sizeMin };
            yield return new object[] { fieldMax, sizeMax };
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        [InlineData(100000)]
        public void CreateField_ShouldFail_InvalidParametersPassed(int fieldSize)
        {
          Assert.Throws<ArgumentOutOfRangeException>(()=> _factory.Create(fieldSize));
        }
       
    }
}
