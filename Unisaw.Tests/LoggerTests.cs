using System;
using System.IO;
using Unisaw.CLI.Logging;
using Xunit;

namespace Unisaw.Tests
{
    public class LoggerTests
    {
        [Fact]
        public void Info_WritesToStdOut_WithInformationLevel()
        {
            // Arrange
            var logger = new ConsoleLogger { MinimumLevel = LogLevel.Information };
            using var sw = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(sw);

            try
            {
                // Act
                logger.Info("hello");

                // Assert
                var output = sw.ToString();
                Assert.Contains("[Information]", output);
                Assert.Contains("hello", output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Error_WritesToStdErr_WithErrorLevel()
        {
            var logger = new ConsoleLogger { MinimumLevel = LogLevel.Information };
            using var swErr = new StringWriter();
            var originalErr = Console.Error;
            Console.SetError(swErr);

            try
            {
                logger.Error("boom");

                var output = swErr.ToString();
                Assert.Contains("[Error]", output);
                Assert.Contains("boom", output);
            }
            finally
            {
                Console.SetError(originalErr);
            }
        }

        [Fact]
        public void Error_WithException_AppendsExceptionTypeAndMessage()
        {
            var logger = new ConsoleLogger { MinimumLevel = LogLevel.Information };
            using var swErr = new StringWriter();
            var originalErr = Console.Error;
            Console.SetError(swErr);

            try
            {
                var ex = new DivideByZeroException("b must not be zero.");
                logger.Error("divide", ex);

                var output = swErr.ToString();
                Assert.Contains("ex=DivideByZeroException", output);
                Assert.Contains("b must not be zero.", output);
            }
            finally
            {
                Console.SetError(originalErr);
            }
        }

        [Fact]
        public void BelowMinimumLevel_IsSuppressed()
        {
            var logger = new ConsoleLogger { MinimumLevel = LogLevel.Warning };
            using var sw = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(sw);

            try
            {
                logger.Info("should not appear");
                var output = sw.ToString();
                Assert.DoesNotContain("should not appear", output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}
