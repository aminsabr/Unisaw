using System;
using Unisaw.Core;
using Xunit;

namespace Unisaw.Tests
{
    public class CalculatorEdgeTests
    {
        [Fact]
        public void Multiply_Overflow_Throws()
        {
            Assert.Throws<OverflowException>(() => Calculator.Multiply(int.MaxValue, 2));
        }

        [Fact]
        public void Subtract_Overflow_Throws()
        {
            Assert.Throws<OverflowException>(() => Calculator.Subtract(int.MaxValue, -1));
        }

        [Fact]
        public void TryDivide_Valid_ReturnsTrueAndSetsResult()
        {
            var ok = Calculator.TryDivide(20, 5, out var res);
            Assert.True(ok);
            Assert.Equal(4, res);
        }

        [Fact]
        public void TryDivide_ByZero_ReturnsFalseAndZero()
        {
            var ok = Calculator.TryDivide(10, 0, out var res);
            Assert.False(ok);
            Assert.Equal(0, res);
        }
    }
}
