using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Testing;
using SupportLibrary.Security;

namespace SupportLibraryTest.Unit_Tests.Security
{
    /// <summary>
    /// Testing of Math namespace classes.
    /// </summary>
    [TestClass]
    public class CryptographyTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Security")]
        public void Cryptography_ComputeHashSHA1()
        {
            // arrange
            string input = "6g916kmny5";
            string expected = "0280A627AD4849191950DB2DB42D6E5498F2704E";

            // act
            string hash = Cryptography.ComputeHashSHA1(input);

            // assert
            Assert.AreEqual(expected, hash, "Assert 01");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Security")]
        public void Cryptography_ComputeHashSHA1_FailsOnNullArgument()
        {
            // arrange
            Action action1 = () => Cryptography.ComputeHashSHA1(null);
            Action action2 = () => Cryptography.ComputeHashSHA1("");

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Security")]
        public void Cryptography_ComputeHashMD5()
        {
            // arrange
            string input = "6g916kmny5";
            string expected = "F6232B068A3B17C7AA7445B88E6766B8";
            
            // act
            string hash = Cryptography.ComputeHashMD5(input);

            // assert
            Assert.AreEqual(expected, hash, "Assert 01");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Security")]
        public void Cryptography_ComputeHashMD5_FailsOnNullArgument()
        {
            // arrange
            Action action1 = () => Cryptography.ComputeHashMD5(null);
            Action action2 = () => Cryptography.ComputeHashMD5("");

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
        }
    }
}
