using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Testing;

namespace SupportLibraryTest.Testing
{
    /// <summary>
    /// Testing of Testing namespace classes.
    /// </summary>
    [TestClass]
    public class AssertEx_ConditionTests
    {
        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Condition_Greater()
        {
            AssertEx.Condition.Greater(2, 1);
            AssertEx.Condition.Greater<int>(2, 1);
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Condition_Greater_Fails()
        {
            // arrange
            Action action1 = () => AssertEx.Condition.Greater(1, 1);
            Action action2 = () => AssertEx.Condition.Greater(1, 2);

            // act & assert
            AssertFailedException exception1 = AssertEx.Exceptions.Throws<AssertFailedException>(action1, "Assert 01");
            AssertFailedException exception2 = AssertEx.Exceptions.Throws<AssertFailedException>(action2, "Assert 02"); 
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Condition_GreaterOrEqual()
        {
            AssertEx.Condition.GreaterOrEqual(2, 1);
            AssertEx.Condition.GreaterOrEqual<int>(2, 1);
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Condition_GreaterOrEqual_Fails()
        {
            // arrange
            Action action1 = () => AssertEx.Condition.GreaterOrEqual(1.0, 1.1);
            Action action2 = () => AssertEx.Condition.GreaterOrEqual(1, 2);

            // act & assert
            AssertFailedException exception1 = AssertEx.Exceptions.Throws<AssertFailedException>(action1, "Assert 01");
            AssertFailedException exception2 = AssertEx.Exceptions.Throws<AssertFailedException>(action2, "Assert 02");
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Condition_Less()
        {
            AssertEx.Condition.Less(1, 2);
            AssertEx.Condition.Less<int>(1, 2);
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Condition_Less_Fails()
        {
            // arrange
            Action action1 = () => AssertEx.Condition.Less(1, 1);
            Action action2 = () => AssertEx.Condition.Less(2, 1);

            // act & assert
            AssertFailedException exception1 = AssertEx.Exceptions.Throws<AssertFailedException>(action1, "Assert 01");
            AssertFailedException exception2 = AssertEx.Exceptions.Throws<AssertFailedException>(action2, "Assert 02");
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Condition_LessOrEqual()
        {
            AssertEx.Condition.LessOrEqual(1, 2);
            AssertEx.Condition.LessOrEqual<int>(1, 2);
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Condition_LessOrEqual_Fails()
        {
            // arrange
            Action action1 = () => AssertEx.Condition.LessOrEqual(1.1, 1.0);
            Action action2 = () => AssertEx.Condition.LessOrEqual(2, 1);

            // act & assert
            AssertFailedException exception1 = AssertEx.Exceptions.Throws<AssertFailedException>(action1, "Assert 01");
            AssertFailedException exception2 = AssertEx.Exceptions.Throws<AssertFailedException>(action2, "Assert 02");
        }
    }
}
