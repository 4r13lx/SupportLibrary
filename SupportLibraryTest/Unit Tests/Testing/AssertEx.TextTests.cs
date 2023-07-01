using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Testing;

namespace SupportLibraryTest.Testing
{
    /// <summary>
    /// Testing of Testing namespace classes.
    /// </summary>
    [TestClass]
    public class AssertEx_TextTests
    {
        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Text_IsEmpty()
        {
            AssertEx.Text.IsEmpty("");
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        [ExpectedException(typeof(AssertFailedException))]
        public void AssertEx_Text_IsEmpty_FailsOnString()
        {
            AssertEx.Text.IsEmpty("This is a test string.");
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        [ExpectedException(typeof(NullReferenceException))]
        public void AssertEx_Text_IsEmpty_FailsOnNullString()
        {
            AssertEx.Text.IsEmpty((string)null);
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Text_IsNotEmpty()
        {
            AssertEx.Text.IsNotEmpty("This is a test string.");
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        [ExpectedException(typeof(AssertFailedException))]
        public void AssertEx_Text_IsNotEmpty_FailsOnEmptyString()
        {
            AssertEx.Text.IsNotEmpty("");
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Text_AreEqualIgnoringCase()
        {
            AssertEx.Text.AreEqualIgnoringCase("mmc", "MMC");
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        [ExpectedException(typeof(AssertFailedException))]
        public void AssertEx_Text_AreEqualIgnoringCase_Fails()
        {
            AssertEx.Text.AreEqualIgnoringCase("MMC", "MMC String");
        }
    }
}
