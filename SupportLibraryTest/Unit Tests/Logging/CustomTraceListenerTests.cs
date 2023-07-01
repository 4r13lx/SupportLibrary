using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Logging;
using SupportLibrary.Logging.TraceListeners;
using SupportLibrary.Text;

namespace SupportLibraryTest.Logging
{
    /// <summary>
    /// Testing of Logging namespace classes.
    /// </summary>
    [TestClass]
    public class CustomTraceListenerTests
    {
        private const string LOG_NAME = "SupportLibrary";
        private const string LOG_SOURCE = "SupportLibraryLogSource";

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void CustomConsoleTraceListener_WriteLine()
        {
            string[] consoleOutput = null;

            using (StringWriter sw = new StringWriter())
            {
                // arrange
                Console.SetOut(sw);

                Trace.Listeners.Clear();
                Trace.Listeners.Add(new CustomConsoleTraceListener(TraceOption.None));
                Trace.Listeners.Add(new CustomConsoleTraceListener(TraceOption.DateTimeOnly));
                Trace.Listeners.Add(new CustomConsoleTraceListener(TraceOption.DateTimeAndMethod));
                Trace.Listeners.Add(new CustomConsoleTraceListener(TraceOption.DateTimeAndClassAndMethod));
                Trace.Listeners.Add(new CustomConsoleTraceListener(TraceOption.DateTimeAndFullClassAndMethod));

                // act
                Trace.WriteLine("Test");
                consoleOutput = sw.ToString().Split(Environment.NewLine);
            }

            // assert
            Assert.IsNotNull(consoleOutput, "Assert 01");
            Assert.AreEqual(6, consoleOutput.Length, "Assert 02");

            Assert.AreNotEqual(String.Empty, consoleOutput[0], "Assert 03");
            Assert.AreNotEqual(String.Empty, consoleOutput[1], "Assert 04");
            Assert.AreNotEqual(String.Empty, consoleOutput[2], "Assert 05");
            Assert.AreNotEqual(String.Empty, consoleOutput[3], "Assert 06");
            Assert.AreNotEqual(String.Empty, consoleOutput[4], "Assert 07");

            Assert.IsTrue(consoleOutput[0].Contains("Test"), "Assert 08");
            Assert.IsTrue(consoleOutput[1].Contains("Test"), "Assert 09");
            Assert.IsTrue(consoleOutput[2].Contains("Test"), "Assert 10");
            Assert.IsTrue(consoleOutput[3].Contains("Test"), "Assert 11");
            Assert.IsTrue(consoleOutput[4].Contains("Test"), "Assert 12");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void CustomEventLogTraceListener_WriteLine()
        {
            // arrange
            EventLog eventLog = new EventLog(LOG_NAME, ".", LOG_SOURCE);

            EventLogEntry prevLogEntry = eventLog.Entries.Cast<EventLogEntry>().Where(a => a.Source == LOG_SOURCE).OrderByDescending(b => b.TimeWritten).FirstOrDefault();
            DateTime prevLogEntryTime = (prevLogEntry != null) ? prevLogEntry.TimeWritten : DateTime.MinValue;

            Trace.Listeners.Clear();
            Trace.Listeners.Add(new CustomEventLogTraceListener(eventLog, TraceOption.ClassAndMethod));

            // act
            Trace.WriteLine("Test");
            EventLogEntry newLogEntry = eventLog.Entries.Cast<EventLogEntry>().Where(a => a.EntryType == EventLogEntryType.Information && a.TimeWritten >= prevLogEntryTime).FirstOrDefault();

            // assert
            Assert.IsNotNull(newLogEntry, "Assert 01");
            Assert.AreEqual(EventLogEntryType.Information, newLogEntry.EntryType, "Assert 02");
            Assert.AreEqual(LOG_SOURCE, newLogEntry.Source, "Assert 03");
            Assert.IsTrue(newLogEntry.Message.Contains("Test"), "Assert 04");

            // cleanup
            eventLog.Clear();
        }
    }
}
