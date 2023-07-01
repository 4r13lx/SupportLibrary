using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Text;
using SupportLibrary.Testing;

namespace SupportLibraryTest.Text
{
    /// <summary>
    /// Testing of Text namespace classes.
    /// </summary>
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_IsNullOrEmpty()
        {
            // arrange
            string testString1 = "This is a test string. This is another test string.";
            string testString2 = "";
            string testString3 = null;

            // act
            bool result1 = testString1.IsNullOrEmpty();
            bool result2 = testString2.IsNullOrEmpty();
            bool result3 = testString3.IsNullOrEmpty();

            // assert
            Assert.AreEqual(false, result1, "Assert 01");
            Assert.AreEqual(true, result2, "Assert 02");
            Assert.AreEqual(true, result3, "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_IsNotNullOrEmpty()
        {
            // arrange
            string testString1 = "This is a test string. This is another test string.";
            string testString2 = "";
            string testString3 = null;

            // act
            bool result1 = testString1.IsNotNullOrEmpty();
            bool result2 = testString2.IsNotNullOrEmpty();
            bool result3 = testString3.IsNotNullOrEmpty();

            // assert
            Assert.AreEqual(true, result1, "Assert 01");
            Assert.AreEqual(false, result2, "Assert 02");
            Assert.AreEqual(false, result3, "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_IsNumeric()
        {
            // arrange
            string testString1 = "1234567890";
            string testString2 = "This is a test string. This is another test string.";
            string testString3 = null;

            // act
            bool result1 = testString1.IsNumeric();
            bool result2 = testString2.IsNumeric();
            bool result3 = testString3.IsNumeric();

            // assert
            Assert.AreEqual(true, result1, "Assert 01");
            Assert.AreEqual(false, result2, "Assert 02");
            Assert.AreEqual(false, result3, "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_SafeSubstring()
        {
            // arrange
            string testString = "This is a test string. This is another test string.";

            // act
            string result1 = testString.SafeSubstring(0, 1000);
            string result2 = testString.SafeSubstring(23, 1000);

            // assert
            Assert.AreEqual(testString, result1, "Assert 01");
            Assert.AreEqual("This is another test string.", result2, "Assert 02");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_SafeSubstring_FailsOnInvalidArgs()
        {
            // arrange
            string testString = null;

            Action action1 = () => testString.SafeSubstring(10, 100);

            // act & assert
            AssertEx.Exceptions.Throws<ArgumentNullException>(action1, "Assert 01");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_Split()
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
        public void Extensions_Split_FailsOnNullArgument()
        {
            // arrange
            string testString = null;
            Action action = () => testString.Split("a", "b");

            // act
            AssertEx.Exceptions.Throws<NullReferenceException>(action);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_SubstringByRange()
        {
            // arrange
            string testString = "This is a test string. This is another test string.";

            // act
            string result1 = testString.SubstringByRange(0, 0);
            string result2 = testString.SubstringByRange(0, testString.Length - 1);
            string result3 = testString.SubstringByRange(0, 1);
            string result4 = testString.SubstringByRange(0, 13);
            string result5 = testString.SubstringByRange(23, 37);
            string result6 = testString.SubstringByRange(5, testString.Length - 1);

            // assert
            Assert.AreEqual("T", result1, "Assert 01");
            Assert.AreEqual(testString, result2, "Assert 02");
            Assert.AreEqual("Th", result3, "Assert 03");
            Assert.AreEqual("This is a test", result4, "Assert 04");
            Assert.AreEqual("This is another", result5, "Assert 05");
            Assert.AreEqual("is a test string. This is another test string.", result6, "Assert 06");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_SubstringByRange_FailsOnInvalidArgs()
        {
            // arrange
            string testString1 = null;
            string testString2 = "This is a test string. This is another test string.";

            Action action1 = () => testString1.SubstringByRange(0, 10);
            Action action2 = () => testString2.SubstringByRange(10, 5);
            Action action3 = () => testString2.SubstringByRange(-10, 5);
            Action action4 = () => testString2.SubstringByRange(0, 100);

            // act & assert
            NullReferenceException ex1 = AssertEx.Exceptions.Throws<NullReferenceException>(action1, "Assert 01");
            ArgumentOutOfRangeException ex2 = AssertEx.Exceptions.Throws<ArgumentOutOfRangeException>(action2, "Assert 02");
            ArgumentOutOfRangeException ex3 = AssertEx.Exceptions.Throws<ArgumentOutOfRangeException>(action3, "Assert 03");
            ArgumentOutOfRangeException ex4 = AssertEx.Exceptions.Throws<ArgumentOutOfRangeException>(action4, "Assert 04");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Text")]
        public void Extensions_Truncate()
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
        public void Extensions_Truncate_FailsOnInvalidArgs()
        {
            // arrange
            string testString1 = null;
            string testString2 = "This is a test string. This is another test string.";

            Action action1 = () => testString1.Truncate(10);
            Action action2 = () => testString2.Truncate(-10);

            // act & assert
            AssertEx.Exceptions.Throws<ArgumentNullException>(action1, "Assert 01");
            AssertEx.Exceptions.Throws<ArgumentOutOfRangeException>(action2, "Assert 02");
        }
    }
}
