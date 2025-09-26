using System;

namespace Unisaw.CLI.Logging
{
    public interface ILogger
    {
        LogLevel MinimumLevel { get; set; }

        void Log(LogLevel level, string message);
        void Log(LogLevel level, string message, Exception ex);

        // Convenience helpers
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Error(string message, Exception ex);
    }
}
