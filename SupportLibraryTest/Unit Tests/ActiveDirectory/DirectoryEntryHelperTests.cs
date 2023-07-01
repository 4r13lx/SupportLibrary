using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.ActiveDirectory;
using SupportLibrary.Text;

namespace SupportLibraryTest.ActiveDirectory
{
    /// <summary>
    /// Testing of ActiveDirectory namespace classes.<para/>
    /// </summary>
    [TestClass]
    public class DirectoryEntryHelperTests
    {
        string accountName = "accountname";

        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        public void DirectoryEntryHelper_GetProperty()
        {
            // arrange
            DirectoryEntry directoryEntry = new ActiveDirectoryHelper().FindDirectoryEntry(accountName);

            // act
            string property1 = DirectoryEntryHelper.GetProperty<string>(directoryEntry, DirectoryEntryProperty.PrincipalName);
            string property2 = DirectoryEntryHelper.GetProperty<string>(directoryEntry, DirectoryEntryProperty.Email);
            string property3 = Convert.ToString(DirectoryEntryHelper.GetProperty(directoryEntry, DirectoryEntryProperty.PrincipalName));
            string property4 = Convert.ToString(DirectoryEntryHelper.GetProperty(directoryEntry, DirectoryEntryProperty.Email));

            // assert
            Assert.IsTrue(property1.IsNotNullOrEmpty(), "Assert 01");
            Assert.IsTrue(property2.IsNotNullOrEmpty(), "Assert 02");
            Assert.IsTrue(property3.IsNotNullOrEmpty(), "Assert 03");
            Assert.IsTrue(property4.IsNotNullOrEmpty(), "Assert 04");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        [ExpectedException(typeof(InvalidCastException))]
        public void DirectoryEntryHelper_GetProperty_FailsOnInvalidProperty()
        {
            // arrange
            DirectoryEntry directoryEntry = new ActiveDirectoryHelper().FindDirectoryEntry(accountName);

            // act & assert
            object property = DirectoryEntryHelper.GetProperty(directoryEntry, DirectoryEntryProperty.None);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        [ExpectedException(typeof(InvalidCastException))]
        public void DirectoryEntryHelper_GetProperty_FailsOnInvalidGenericsType()
        {
            // arrange
            DirectoryEntry directoryEntry = new ActiveDirectoryHelper().FindDirectoryEntry(accountName);

            // act & assert
            DateTime property = DirectoryEntryHelper.GetProperty<DateTime>(directoryEntry, DirectoryEntryProperty.PrincipalName);
        }
    }
}
