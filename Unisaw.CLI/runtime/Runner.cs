using System;
using System.Globalization;
using Unisaw.CLI.Commands;
using Unisaw.CLI.Logging;

namespace Unisaw.CLI.Runtime
{
    public sealed class Runner
    {
        public const int Ok = 0;
        public const int BadInput = 1;
        public const int ArithmeticError = 2;
        public const int UnknownCommand = 3;

        private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        private readonly CommandRegistry _registry;
        private readonly ILogger _logger;

        public Runner(CommandRegistry registry, ILogger logger)
        {
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public int Execute(string[] parts)
        {
            if (parts.Length != 3)
            {
                _logger.Error("Error: expected exactly 2 operands.");
                return BadInput;
            }

            if (!_registry.TryGet(parts[0], out var command))
            {
                _logger.Error($"Error: unknown command '{parts[0]}'.");
                return UnknownCommand;
            }

            if (!int.TryParse(parts[1], NumberStyles.Integer, Inv, out var a) ||
                !int.TryParse(parts[2], NumberStyles.Integer, Inv, out var b))
            {
                _logger.Error("Error: operands must be 32-bit integers (decimal).");
                return BadInput;
            }

            try
            {
                var result = command.Execute(a, b);
                _logger.Info(result.ToString(Inv));
                return Ok;
            }
            catch (DivideByZeroException ex)
            {
                _logger.Error("Error: divide by zero.", ex);
                return ArithmeticError;
            }
            catch (OverflowException ex)
            {
                _logger.Error("Error: arithmetic overflow.", ex);
                return ArithmeticError;
            }
        }
    }
}
