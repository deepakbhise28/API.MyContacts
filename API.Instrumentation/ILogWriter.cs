using System;

namespace API.Instrumentation
{
  public interface ILogWriter
    {
        void LogInformation(string message);
        void LogError(Exception ex);
        void LogError(string message, Exception ex);
        void LogDebug(string message);
        void LogWarning(string message);
        void LogWarning(string message, Exception ex);
    }
}
