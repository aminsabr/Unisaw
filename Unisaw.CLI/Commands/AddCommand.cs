using Unisaw.Core;

namespace Unisaw.CLI.Commands
{
    public sealed class AddCommand : ICommand
    {
        public string Name => "add";
        public string[] Aliases => new[] { "plus" };

        public int Execute(int a, int b) => Calculator.Add(a, b);
    }
}
