using Unisaw.Core;

namespace Unisaw.CLI.Commands
{
    public sealed class DivCommand : ICommand
    {
        public string Name => "div";
        public string[] Aliases => new[] { "divide" };

        public int Execute(int a, int b) => Calculator.Divide(a, b);
    }
}
