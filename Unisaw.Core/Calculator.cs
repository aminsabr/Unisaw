namespace Unisaw.Core
{
    public static class Calculator
    {
        public static int Add(int a, int b) => checked(a + b);
        public static int Subtract(int a, int b) => checked(a - b);
        public static int Multiply(int a, int b) => checked(a * b);

        public static int Divide(int a, int b)
        {
            if (b == 0) throw new DivideByZeroException("b must not be zero.");
            return checked(a / b);
        }

        public static bool TryDivide(int a, int b, out int result)
        {
            if (b == 0)
            {
                result = 0;
                return false;
            }

            try
            {
                result = checked(a / b);
                return true;
            }
            catch (OverflowException)
            {
                result = 0;
                return false;
            }
        }
    }
}
