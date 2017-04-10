using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPilotApp.Common
{
    public enum LogLevel
    {
        Error,
        Warning,
        Event,
        Info
    }
    public sealed class LoggerEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public LogLevel Level { get; private set; }
        public LoggerEventArgs(string message, LogLevel level)
        {
            Message = message;
            Level = level;
        }
    }

    public static class Logger
    {
        public static event EventHandler<LoggerEventArgs> LogReceived;

        public static void LogInfo(string info)
        {
            Log($"Info: {info}", LogLevel.Info);
        }
        public static void LogError(string error)
        {
            Log($"Error: {error}", LogLevel.Error);
        }
        public static void LogException(Exception ex)
        {
            Log($"Exception: {ex.Message}: {ex.StackTrace}", LogLevel.Error);
        }

        public static void Log(string message, LogLevel level)
        {
            Debug.WriteLine($"({DateTime.UtcNow}) {message}");
            LogReceived?.Invoke(null, new LoggerEventArgs(message, level));
        }
    }
}
