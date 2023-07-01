using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Core;
using SupportLibrary.Testing;

namespace SupportLibraryTest.Core
{
    /// <summary>
    /// Testing of Core namespace classes.
    /// </summary>
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.DateTime")]
        public void Extensions_LastDayOfMonth()
        {
            // arrange
            DateTime input = new DateTime(2017, 09, 19);

            // act
            DateTime lastDay = input.LastDayOfMonth();

            // assert
            Assert.AreEqual(new DateTime(2017, 09, 30), lastDay);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.DateTime")]
        public void Extensions_FirstDayOfNextMonth()
        {
            // arrange
            DateTime input = new DateTime(2017, 09, 19);

            // act
            DateTime firstDay = input.FirstDayOfNextMonth();

            // assert
            Assert.AreEqual(new DateTime(2017, 10, 01), firstDay);
        }
    }
}
