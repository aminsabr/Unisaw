using System;
using System.Globalization;

namespace Unisaw.CLI.Logging
{
    public sealed class ConsoleLogger : ILogger
    {
        private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        public LogLevel MinimumLevel { get; set; } = LogLevel.Information;

        public void Log(LogLevel level, string message)
        {
            if (level < MinimumLevel) return;

            var ts = DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", Inv);
            var line = $"[{ts}] [{level}] {message}";

            if (level >= LogLevel.Error)
                Console.Error.WriteLine(line);
            else
                Console.WriteLine(line);
        }

        public void Log(LogLevel level, string message, Exception ex)
            => Log(level, $"{message} | ex={ex.GetType().Name}: {ex.Message}");

        public void Info(string message) => Log(LogLevel.Information, message);
        public void Warn(string message) => Log(LogLevel.Warning, message);
        public void Error(string message) => Log(LogLevel.Error, message);
        public void Error(string message, Exception ex) => Log(LogLevel.Error, message, ex);
    }
}
