namespace Unisaw.CLI.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string[] Aliases { get; }
        int Execute(int a, int b);
    }
}
