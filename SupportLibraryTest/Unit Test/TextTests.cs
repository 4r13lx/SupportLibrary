using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Text;
using SupportLibrary.Testing;

namespace SupportLibraryTest
{
    /// <summary>
    /// Testing of Text namespace classes.
    /// </summary>
    [TestClass]
    public class TextTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_Split_Valid()
        {
            // arrange
            string testString1 = "This is a test string. This is another test string.";
            string testString2 = "";

            string[] expected1 = new string[] { "This is ", " test string. This is ", "nother test string." };
            string[] expected2 = new string[] { "This is a test string", " This is another test string" };
            string[] expected3 = new string[] { "Th", "s ", "s ", " t", "st str", "ng. Th", "s ", "s ", "n", "th", "r t", "st str", "ng." };
            string[] expected4 = new string[] { };

            // act
            string[] result1 = testString1.Split("a");
            string[] result2 = testString1.Split(".");
            string[] result3 = testString1.Split("a", "e", "i", "o", "u");
            string[] result4 = testString2.Split("a", "b");

            // assert
            CollectionAssert.AreEqual(expected1, result1, "Assert 01");
            CollectionAssert.AreEqual(expected2, result2, "Assert 02");
            CollectionAssert.AreEqual(expected3, result3, "Assert 03");
            CollectionAssert.AreEqual(expected4, result4, "Assert 04");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        [ExpectedException(typeof(NullReferenceException))]
        public void Extensions_Split_Invalid()
        {
            // arrange
            string testString1 = null;

            // act 
            string[] result1 = testString1.Split("a", "b");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_Truncate_Valid()
        {
            // arrange
            string testString1 = "This is a test string. This is another test string.";
            string testString2 = "";

            string expected1 = "This is a test string. This is another test string.";
            string expected2 = "This is a test";

            // act
            string result1 = testString1.Truncate(1000);
            string result2 = testString1.Truncate(14);
            string result3 = testString2.Truncate(10);

            // assert
            Assert.AreEqual(expected1, result1, "Assert 01");
            Assert.AreEqual(expected2, result2, "Assert 02");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_Truncate_Invalid()
        {
            // arrange
            string testString1 = "This is a test string. This is another test string.";
            string testString2 = null;

            Action action1 = () => testString1.Truncate(-10);
            Action action2 = () => testString2.Truncate(10);

            // act & assert
            ArgumentOutOfRangeException exception1 = TestHelper.AssertThrows<ArgumentOutOfRangeException>(action1, "Assert 01");
            NullReferenceException exception2 = TestHelper.AssertThrows<NullReferenceException>(action2, "Assert 02");
        }
    }
}
