using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Core;

namespace SupportLibraryTest.Core
{
    /// <summary>
    /// Testing of Core namespace classes.
    /// </summary>
    [TestClass]
    public class DateTimeTest
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.DateTime")]
        public void DateTimeEx_LastDayOfCurrentMonth()
        {
            // arrange
            DateTime input1 = new DateTime(2017, 09, 19);
            DateTime input2 = new DateTime(2017, 12, 25);

            DateTime expected1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            DateTime expected2 = new DateTime(2017, 09, 30);
            DateTime expected3 = new DateTime(2017, 12, 31);

            // act
            DateTime lastDay1 = DateTimeEx.LastDayOfMonth();         // 2017-09-30 use today
            DateTime lastDay2 = DateTimeEx.LastDayOfMonth(input1);   // use a specific date
            DateTime lastDay3 = DateTimeEx.LastDayOfMonth(input2);   // use a specific date

            // assert
            Assert.AreEqual(expected1, lastDay1);
            Assert.AreEqual(expected2, lastDay2);
            Assert.AreEqual(expected3, lastDay3);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.DateTime")]
        public void DateTimeEx_FirstDayOfNextMonth()
        {
            // arrange
            DateTime input1 = new DateTime(2017, 09, 19);
            DateTime input2 = new DateTime(2017, 12, 25);

            DateTime expected1 = new DateTime(DateTime.Today.AddMonths(1).Year, DateTime.Today.AddMonths(1).Month, 1);
            DateTime expected2 = new DateTime(2017, 10, 01);
            DateTime expected3 = new DateTime(2018, 01, 01);

            // act
            DateTime firstDay1 = DateTimeEx.FirstDayOfNextMonth();         // use today
            DateTime firstDay2 = DateTimeEx.FirstDayOfNextMonth(input1);   // use a specific date
            DateTime firstDay3 = DateTimeEx.FirstDayOfNextMonth(input2);   // use a specific date

            // assert
            Assert.AreEqual(expected1, firstDay1);
            Assert.AreEqual(expected2, firstDay2);
            Assert.AreEqual(expected3, firstDay3);
        }
    }
}
