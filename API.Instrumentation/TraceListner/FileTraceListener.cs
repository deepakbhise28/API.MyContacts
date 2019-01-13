using System;
using System.Diagnostics;
using System.IO;

namespace API.Instrumentation.TraceListner
{
    public class FileTraceListener : TraceListener
    {
        private string FilePath;
        private static readonly object Locker = new object();
        

        public FileTraceListener(string initializeData)
        {
            //var properties = initializeData.Split('|');
            FilePath = initializeData;            
        }
        public override void Write(string message)
        {
           // ErrorTraceLog(message);
        }

        public override void WriteLine(string message)
        {
            ErrorTraceLog(message);
        }


        private void ErrorTraceLog(string message)
        {
            DirectoryInfo dirInfo = null;
            try
            {
                dirInfo = new DirectoryInfo(FilePath);
                if (!dirInfo.Parent.Exists)
                {
                    dirInfo.Parent.Create();
                }

                lock (Locker)
                {                 

                    using (StreamWriter writer = new StreamWriter(FilePath, true))
                    {
                        writer.WriteLine(string.Format("{0} : {1}", "TIMESTAMP".PadRight(15, ' '), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
                        writer.WriteLine(message);
                        writer.WriteLine("--------------------------------------------------------------------------------------------------");
                    }
                }
            }
            finally
            {
                if (dirInfo != null)
                {
                    dirInfo = null;
                }
            }
        }

        //public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        //{


        //}

        //public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        //{
        //    base.TraceEvent(eventCache, source, eventType, id, format, args);
        //}
    }
}
