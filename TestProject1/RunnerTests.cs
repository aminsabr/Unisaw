using System;
using System.Collections.Generic;
using Unisaw.CLI.Commands;
using Unisaw.CLI.Logging;
using Unisaw.CLI.Runtime;
using Xunit;

namespace Unisaw.Tests
{
    // A very small test logger to capture log calls for assertions
    internal sealed class TestLogger : ILogger
    {
        public LogLevel MinimumLevel { get; set; } = LogLevel.Trace;
        public List<(LogLevel level, string message, string? exception)> Entries { get; } = new();

        public void Log(LogLevel level, string message)
        {
            if (level < MinimumLevel) return;
            Entries.Add((level, message, null));
        }

        public void Log(LogLevel level, string message, Exception ex)
        {
            if (level < MinimumLevel) return;
            Entries.Add((level, message, $"{ex.GetType().Name}: {ex.Message}"));
        }

        public void Info(string message) => Log(LogLevel.Information, message);
        public void Warn(string message) => Log(LogLevel.Warning, message);
        public void Error(string message) => Log(LogLevel.Error, message);
        public void Error(string message, Exception ex) => Log(LogLevel.Error, message, ex);
    }

    public class RunnerTests
    {
        private static CommandRegistry BuildRegistry()
        {
            // Create the same registry as Program would
            return new CommandRegistry(new ICommand[]
            {
                new AddCommand(),
                new SubCommand(),
                new MulCommand(),
                new DivCommand()
            });
        }

        [Fact]
        public void AddCommand_ReturnsOk_AndLogsResult()
        {
            // Arrange
            var registry = BuildRegistry();
            var logger = new TestLogger { MinimumLevel = LogLevel.Information };
            var runner = new Runner(registry, logger);

            // Act
            var code = runner.Execute(new[] { "add", "2", "3" });

            // Assert
            Assert.Equal(Runner.Ok, code);
            // last info entry should contain result "5"
            Assert.Contains(logger.Entries, e => e.level == LogLevel.Information && e.message.Contains("5"));
        }

        [Fact]
        public void Div_ByZero_ReturnsArithmeticError_AndLogsError()
        {
            // Arrange
            var registry = BuildRegistry();
            var logger = new TestLogger { MinimumLevel = LogLevel.Information };
            var runner = new Runner(registry, logger);

            // Act
            var code = runner.Execute(new[] { "div", "1", "0" });

            // Assert
            Assert.Equal(Runner.ArithmeticError, code);
            // should have at least one error-level entry mentioning divide
            Assert.Contains(logger.Entries, e => e.level == LogLevel.Error && (e.message.Contains("divide", StringComparison.OrdinalIgnoreCase) || (e.exception != null && e.exception.Contains("DivideByZeroException"))));
        }

        [Fact]
        public void UnknownCommand_ReturnsUnknownCommand_AndLogsError()
        {
            // Arrange
            var registry = BuildRegistry();
            var logger = new TestLogger { MinimumLevel = LogLevel.Information };
            var runner = new Runner(registry, logger);

            // Act
            var code = runner.Execute(new[] { "xyzcmd", "1", "2" });

            // Assert
            Assert.Equal(Runner.UnknownCommand, code);
            Assert.Contains(logger.Entries, e => e.level == LogLevel.Error && e.message.Contains("unknown command", StringComparison.OrdinalIgnoreCase));
        }
    }
}
