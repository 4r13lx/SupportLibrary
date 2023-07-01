using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupportLibrary.Core;
using SupportLibrary.Testing;

using SupportLibraryTest.Entities;

namespace SupportLibraryTest.Core
{
    /// <summary>
    /// Testing of Core namespace classes.
    /// </summary>
    [TestClass]
    public class EnumerationExtensionsTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Enumeration")]
        public void Extensions_GetValue()
        {
            // arrange
            TestEnumSimple testEnum1 = TestEnumSimple.Value1;
            TestEnumSimple testEnum2 = TestEnumSimple.Value2;
            TestEnumSimple testEnum3 = TestEnumSimple.Value3;
            TestEnumSimple testEnum4 = TestEnumSimple.Unknown;
            TestEnumSimple testEnum5 = (TestEnumSimple)20;

            // act
            int value1 = testEnum1.GetValue();
            int value2 = testEnum2.GetValue();
            int value3 = testEnum3.GetValue();
            int value4 = testEnum4.GetValue();
            int value5 = testEnum5.GetValue();

            // assert
            Assert.AreEqual(1, value1);
            Assert.AreEqual(2, value2);
            Assert.AreEqual(3, value3);
            Assert.AreEqual(0, value4);
            Assert.AreEqual(20, value5);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Enumeration")]
        public void Extensions_GetDescription()
        {
            // arrange
            TestEnumDescription testEnum1 = TestEnumDescription.Value1;
            TestEnumDescription testEnum2 = TestEnumDescription.Value2;
            TestEnumDescription testEnum3 = TestEnumDescription.Value3;
            TestEnumDescription testEnum4 = TestEnumDescription.Unknown;

            // act
            string value1 = testEnum1.GetDescription();
            string value2 = testEnum2.GetDescription();
            string value3 = testEnum3.GetDescription();
            string value4 = testEnum4.GetDescription();

            // assert
            Assert.AreEqual("One", value1);
            Assert.AreEqual("Two", value2);
            Assert.AreEqual("Three", value3);
            Assert.AreEqual("Unknown", value4);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Enumeration")]
        public void Extensions_GetDescription_FailsOnNoDescription()
        {
            // arrange
            TestEnumSimple testEnum1 = TestEnumSimple.Value1;
            Action action = () => testEnum1.GetDescription();

            // act & assert
            ArgumentException ex = AssertEx.Exceptions.Throws<ArgumentException>(action);
        }
    }
}
