using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Logging;
using SupportLibrary.Testing;

namespace SupportLibraryTest.Logging
{
    /// <summary>
    /// Testing of Logging namespace classes.
    /// </summary>
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void Extensions_ToEventLog()
        {
            // arrange & act
            EventLogEntryType resultEntryType1 = EntryType.Info.ToEventLog();
            EventLogEntryType resultEntryType2 = EntryType.Warning.ToEventLog();
            EventLogEntryType resultEntryType3 = EntryType.Error.ToEventLog();

            // assert
            Assert.AreEqual(EventLogEntryType.Information, resultEntryType1, "Assert 01");
            Assert.AreEqual(EventLogEntryType.Warning, resultEntryType2, "Assert 02");
            Assert.AreEqual(EventLogEntryType.Error, resultEntryType3, "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void Extensions_ToEventLog_Fails()
        {
            // arrange
            Action action = () => EntryType.None.ToEventLog();

            // act & assert
            InvalidCastException exception = AssertEx.Exceptions.Throws<InvalidCastException>(action);
            Assert.IsTrue(exception.Message.Contains(EntryType.None.ToString()));
        }
    }
}
