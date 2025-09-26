using System;
using Unisaw.Core;
using Xunit;

namespace Unisaw.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_ReturnsCorrectSum()
        {
            var result = Calculator.Add(5, 7);
            Assert.Equal(12, result);
        }

        [Fact]
        public void Subtract_ReturnsCorrectDifference()
        {
            var result = Calculator.Subtract(10, 3);
            Assert.Equal(7, result);
        }

        [Fact]
        public void Multiply_ReturnsCorrectProduct()
        {
            var result = Calculator.Multiply(6, 7);
            Assert.Equal(42, result);
        }

        [Fact]
        public void Divide_ReturnsCorrectQuotient()
        {
            var result = Calculator.Divide(20, 5);
            Assert.Equal(4, result);
        }

        [Fact]
        public void Divide_ByZero_Throws()
        {
            Assert.Throws<DivideByZeroException>(() => Calculator.Divide(1, 0));
        }

        [Fact]
        public void Add_Overflow_Throws()
        {
            Assert.Throws<OverflowException>(() => Calculator.Add(int.MaxValue, 1));
        }
    }
}
