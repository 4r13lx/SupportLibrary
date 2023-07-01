using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Email;
using SupportLibrary.Testing;

namespace SupportLibraryTest
{
    /// <summary>
    /// Testing of Email namespace classes.
    /// </summary>
    [TestClass]
    public class EmailTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Email")]
        public void EmailHelper_IsValidEmailAddress()
        {
            // arrange & act
            bool result01 = EmailHelper.IsValidEmailAddress("test@asd.com");                // good
            bool result02 = EmailHelper.IsValidEmailAddress("test@asd.com.ar");             // good
            bool result03 = EmailHelper.IsValidEmailAddress("test.test@asd.com");           // good
            bool result04 = EmailHelper.IsValidEmailAddress("test.test@asd.com.ar");        // good
            bool result05 = EmailHelper.IsValidEmailAddress("test@asd.asd.com");            // good
            bool result06 = EmailHelper.IsValidEmailAddress("test@asd.asd.com.ar");         // good
            bool result07 = EmailHelper.IsValidEmailAddress("@");                           // wrong, incomplete address
            bool result08 = EmailHelper.IsValidEmailAddress("@asd");                        // wrong, incomplete address
            bool result09 = EmailHelper.IsValidEmailAddress("@asd.com");                    // wrong, incomplete address
            bool result10 = EmailHelper.IsValidEmailAddress("test");                        // wrong, incomplete address
            bool result11 = EmailHelper.IsValidEmailAddress("test@");                       // wrong, incomplete address
            bool result12 = EmailHelper.IsValidEmailAddress("test@asd");                    // wrong, incomplete address
            bool result13 = EmailHelper.IsValidEmailAddress("test@asd.");                   // wrong, incomplete address
            bool result14 = EmailHelper.IsValidEmailAddress("te st@asd.com");               // wrong, incomplete address
            bool result15 = EmailHelper.IsValidEmailAddress("test.asd.com");                // wrong, incomplete address
            bool result16 = EmailHelper.IsValidEmailAddress("test test@asd.com");           // wrong, spaces are not allowed
            bool result17 = EmailHelper.IsValidEmailAddress("te#st@asd.com");               // wrong, invalid character '#'
            bool result18 = EmailHelper.IsValidEmailAddress("tést@asd.com");                // wrong, invalid character 'é'
            bool result19 = EmailHelper.IsValidEmailAddress("");                            // wrong, empty string
            bool result20 = EmailHelper.IsValidEmailAddress(null);                          // wrong, null value

            // assert
            Assert.AreEqual(true, result01, "Assert 01");
            Assert.AreEqual(true, result02, "Assert 02");
            Assert.AreEqual(true, result03, "Assert 03");
            Assert.AreEqual(true, result04, "Assert 04");
            Assert.AreEqual(true, result05, "Assert 05");
            Assert.AreEqual(true, result06, "Assert 06");
            Assert.AreEqual(false, result07, "Assert 07");
            Assert.AreEqual(false, result08, "Assert 08");
            Assert.AreEqual(false, result09, "Assert 09");
            Assert.AreEqual(false, result10, "Assert 10");
            Assert.AreEqual(false, result11, "Assert 11");
            Assert.AreEqual(false, result12, "Assert 12");
            Assert.AreEqual(false, result13, "Assert 13");
            Assert.AreEqual(false, result14, "Assert 14");
            Assert.AreEqual(false, result15, "Assert 15");
            Assert.AreEqual(false, result16, "Assert 16");
            Assert.AreEqual(false, result17, "Assert 17");
            Assert.AreEqual(false, result17, "Assert 18");
            Assert.AreEqual(false, result17, "Assert 19");
            Assert.AreEqual(false, result17, "Assert 20");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Email")]
        public void EmailHelper_SplitAddresses_Valid()
        {
            // arrange
            string input1 = "asd@test.com";
            string input2 = "asd@test.com;";
            string input3 = "asd@test.com ,; qwerty@test.com";
            string input4 = "asd@test.com ; ; qwerty@test.com";
            string input5 = "asd@test.com, qwerty@test.com; zxcv@test.com";
            string input6 = "asd@test.com| qwerty@test.com | zxcv@test.com";
            string input7 = "asd@test.com # qwerty@test.com |zxcv@test.com";

            EmailHelper.SplitAddresses(input1);

            List<string> listExpected1 = new List<string>() { "asd@test.com" };
            List<string> listExpected2 = new List<string>() { "asd@test.com", "qwerty@test.com" };
            List<string> listExpected3 = new List<string>() { "asd@test.com", "qwerty@test.com", "zxcv@test.com" };

            // act
            List<string> listResults1 = EmailHelper.SplitAddresses(input1);
            List<string> listResults2 = EmailHelper.SplitAddresses(input2);
            List<string> listResults3 = EmailHelper.SplitAddresses(input3);
            List<string> listResults4 = EmailHelper.SplitAddresses(input4);
            List<string> listResults5 = EmailHelper.SplitAddresses(input5);
            List<string> listResults6 = EmailHelper.SplitAddresses(input6, '|');
            List<string> listResults7 = EmailHelper.SplitAddresses(input7, new char[] { '|', '#' });

            // assert
            CollectionAssert.AreEqual(listExpected1, listResults1, "Assert 01");
            CollectionAssert.AreEqual(listExpected1, listResults2, "Assert 02");
            CollectionAssert.AreEqual(listExpected2, listResults3, "Assert 03");
            CollectionAssert.AreEqual(listExpected2, listResults4, "Assert 04");
            CollectionAssert.AreEqual(listExpected3, listResults5, "Assert 05");
            CollectionAssert.AreEqual(listExpected3, listResults6, "Assert 06");
            CollectionAssert.AreEqual(listExpected3, listResults7, "Assert 07");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Email")]
        public void EmailHelper_SplitAddresses_Invalid()
        {
            try
            {
                // arrange
                string input1 = "asd@test.com, qwerty@test.com";                    // valid text (with default delimiters)
                string input2 = "asd@test.com @ qwerty@test.com";                   // invalid text (with default delimiters)
                string input3 = "asd@test.com . qwerty@test.com";                   // invalid text (with default delimiters)

                Action action1 = () => EmailHelper.SplitAddresses(input1, '*');
                Action action2 = () => EmailHelper.SplitAddresses(input2);
                Action action3 = () => EmailHelper.SplitAddresses(input2, '@');
                Action action4 = () => EmailHelper.SplitAddresses(input3);
                Action action5 = () => EmailHelper.SplitAddresses(input3, '.');

                // act
                ArgumentException exception1 = TestHelper.AssertThrows<ArgumentException>(action1, "Assert 01");
                ArgumentException exception2 = TestHelper.AssertThrows<ArgumentException>(action2, "Assert 02");
                ArgumentException exception3 = TestHelper.AssertThrows<ArgumentException>(action3, "Assert 03");
                ArgumentException exception4 = TestHelper.AssertThrows<ArgumentException>(action4, "Assert 04");
                ArgumentException exception5 = TestHelper.AssertThrows<ArgumentException>(action5, "Assert 05");

                // assert
                Assert.AreEqual("text", exception1.ParamName);
                Assert.AreEqual("text", exception2.ParamName);
                Assert.AreEqual("text", exception3.ParamName);
                Assert.AreEqual("text", exception4.ParamName);
                Assert.AreEqual("text", exception5.ParamName);
            }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }
    }
}
