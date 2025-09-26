using Unisaw.Core;

namespace Unisaw.CLI.Commands
{
    public sealed class SubCommand : ICommand
    {
        public string Name => "sub";
        public string[] Aliases => new[] { "subtract" };

        public int Execute(int a, int b) => Calculator.Subtract(a, b);
    }
}
