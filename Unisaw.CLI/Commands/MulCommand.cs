using Unisaw.Core;

namespace Unisaw.CLI.Commands
{
    public sealed class MulCommand : ICommand
    {
        public string Name => "mul";
        public string[] Aliases => new[] { "multiply" };

        public int Execute(int a, int b) => Calculator.Multiply(a, b);
    }
}
