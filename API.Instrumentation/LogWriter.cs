using System;
using System.Diagnostics;
using System.Text;

namespace API.Instrumentation
{
    public sealed class LogWriter : ILogWriter
    {   
        static TraceSource ts = new TraceSource("Instrumentation");

        private static readonly Lazy<LogWriter> lazy =
        new Lazy<LogWriter>(() => new LogWriter());

        public static LogWriter Instance { get { return lazy.Value; } }

        private LogWriter()
        {
        }
        public void LogDebug(string message)
        {
            ErrorTraceLog(TraceEventType.Verbose, message);
        }

        public void LogError(Exception ex)
        {
            ErrorTraceLog(TraceEventType.Critical, null,ex);
        }

        public void LogError(string message, Exception ex)
        {
            ErrorTraceLog(TraceEventType.Critical, message, ex);
        }

        public void LogInformation(string message)
        {
            ErrorTraceLog(TraceEventType.Information, message);
        }    

        public void LogWarning(string message)
        {
            ErrorTraceLog(TraceEventType.Warning, message);
        }

        public void LogWarning(string message, Exception ex)
        {
            ErrorTraceLog(TraceEventType.Warning, message);
        }

        private void ErrorTraceLog(TraceEventType type, string message = null, Exception exception =null)
        {
            StringBuilder writer = new StringBuilder();
            string stackTrace = string.Empty;
            string log = message;           
            string userID = string.Empty;           

            if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity != null)
            {
                userID = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            }

            if (exception != null)
            {
                stackTrace = string.Format("{0}: {1}{2}{3}", message, exception.Message, Environment.NewLine, exception.StackTrace);
            }
           
            writer.AppendLine(string.Format("{0} : {1}", "TYPE".PadRight(15, ' '), type));           
            writer.AppendLine(string.Format("{0} : {1}", "USER".PadRight(15, ' '), userID));  
            if (!string.IsNullOrWhiteSpace(log))
            {
                writer.AppendLine(string.Format("{0} : {1}", "LOG".PadRight(15, ' '), log));
            }
            if (!string.IsNullOrWhiteSpace(stackTrace))
            {
                writer.AppendLine(string.Format("{0} : {1}", "STACKTRACE".PadRight(15, ' '), stackTrace));
            }
            ts.TraceData(type, 0, writer.ToString());            
        }
    }
}
