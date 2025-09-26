using System;
using System.Globalization;
using Unisaw.CLI.Commands;
using Unisaw.CLI.Logging;
using Unisaw.CLI.Runtime;

namespace Unisaw.CLI
{
    internal static class Program
    {
        private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        // Commands registry
        private static readonly CommandRegistry Registry = new CommandRegistry(new ICommand[]
        {
            new AddCommand(),
            new SubCommand(),
            new MulCommand(),
            new DivCommand()
        });

        // Logger field
        private static readonly ILogger Logger = new ConsoleLogger
        {
            MinimumLevel = LogLevel.Information
        };

        // Runner (injects registry + logger)
        private static readonly Runner Runner = new Runner(Registry, Logger);

        private static int Main(string[] args)
        {
            if (args.Length > 0) return Runner.Execute(args);

            PrintBanner();
            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (line is null) break;

                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (parts.Length == 0) continue;

                if (parts[0].Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                    parts[0].Equals("quit", StringComparison.OrdinalIgnoreCase))
                    return Runner.Ok;

                if (parts[0].Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    PrintUsage();
                    continue;
                }

                _ = Runner.Execute(parts); // keep interactive loop alive
            }

            return Runner.Ok;
        }

        private static void PrintBanner()
        {
            Logger.Info("Unisaw CLI");
            PrintUsage();
        }

        private static void PrintUsage()
        {
            Logger.Info("Usage:");
            Logger.Info("  unisaw add <a> <b>");
            Logger.Info("  unisaw sub <a> <b>");
            Logger.Info("  unisaw mul <a> <b>");
            Logger.Info("  unisaw div <a> <b>");
            Logger.Info("Interactive mode commands: help, exit");
        }
    }
}
