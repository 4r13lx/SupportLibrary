using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.RegularExpressions.PasswordValidation;
using SupportLibrary.Testing;

namespace SupportLibraryTest.RegularExpressions
{
    /// <summary>
    /// Testing of RegularExpressions namespace classes.
    /// </summary>
    [TestClass]
    public class PasswordValidatorTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void PasswordValidator_Validate_Full_GoodPassword()
        {
            // arrange
            PasswordValidator pv = new PasswordValidator("abmldq3421!");
            pv.Validations.SetMinLenght(8);
            pv.Validations.SetMaxLenght(50);
            pv.Validations.SetMinNumericChars(1);
            pv.Validations.SetMinSpecialChars(1);
            pv.Validations.SetMaxConsecutiveChars(5);
            pv.Validations.SetMaxIncrementalChars(3);
            pv.Validations.SetMaxDecrementalChars(3);

            // act
            bool result = pv.Validate();
            ValidationResult resultVerbose = pv.ValidateVerbose();

            // assert
            Assert.IsTrue(result, "Assert 01");
            Assert.IsTrue(resultVerbose.Result, "Assert 02");
            Assert.AreEqual(0, resultVerbose.WithErrors.Count, "Assert 03");
            Assert.AreEqual("", resultVerbose.DetailedMessage, "Assert 04");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void PasswordValidator_Validate_Full_WrongPassword()
        {
            // arrange
            PasswordValidator pv = new PasswordValidator("abcdzyx");
            pv.Validations.SetMinLenght(8);
            pv.Validations.SetMinNumericChars(1);
            pv.Validations.SetMinSpecialChars(1);
            pv.Validations.SetMaxIncrementalChars(3);
            pv.Validations.SetMaxDecrementalChars(2);

            // act
            bool result = pv.Validate();
            ValidationResult resultVerbose = pv.ValidateVerbose();

            // assert
            Assert.IsFalse(result, "Assert 01");
            Assert.IsFalse(resultVerbose.Result, "Assert 02");
            Assert.AreEqual(5, resultVerbose.WithErrors.Count, "Assert 03");
            Assert.AreNotEqual("", resultVerbose.DetailedMessage, "Assert 04");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void PasswordValidator_Validate_SetMinLenght()
        {
            // arrange
            string goodPassword = "abcdef", wrongPassword = "abc";

            PasswordValidator pv = new PasswordValidator();
            pv.Validations.SetMinLenght(6);

            // act
            pv.Password = goodPassword;
            ValidationResult resultGoodPassword = pv.ValidateVerbose();

            pv.Password = wrongPassword;
            ValidationResult resultWrongPassword = pv.ValidateVerbose();

            // assert
            Assert.IsTrue(resultGoodPassword.Result, "Assert 01");
            Assert.IsFalse(resultWrongPassword.Result, "Assert 02");
            Assert.IsTrue(resultWrongPassword.WithErrors.Contains(ValidationType.MinLenght), "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void PasswordValidator_Validate_SetMaxLenght()
        {
            // arrange
            string goodPassword = "abcdef", wrongPassword = "abcdefghijkl";

            PasswordValidator pv = new PasswordValidator();
            pv.Validations.SetMaxLenght(10);

            // act
            pv.Password = goodPassword;
            ValidationResult resultGoodPassword = pv.ValidateVerbose();

            pv.Password = wrongPassword;
            ValidationResult resultWrongPassword = pv.ValidateVerbose();

            // assert
            Assert.IsTrue(resultGoodPassword.Result, "Assert 01");
            Assert.IsFalse(resultWrongPassword.Result, "Assert 02");
            Assert.IsTrue(resultWrongPassword.WithErrors.Contains(ValidationType.MaxLenght), "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void PasswordValidator_Validate_SetMinNumericChars()
        {
            // arrange
            string goodPassword = "1abc2def3", wrongPassword = "1abcdef2";

            PasswordValidator pv = new PasswordValidator();
            pv.Validations.SetMinNumericChars(3);

            // act
            pv.Password = goodPassword;
            ValidationResult resultGoodPassword = pv.ValidateVerbose();

            pv.Password = wrongPassword;
            ValidationResult resultWrongPassword = pv.ValidateVerbose();

            // assert
            Assert.IsTrue(resultGoodPassword.Result, "Assert 01");
            Assert.IsFalse(resultWrongPassword.Result, "Assert 02");
            Assert.IsTrue(resultWrongPassword.WithErrors.Contains(ValidationType.MinNumericChars), "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void PasswordValidator_Validate_SetMinSpecialChars()
        {
            // arrange
            string goodPassword = "abc@def#", wrongPassword = "abcdef!";

            PasswordValidator pv = new PasswordValidator();
            pv.Validations.SetMinSpecialChars(2);

            // act
            pv.Password = goodPassword;
            ValidationResult resultGoodPassword = pv.ValidateVerbose();

            pv.Password = wrongPassword;
            ValidationResult resultWrongPassword = pv.ValidateVerbose();

            // assert
            Assert.IsTrue(resultGoodPassword.Result, "Assert 01");
            Assert.IsFalse(resultWrongPassword.Result, "Assert 02");
            Assert.IsTrue(resultWrongPassword.WithErrors.Contains(ValidationType.MinSpecialChars), "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void PasswordValidator_Validate_SetMaxConsecutiveChars()
        {
            // arrange
            string goodPassword = "abcccdef", wrongPassword = "abccccdef";

            PasswordValidator pv = new PasswordValidator();
            pv.Validations.SetMaxConsecutiveChars(3);

            // act
            pv.Password = goodPassword;
            ValidationResult resultGoodPassword = pv.ValidateVerbose();

            pv.Password = wrongPassword;
            ValidationResult resultWrongPassword = pv.ValidateVerbose();

            // assert
            Assert.IsTrue(resultGoodPassword.Result, "Assert 01");
            Assert.IsFalse(resultWrongPassword.Result, "Assert 02");
            Assert.IsTrue(resultWrongPassword.WithErrors.Contains(ValidationType.MaxConsecutiveChars), "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void PasswordValidator_Validate_SetMaxIncrementalChars()
        {
            // arrange
            string goodPassword = "abc1def", wrongPassword = "abcd1ef";

            PasswordValidator pv = new PasswordValidator();
            pv.Validations.SetMaxIncrementalChars(3);

            // act
            pv.Password = goodPassword;
            ValidationResult resultGoodPassword = pv.ValidateVerbose();

            pv.Password = wrongPassword;
            ValidationResult resultWrongPassword = pv.ValidateVerbose();

            // assert
            Assert.IsTrue(resultGoodPassword.Result, "Assert 01");
            Assert.IsFalse(resultWrongPassword.Result, "Assert 02");
            Assert.IsTrue(resultWrongPassword.WithErrors.Contains(ValidationType.MaxIncrementalChars), "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void PasswordValidator_Validate_SetMaxDecrementalChars()
        {
            // arrange
            string goodPassword = "abcdef987", wrongPassword = "abcdef9876";

            PasswordValidator pv = new PasswordValidator();
            pv.Validations.SetMaxDecrementalChars(3);

            // act
            pv.Password = goodPassword;
            ValidationResult resultGoodPassword = pv.ValidateVerbose();

            pv.Password = wrongPassword;
            ValidationResult resultWrongPassword = pv.ValidateVerbose();

            // assert
            Assert.IsTrue(resultGoodPassword.Result, "Assert 01");
            Assert.IsFalse(resultWrongPassword.Result, "Assert 02");
            Assert.IsTrue(resultWrongPassword.WithErrors.Contains(ValidationType.MaxDecrementalChars), "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void PasswordValidator_Validate_FailsOnNullPassword()
        {
            // arrange
            PasswordValidator pv = new PasswordValidator(null);
            pv.Validations.SetMinLenght(6);
            Action action = () => pv.Validate();

            // act & assert
            ArgumentNullException ex = AssertEx.Exceptions.Throws<ArgumentNullException>(action);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void PasswordValidator_Validate_FailsOnEmptyValidations()
        {
            // arrange
            PasswordValidator pv = new PasswordValidator("abcd1234");
            Action action = () => pv.Validate();

            // act & assert
            ArgumentNullException ex = AssertEx.Exceptions.Throws<ArgumentNullException>(action);
        }
    }
}
