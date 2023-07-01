using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.ActiveDirectory;
using SupportLibrary.Testing;

namespace SupportLibraryTest.ActiveDirectory
{
    /// <summary>
    /// Testing of ActiveDirectory namespace classes.
    /// </summary>
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        public void Extensions_ToPropertyName()
        {
            // arrange & act
            string propertyName01 = DirectoryEntryProperty.CompleteName.ToPropertyName();
            string propertyName02 = DirectoryEntryProperty.FirstName.ToPropertyName();
            string propertyName03 = DirectoryEntryProperty.LastName.ToPropertyName();
            string propertyName04 = DirectoryEntryProperty.Name.ToPropertyName();
            string propertyName05 = DirectoryEntryProperty.DisplayName.ToPropertyName();
            string propertyName06 = DirectoryEntryProperty.Description.ToPropertyName();
            string propertyName07 = DirectoryEntryProperty.AccountName.ToPropertyName();
            string propertyName08 = DirectoryEntryProperty.PrincipalName.ToPropertyName();
            string propertyName09 = DirectoryEntryProperty.TelephoneNumber.ToPropertyName();
            string propertyName10 = DirectoryEntryProperty.ExtensionNumber.ToPropertyName();
            string propertyName11 = DirectoryEntryProperty.Email.ToPropertyName();
            string propertyName12 = DirectoryEntryProperty.EmailNickname.ToPropertyName();
            string propertyName13 = DirectoryEntryProperty.HomeDirectory.ToPropertyName();
            string propertyName14 = DirectoryEntryProperty.HomeDrive.ToPropertyName();
            string propertyName15 = DirectoryEntryProperty.Department1.ToPropertyName();
            string propertyName16 = DirectoryEntryProperty.Department2.ToPropertyName();
            string propertyName17 = DirectoryEntryProperty.Department3.ToPropertyName();
            string propertyName18 = DirectoryEntryProperty.Title.ToPropertyName();

            // assert
            Assert.AreEqual("cn", propertyName01, "Assert 01");
            Assert.AreEqual("givenName", propertyName02, "Assert 02");
            Assert.AreEqual("sn", propertyName03, "Assert 03");
            Assert.AreEqual("name", propertyName04, "Assert 04");
            Assert.AreEqual("displayName", propertyName05, "Assert 05");
            Assert.AreEqual("description", propertyName06, "Assert 06");
            Assert.AreEqual("sAMAccountName", propertyName07, "Assert 07");
            Assert.AreEqual("userPrincipalName", propertyName08, "Assert 08");
            Assert.AreEqual("telephoneNumber", propertyName09, "Assert 09");
            Assert.AreEqual("ipPhone", propertyName10, "Assert 10");
            Assert.AreEqual("mail", propertyName11, "Assert 11");
            Assert.AreEqual("mailNickname", propertyName12, "Assert 12");
            Assert.AreEqual("homeDirectory", propertyName13, "Assert 13");
            Assert.AreEqual("homeDrive", propertyName14, "Assert 14");
            Assert.AreEqual("department", propertyName15, "Assert 15");
            Assert.AreEqual("extensionAttribute6", propertyName16, "Assert 16");
            Assert.AreEqual("extensionAttribute7", propertyName17, "Assert 17");
            Assert.AreEqual("extensionAttribute8", propertyName18, "Assert 18");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "ActiveDirectory")]
        public void Extensions_ToPropertyName_Fails()
        {
            // arrange
            Action action = () => DirectoryEntryProperty.None.ToPropertyName();

            // act & assert
            InvalidCastException exception = AssertEx.Exceptions.Throws<InvalidCastException>(action);
            Assert.IsTrue(exception.Message.Contains(DirectoryEntryProperty.None.ToString()));
        }
    }
}
