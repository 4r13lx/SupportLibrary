using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.ActiveDirectory;
using SupportLibrary.Text;
using SupportLibrary.Testing;

namespace SupportLibraryTest.ActiveDirectory
{
    /// <summary>
    /// Testing of ActiveDirectory namespace classes.<para/>
    /// </summary>
    [TestClass]
    public class ActiveDirectoryHelperTests
    {
        string accountName = "accountname";

        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        public void ActiveDirectoryHelper_Constructor()
        {
            // arrange
            string defaultPath = "LDAP://domain.name.com", otherPath = "LDAP://test.domain.name.com";

            // act
            ActiveDirectoryHelper activeDirectoryHelper1 = new ActiveDirectoryHelper();
            ActiveDirectoryHelper activeDirectoryHelper2 = new ActiveDirectoryHelper(otherPath);
            ActiveDirectoryHelper activeDirectoryHelper3 = new ActiveDirectoryHelper(true);
            ActiveDirectoryHelper activeDirectoryHelper4 = new ActiveDirectoryHelper(otherPath, true);

            // assert
            Assert.AreEqual(defaultPath, activeDirectoryHelper1.ActiveDirectoryPath, "Assert 01");
            Assert.AreEqual(false, activeDirectoryHelper1.RevertImpersonation, "Assert 02");

            Assert.AreEqual(otherPath, activeDirectoryHelper2.ActiveDirectoryPath, "Assert 03");
            Assert.AreEqual(false, activeDirectoryHelper2.RevertImpersonation, "Assert 04");

            Assert.AreEqual(defaultPath, activeDirectoryHelper3.ActiveDirectoryPath, "Assert 05");
            Assert.AreEqual(true, activeDirectoryHelper3.RevertImpersonation, "Assert 06");

            Assert.AreEqual(otherPath, activeDirectoryHelper4.ActiveDirectoryPath, "Assert 07");
            Assert.AreEqual(true, activeDirectoryHelper4.RevertImpersonation, "Assert 08");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        public void ActiveDirectoryHelper_FindDirectoryEntry()
        {
            // act
            DirectoryEntry directoryEntry = new ActiveDirectoryHelper().FindDirectoryEntry(accountName);

            // assert
            Assert.IsNotNull(directoryEntry, "Assert 01");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        [ExpectedException(typeof(DirectoryServicesCOMException))]
        public void ActiveDirectoryHelper_FindDirectoryEntry_FailsOnInvalidServer()
        {
            // act
            DirectoryEntry directoryEntry = new ActiveDirectoryHelper("LDAP://nonexist.domain.name.net").FindDirectoryEntry(accountName);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        [ExpectedException(typeof(ActiveDirectoryObjectNotFoundException))]
        public void ActiveDirectoryHelper_FindDirectoryEntry_FailsOnInvalidAccountName()
        {
            // act
            DirectoryEntry directoryEntry = new ActiveDirectoryHelper().FindDirectoryEntry("noexist");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        public void ActiveDirectoryHelper_GetProperty()
        {
            // act
            string property1 = new ActiveDirectoryHelper().GetProperty<string>(accountName, DirectoryEntryProperty.Email);
            string property2 = new ActiveDirectoryHelper().GetProperty<string>(accountName, "mail");
            string property3 = new ActiveDirectoryHelper().GetProperty(accountName, DirectoryEntryProperty.Email).ToString();
            string property4 = new ActiveDirectoryHelper().GetProperty(accountName, "mail").ToString();

            // assert
            Assert.IsTrue(property1.IsNotNullOrEmpty(), "Assert 01");
            Assert.IsTrue(property2.IsNotNullOrEmpty(), "Assert 02");
            Assert.IsTrue(property3.IsNotNullOrEmpty(), "Assert 03");
            Assert.IsTrue(property4.IsNotNullOrEmpty(), "Assert 04");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        public void ActiveDirectoryHelper_GetProperty_FailsOnInvalidProperty()
        {
            // arrange
            Action action1 = () => new ActiveDirectoryHelper().GetProperty<string>(accountName, DirectoryEntryProperty.None);
            Action action2 = () => new ActiveDirectoryHelper().GetProperty<string>(accountName, "noexist");

            // act & assert
            InvalidCastException exception1 = AssertEx.Exceptions.Throws<InvalidCastException>(action1);
            ActiveDirectoryObjectNotFoundException exception2 = AssertEx.Exceptions.Throws<ActiveDirectoryObjectNotFoundException>(action2);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        public void ActiveDirectoryHelper_GetProperty_FailsOnInvalidGenericsType()
        {
            // arrange
            Action action1 = () => new ActiveDirectoryHelper().GetProperty<DateTime>(accountName, DirectoryEntryProperty.Email);
            Action action2 = () => new ActiveDirectoryHelper().GetProperty<DateTime>(accountName, "mail");

            // act & assert
            InvalidCastException exception1 = AssertEx.Exceptions.Throws<InvalidCastException>(action1);
            InvalidCastException exception2 = AssertEx.Exceptions.Throws<InvalidCastException>(action2);
        }
    }
}
