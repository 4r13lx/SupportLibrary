using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Testing;
using SupportLibrary.Logging;

namespace SupportLibraryTest.Logging
{
    /// <summary>
    /// Testing of Logging namespace classes.
    /// </summary>
    [TestClass]
    public class EventLogHelperTests
    {
        private const string LOG_NAME = "SupportLibrary";
        private const string LOG_SOURCE = "SupportLibraryLogSource";
        private const string MSG_INFO = "Info message.", MSG_WARNING = "Warning message.", MSG_ERROR = "Error message.";

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void EventLogHelper_Log_Message()
        {
            // arrange
            if (!EventLog.SourceExists(LOG_SOURCE)) { EventLog.CreateEventSource(LOG_SOURCE, LOG_NAME); }
            EventLog eventLog = new EventLog(LOG_NAME, ".", LOG_SOURCE);

            EventLogEntry prevLogEntry = eventLog.Entries.Cast<EventLogEntry>().Where(a => a.Source == LOG_SOURCE).OrderByDescending(b => b.TimeWritten).FirstOrDefault();
            DateTime prevLogEntryTime = (prevLogEntry != null) ? prevLogEntry.TimeWritten : DateTime.MinValue;

            // act
            new EventLogHelper(LOG_NAME, LOG_SOURCE).Log(EntryType.Info, MSG_INFO);
            new EventLogHelper(LOG_NAME, LOG_SOURCE).Log(EntryType.Warning, MSG_WARNING);
            new EventLogHelper(LOG_NAME, LOG_SOURCE).Log(EntryType.Error, MSG_ERROR);

            EventLogEntry newLogEntry1 = eventLog.Entries.Cast<EventLogEntry>().Where(a => a.EntryType == EventLogEntryType.Information && a.TimeWritten >= prevLogEntryTime).FirstOrDefault();
            EventLogEntry newLogEntry2 = eventLog.Entries.Cast<EventLogEntry>().Where(a => a.EntryType == EventLogEntryType.Warning && a.TimeWritten >= prevLogEntryTime).FirstOrDefault();
            EventLogEntry newLogEntry3 = eventLog.Entries.Cast<EventLogEntry>().Where(a => a.EntryType == EventLogEntryType.Error && a.TimeWritten >= prevLogEntryTime).FirstOrDefault();

            // assert
            Assert.IsNotNull(newLogEntry1, "Assert 01");
            Assert.AreEqual(EventLogEntryType.Information, newLogEntry1.EntryType, "Assert 02");
            Assert.AreEqual(LOG_SOURCE, newLogEntry1.Source, "Assert 03");
            Assert.AreEqual(MSG_INFO, newLogEntry1.Message, "Assert 04");

            Assert.IsNotNull(newLogEntry2, "Assert 05");
            Assert.AreEqual(EventLogEntryType.Warning, newLogEntry2.EntryType, "Assert 06");
            Assert.AreEqual(LOG_SOURCE, newLogEntry2.Source, "Assert 07");
            Assert.AreEqual(MSG_WARNING, newLogEntry2.Message, "Assert 08");

            Assert.IsNotNull(newLogEntry3, "Assert 09");
            Assert.AreEqual(EventLogEntryType.Error, newLogEntry3.EntryType, "Assert 10");
            Assert.AreEqual(LOG_SOURCE, newLogEntry3.Source, "Assert 11");
            Assert.AreEqual(MSG_ERROR, newLogEntry3.Message, "Assert 12");

            // cleanup
            eventLog.Clear();
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void EventLogHelper_Log_Exception()
        {
            // arrange
            if (!EventLog.SourceExists(LOG_SOURCE)) { EventLog.CreateEventSource(LOG_SOURCE, LOG_NAME); }
            EventLog eventLog = new EventLog(LOG_NAME, ".", LOG_SOURCE);

            EventLogEntry prevLogEntry = eventLog.Entries.Cast<EventLogEntry>().Where(a => a.Source == LOG_SOURCE).OrderByDescending(b => b.TimeWritten).FirstOrDefault();
            DateTime prevLogEntryTime = (prevLogEntry != null) ? prevLogEntry.TimeWritten : DateTime.MinValue;

            // act
            new EventLogHelper(LOG_NAME, LOG_SOURCE).Log(new ArgumentException("Param1 is null.", "Param1"));
            new EventLogHelper(LOG_NAME, LOG_SOURCE).Log(new DivideByZeroException());

            EventLogEntry newLogEntry = eventLog.Entries.Cast<EventLogEntry>().Where(a => a.EntryType == EventLogEntryType.Error && a.TimeWritten >= prevLogEntryTime).FirstOrDefault();

            // assert
            Assert.IsNotNull(newLogEntry, "Assert 01");
            Assert.AreEqual(EventLogEntryType.Error, newLogEntry.EntryType, "Assert 02");
            Assert.AreEqual(LOG_SOURCE, newLogEntry.Source, "Assert 03");

            // cleanup
            eventLog.Clear();
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void EventLogHelper_Log_FailsOnEmptyParams()
        {
            // arrange
            Action action1 = () => new EventLogHelper("", LOG_SOURCE).Log(EntryType.Info, MSG_INFO); // invalid logName
            Action action2 = () => new EventLogHelper(LOG_NAME, null).Log(EntryType.Info, MSG_INFO); // invalid logSource

            // act & assert
            ArgumentNullException exception1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            Assert.AreEqual("logName", exception1.ParamName, "Assert 01");

            ArgumentNullException exception2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
            Assert.AreEqual("logSource", exception2.ParamName, "Assert 02");
        }
    }
}
