using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Logging;

namespace SupportLibraryTest
{
    /// <summary>
    /// Testing of Logging namespace classes.
    /// </summary>
    [TestClass]
    public class LoggingTests
    {
        private const string LOG_NAME = "SupportLibrary";
        private const string LOG_SOURCE = "SupportLibraryLogSource";
        private const string MSG_INFO = "Info message.", MSG_WARNING = "Warning message.", MSC_ERROR = "Error message.";

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void EventLogHelper_Log_Message_Valid()
        {
            // arrange
            if (!EventLog.SourceExists(LOG_SOURCE)) { EventLog.CreateEventSource(LOG_SOURCE, LOG_NAME); }
            EventLog eventLog = new EventLog(LOG_NAME, ".", LOG_SOURCE);

            EventLogEntry prevLogEntry = eventLog.Entries.Cast<EventLogEntry>().Where(a => a.Source == LOG_SOURCE).OrderByDescending(b => b.TimeWritten).FirstOrDefault();
            DateTime prevLogEntryTime = (prevLogEntry != null) ? prevLogEntry.TimeWritten : DateTime.MinValue;

            // act
            new EventLogHelper(LOG_NAME, LOG_SOURCE).Log(EventLogEntryType.Information, MSG_INFO);
            new EventLogHelper(LOG_NAME, LOG_SOURCE).Log(EventLogEntryType.Warning, MSG_WARNING);
            new EventLogHelper(LOG_NAME, LOG_SOURCE).Log(EventLogEntryType.Error, MSC_ERROR);

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
            Assert.AreEqual(MSC_ERROR, newLogEntry3.Message, "Assert 12");

            // clean
            eventLog.Clear();
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void EventLogHelper_Log_Exception_Valid()
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

            // clean
            eventLog.Clear();
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EventLogHelper_Log_Message_Invalid_LogName()
        {
            // arrange & act
            new EventLogHelper("", LOG_SOURCE).Log(EventLogEntryType.Information, MSG_INFO);

            // assert
            Assert.Fail("EventLogHelper.Log() parameters were not properly validated.");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EventLogHelper_Log_Message_Invalid_LogSource()
        {
            // arrange & act
            new EventLogHelper(LOG_NAME, null).Log(EventLogEntryType.Information, MSG_INFO);

            // assert
            Assert.Fail("EventLogHelper.Log() parameters were not properly validated.");
        }    
    }
}
