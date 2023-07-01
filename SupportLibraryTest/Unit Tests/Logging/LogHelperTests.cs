using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Logging;
using SupportLibrary.Testing;

namespace SupportLibraryTest.Logging
{
    /// <summary>
    /// Testing of Logging namespace classes.
    /// </summary>
    [TestClass]
    public class LogHelperTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void LogHelper_BuildExceptionMessage()
        {
            // arrange
            StubLogHelper logHelper = new StubLogHelper();

            // act
            string exceptionMessage1 = logHelper.BuildExceptionMessage(new ArgumentException("Param1 is null.", "Param1"));
            string exceptionMessage2 = logHelper.BuildExceptionMessage(new DivideByZeroException());

            // assert
            Assert.IsTrue(exceptionMessage1.Contains("System.ArgumentException"));
            Assert.IsTrue(exceptionMessage2.Contains("System.DivideByZeroException"));
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void LogHelper_BuildExceptionMessage_FailsOnNullArgument()
        {
            // arrange
            StubLogHelper logHelper = new StubLogHelper();
            Action action = () => logHelper.BuildExceptionMessage(null);

            // act
            ArgumentNullException exception = AssertEx.Exceptions.Throws<ArgumentNullException>(action);
        }

        /// <summary>
        /// Just to make use of 'LogHelper.BuildExceptionMessage()'
        /// </summary>
        private class StubLogHelper : LogHelper
        {
            public new string BuildExceptionMessage(Exception exception)
            {
                return base.BuildExceptionMessage(exception);
            }

            public override void Log(EntryType entryType, string message)
            {
                throw new NotImplementedException();
            }

            public override void Log(Exception exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
