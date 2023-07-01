using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

using SupportLibrary.Testing;

namespace SupportLibraryTest.Testing
{
    /// <summary>
    /// Testing of Testing namespace classes.
    /// </summary>
    [TestClass]
    public class AssertEx_ExceptionsTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Exceptions_Throws()
        {
            // arrange
            Action action1 = () => { throw new Exception("A regular exception."); };
            Action action2 = () => { throw new ApplicationException("A application exception."); };
            Action action3 = () => { throw new ArgumentException("A argument exception."); };
            Action action4 = () => { throw new DivideByZeroException("A division by zero exception."); };
            Action action5 = () => { int a = 10, b = 0, c = a / b; };   // DivideByZeroException

            // act
            Exception exception1 = AssertEx.Exceptions.Throws<Exception>(action1);
            ApplicationException exception2 = AssertEx.Exceptions.Throws<ApplicationException>(action2);
            ArgumentException exception3 = AssertEx.Exceptions.Throws<ArgumentException>(action3);
            DivideByZeroException exception4 = AssertEx.Exceptions.Throws<DivideByZeroException>(action4);
            DivideByZeroException exception5 = AssertEx.Exceptions.Throws<DivideByZeroException>(action5);

            // assert
            Assert.AreEqual("A regular exception.", exception1.Message, "Assert 01");
            Assert.AreEqual("A application exception.", exception2.Message, "Assert 02");
            Assert.AreEqual("A argument exception.", exception3.Message, "Assert 03");
            Assert.AreEqual("A division by zero exception.", exception4.Message, "Assert 04");
            Assert.AreEqual("Attempted to divide by zero.", exception5.Message, "Assert 05"); // exception default message
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Exceptions_Throws_FailsOnNoException()
        {
            // arrange
            List<Action> lstAction = new List<Action>()
            {
                () => { return; },
                () => { int a = 10, b = 10, c = a / b; },
                () => Substitute.For<IServiceProvider>().ToString(),
            };

            foreach (Action action in lstAction)
            {
                try
                {
                    // act
                    AssertEx.Exceptions.Throws<Exception>(action);

                    // assert
                    Assert.Fail("TestHelper.AssertThrows() lack of exception was not properly validated.");
                }
                catch (AssertFailedExceptionEx ex)  // expected exception
                {
                    Assert.AreEqual(AssertFailedExceptionCause.NoExceptionThrown, ex.AssertFailedExceptionCause);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Exceptions_Throws_FailsOnDiferentException()
        {
            // arrange
            List<Action> lstAction = new List<Action>()
            {
                () => { int a = 10, b = 0, c = a / b; },                    // DivideByZeroException
                () => Substitute.For<System.Text.StringBuilder>().Clear()   // TypeLoadException
            };

            foreach (Action action in lstAction)
            {
                try
                {
                    // act
                    AssertEx.Exceptions.Throws<Exception>(action);

                    // assert
                    Assert.Fail("TestHelper.AssertThrows() lack of exception was not properly validated.");
                }
                catch (AssertFailedExceptionEx ex)  // expected exception
                {
                    Assert.AreEqual(AssertFailedExceptionCause.DiferentExceptionThrown, ex.AssertFailedExceptionCause);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }
    }
}
