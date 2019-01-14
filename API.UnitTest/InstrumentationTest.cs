using System;
using API.Instrumentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace API.UnitTest
{
    [TestClass]
    public class InstrumentationTest
    {
        [TestMethod]
        public void LogError()
        {
            LogWriter.Instance.LogError(new ArgumentException("Test Mathod"));
        }

        [TestMethod]
        public void LogDebug()
        {
            LogWriter.Instance.LogDebug("Debug Test");
        }

        [TestMethod]
        public void LogInformation()
        {
            LogWriter.Instance.LogInformation("Information test");
        }

        [TestMethod]
        public void LogWarning()
        {
            LogWriter.Instance.LogWarning("Log Warning");
        }      
    }
}
