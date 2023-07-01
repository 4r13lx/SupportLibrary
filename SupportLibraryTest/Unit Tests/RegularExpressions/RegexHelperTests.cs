using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.RegularExpressions;
using SupportLibrary.Text;

namespace SupportLibraryTest.RegularExpressions
{
    /// <summary>
    /// Testing of RegularExpressions namespace classes.
    /// </summary>
    [TestClass]
    public class RegexHelperTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Business_IsValidBillOfLaddingNumber()
        {
            // arrange & act
            bool result01 = RegexHelper.Business.IsValidBillOfLaddingNumber("MMCAA1234567");    // good
            bool result02 = RegexHelper.Business.IsValidBillOfLaddingNumber("MMCAAA123456");    // good
            bool result03 = RegexHelper.Business.IsValidBillOfLaddingNumber("MMDUSH035637");    // good
            bool result04 = RegexHelper.Business.IsValidBillOfLaddingNumber("MMDUNI800560");    // good
            bool result05 = RegexHelper.Business.IsValidBillOfLaddingNumber("AAABBB123456");    // wrong, must start with 'MMC'
            bool result06 = RegexHelper.Business.IsValidBillOfLaddingNumber("MMC123456789");    // wrong, too few letters
            bool result07 = RegexHelper.Business.IsValidBillOfLaddingNumber("MMCA12345678");    // wrong, too few letters
            bool result08 = RegexHelper.Business.IsValidBillOfLaddingNumber("MMCAAAA12345");    // wrong, too many letters
            bool result09 = RegexHelper.Business.IsValidBillOfLaddingNumber("MMCAAA12345");     // wrong, too few numbers
            bool result10 = RegexHelper.Business.IsValidBillOfLaddingNumber("MMCAAA1234567");   // wrong, too many numbers
            bool result11 = RegexHelper.Business.IsValidBillOfLaddingNumber("MMCAA*123456");    // wrong, just letters & numbers are allowed
            bool result12 = RegexHelper.Business.IsValidBillOfLaddingNumber("MMCAA123456 ");    // wrong, spaces are not allowe
            bool result13 = RegexHelper.Business.IsValidBillOfLaddingNumber("");                // wrong, empty string
            bool result14 = RegexHelper.Business.IsValidBillOfLaddingNumber(null);              // wrong, null value

            // assert
            Assert.AreEqual(true, result01, "Assert 01");
            Assert.AreEqual(true, result02, "Assert 02");
            Assert.AreEqual(true, result03, "Assert 03");
            Assert.AreEqual(true, result04, "Assert 04");
            Assert.AreEqual(false, result05, "Assert 05");
            Assert.AreEqual(false, result06, "Assert 06");
            Assert.AreEqual(false, result07, "Assert 07");
            Assert.AreEqual(false, result08, "Assert 08");
            Assert.AreEqual(false, result09, "Assert 09");
            Assert.AreEqual(false, result10, "Assert 10");
            Assert.AreEqual(false, result11, "Assert 11");
            Assert.AreEqual(false, result12, "Assert 12");
            Assert.AreEqual(false, result13, "Assert 13");
            Assert.AreEqual(false, result14, "Assert 14");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Business_IsValidBookingNumber()
        {
            // arrange & act
            bool result01 = RegexHelper.Business.IsValidBookingNumber("AR1234567");             // good
            bool result02 = RegexHelper.Business.IsValidBookingNumber("IAR1234567");            // good
            bool result03 = RegexHelper.Business.IsValidBookingNumber("153AR1234567");          // good
            bool result04 = RegexHelper.Business.IsValidBookingNumber("153IAR1234567");         // good
            bool result05 = RegexHelper.Business.IsValidBookingNumber("AA1234567");             // wrong, must start with 'AR', 'IAR', '153AR', '153IAR','EBKG'
            bool result06 = RegexHelper.Business.IsValidBookingNumber("A12345678");             // wrong, too few letters
            bool result07 = RegexHelper.Business.IsValidBookingNumber("IA12345678");            // wrong, too few letters
            bool result08 = RegexHelper.Business.IsValidBookingNumber("153A12345678");          // wrong, too few letters
            bool result09 = RegexHelper.Business.IsValidBookingNumber("153IA12345678");         // wrong, too few letters
            bool result10 = RegexHelper.Business.IsValidBookingNumber("AR123456A");             // wrong, letter out of place
            bool result11 = RegexHelper.Business.IsValidBookingNumber("IAR123456A");            // wrong, letter out of place
            bool result12 = RegexHelper.Business.IsValidBookingNumber("153AR123456A");          // wrong, letter out of place
            bool result13 = RegexHelper.Business.IsValidBookingNumber("153IAR123456A");         // wrong, letter out of place
            bool result14 = RegexHelper.Business.IsValidBookingNumber("ARR123456");             // wrong, too many letters
            bool result15 = RegexHelper.Business.IsValidBookingNumber("AR123456");              // wrong, too few numbers
            bool result16 = RegexHelper.Business.IsValidBookingNumber("IAR123456");             // wrong, too few numbers
            bool result17 = RegexHelper.Business.IsValidBookingNumber("153AR123456");           // wrong, too few numbers
            bool result18 = RegexHelper.Business.IsValidBookingNumber("135IAR123456");          // wrong, too few numbers
            bool result19 = RegexHelper.Business.IsValidBookingNumber("AR12345678");            // wrong, too many numbers
            bool result20 = RegexHelper.Business.IsValidBookingNumber("IAR12345678");           // wrong, too many numbers
            bool result21 = RegexHelper.Business.IsValidBookingNumber("153AR12345678");         // wrong, too many numbers
            bool result22 = RegexHelper.Business.IsValidBookingNumber("153IAR12345678");        // wrong, too many numbers
            bool result23 = RegexHelper.Business.IsValidBookingNumber("*AR1234567");            // wrong, just letters & numbers are allowed
            bool result24 = RegexHelper.Business.IsValidBookingNumber("AR123456 ");             // wrong, spaces are not allowed
            bool result25 = RegexHelper.Business.IsValidBookingNumber("153 IAR1234567");        // wrong, spaces are not allowed
            bool result26 = RegexHelper.Business.IsValidBookingNumber("");                      // wrong, empty string
            bool result27 = RegexHelper.Business.IsValidBookingNumber(null);                    // wrong, null value
            bool result28 = RegexHelper.Business.IsValidBookingNumber("EBKG12345678");          // good
            bool result29 = RegexHelper.Business.IsValidBookingNumber("EBK1234567");            // wrong, too few letters
            bool result30 = RegexHelper.Business.IsValidBookingNumber("EBKG1234567");           // wrong, too few numbers
            bool result31 = RegexHelper.Business.IsValidBookingNumber("EBKG123456789");         // wrong, too many numbers
            bool result32 = RegexHelper.Business.IsValidBookingNumber("EBK1234G5678");          // wrong, letter out of place
            bool result33 = RegexHelper.Business.IsValidBookingNumber("153IMZ1234567");         // good
            bool result34 = RegexHelper.Business.IsValidBookingNumber("153IM12345678");         // wrong, too few letters
            bool result35 = RegexHelper.Business.IsValidBookingNumber("153IMZ123456A");         // wrong, letter out of place
            bool result36 = RegexHelper.Business.IsValidBookingNumber("135IMZ123456");          // wrong, too few numbers
            bool result37 = RegexHelper.Business.IsValidBookingNumber("153IMZ12345678");        // wrong, too many numbers
            bool result38 = RegexHelper.Business.IsValidBookingNumber("153 IMZ1234567");        // wrong, spaces are not allowed

            // assert
            Assert.AreEqual(true, result01, "Assert 01");
            Assert.AreEqual(true, result02, "Assert 02");
            Assert.AreEqual(true, result03, "Assert 03");
            Assert.AreEqual(true, result04, "Assert 04");
            Assert.AreEqual(false, result05, "Assert 05");
            Assert.AreEqual(false, result06, "Assert 06");
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
            Assert.AreEqual(false, result18, "Assert 18");
            Assert.AreEqual(false, result19, "Assert 19");
            Assert.AreEqual(false, result20, "Assert 20");
            Assert.AreEqual(false, result21, "Assert 21");
            Assert.AreEqual(false, result22, "Assert 22");
            Assert.AreEqual(false, result23, "Assert 23");
            Assert.AreEqual(false, result24, "Assert 24");
            Assert.AreEqual(false, result25, "Assert 25");
            Assert.AreEqual(false, result26, "Assert 26");
            Assert.AreEqual(false, result27, "Assert 27");
            Assert.AreEqual(true, result28, "Assert 28");
            Assert.AreEqual(false, result29, "Assert 29");
            Assert.AreEqual(false, result30, "Assert 30");
            Assert.AreEqual(false, result31, "Assert 31");
            Assert.AreEqual(false, result32, "Assert 32");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Business_IsValidContainerNumber()
        {
            // arrange & act
            bool result01 = RegexHelper.Business.IsValidContainerNumber("AAAA1234567");         // good
            bool result02 = RegexHelper.Business.IsValidContainerNumber("AAA1234567");          // wrong, too few letters
            bool result03 = RegexHelper.Business.IsValidContainerNumber("AAA12345678");         // wrong, too few letters
            bool result04 = RegexHelper.Business.IsValidContainerNumber("AAAAA123456");         // wrong, too many letters
            bool result05 = RegexHelper.Business.IsValidContainerNumber("AAAAA1234567");        // wrong, too many letters
            bool result06 = RegexHelper.Business.IsValidContainerNumber("AAAA123456");          // wrong, too few numbers
            bool result07 = RegexHelper.Business.IsValidContainerNumber("AAAA12345678");        // wrong, too many numbers
            bool result08 = RegexHelper.Business.IsValidContainerNumber("AAAA*123456");         // wrong, just letters & numbers are allowed
            bool result09 = RegexHelper.Business.IsValidContainerNumber("AAA 1234567");         // wrong, spaces are not allowed
            bool result10 = RegexHelper.Business.IsValidContainerNumber("AAAA123456 ");         // wrong, spaces are not allowed
            bool result11 = RegexHelper.Business.IsValidContainerNumber("");                    // wrong, empty string
            bool result12 = RegexHelper.Business.IsValidContainerNumber(null);                  // wrong, null value
            
            // assert
            Assert.AreEqual(true, result01, "Assert 01");
            Assert.AreEqual(false, result02, "Assert 02");
            Assert.AreEqual(false, result03, "Assert 03");
            Assert.AreEqual(false, result04, "Assert 04");
            Assert.AreEqual(false, result05, "Assert 05");
            Assert.AreEqual(false, result06, "Assert 06");
            Assert.AreEqual(false, result07, "Assert 07");
            Assert.AreEqual(false, result08, "Assert 08");
            Assert.AreEqual(false, result09, "Assert 09");
            Assert.AreEqual(false, result10, "Assert 10");
            Assert.AreEqual(false, result11, "Assert 11");
            Assert.AreEqual(false, result12, "Assert 12");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Business_IsValidShippingInstruction()
        {
            // arrange & act
            bool result01 = RegexHelper.Business.IsValidShippingInstruction("123456789");       // good
            bool result02 = RegexHelper.Business.IsValidShippingInstruction("12345678");        // wrong, too few numbers
            bool result03 = RegexHelper.Business.IsValidShippingInstruction("1234567890");      // wrong, too many numbers
            bool result04 = RegexHelper.Business.IsValidShippingInstruction("A12345678");       // wrong, just numbers are allowed
            bool result05 = RegexHelper.Business.IsValidShippingInstruction("123 456789");      // wrong, spaces are not allowed
            bool result06 = RegexHelper.Business.IsValidShippingInstruction("123456789 ");      // wrong, spaces are not allowed
            bool result07 = RegexHelper.Business.IsValidShippingInstruction("");                // wrong, empty string
            bool result08 = RegexHelper.Business.IsValidShippingInstruction(null);              // wrong, null value

            // assert
            Assert.AreEqual(true, result01, "Assert 01");
            Assert.AreEqual(false, result02, "Assert 02");
            Assert.AreEqual(false, result03, "Assert 03");
            Assert.AreEqual(false, result04, "Assert 04");
            Assert.AreEqual(false, result05, "Assert 05");
            Assert.AreEqual(false, result06, "Assert 06");
            Assert.AreEqual(false, result07, "Assert 07");
            Assert.AreEqual(false, result08, "Assert 08");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Business_IsValidFormNumber()
        {
            // arrange & act
            bool result01 = RegexHelper.Business.IsValidFormNumber("1234567");         // good
            bool result02 = RegexHelper.Business.IsValidFormNumber("12345678");        // good
            bool result03 = RegexHelper.Business.IsValidFormNumber("123456789");       // wrong, too many numbers
            bool result04 = RegexHelper.Business.IsValidFormNumber("A12345678");       // wrong, just numbers are allowed
            bool result05 = RegexHelper.Business.IsValidFormNumber("123 45678");       // wrong, spaces are not allowed
            bool result06 = RegexHelper.Business.IsValidFormNumber("12345678 ");       // wrong, spaces are not allowed
            bool result07 = RegexHelper.Business.IsValidFormNumber("");                // wrong, empty string
            bool result08 = RegexHelper.Business.IsValidFormNumber(null);              // wrong, null value

            // assert
            Assert.AreEqual(true, result01, "Assert 01");
            Assert.AreEqual(true, result02, "Assert 02");
            Assert.AreEqual(false, result03, "Assert 03");
            Assert.AreEqual(false, result04, "Assert 04");
            Assert.AreEqual(false, result05, "Assert 05");
            Assert.AreEqual(false, result06, "Assert 06");
            Assert.AreEqual(false, result07, "Assert 07");
            Assert.AreEqual(false, result08, "Assert 08");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Business_IdentifyDocument()
        {
            // arrange & act
            BusinessDocument businessDocument01 = RegexHelper.Business.IdentifyDocument("MMCAA1234567");    // good
            BusinessDocument businessDocument02 = RegexHelper.Business.IdentifyDocument("MMCAAA123456");    // good
            BusinessDocument businessDocument03 = RegexHelper.Business.IdentifyDocument("AR1234567");       // good
            BusinessDocument businessDocument04 = RegexHelper.Business.IdentifyDocument("IAR1234567");      // good
            BusinessDocument businessDocument05 = RegexHelper.Business.IdentifyDocument("123AR1234567");    // good
            BusinessDocument businessDocument06 = RegexHelper.Business.IdentifyDocument("123IAR1234567");   // good
            BusinessDocument businessDocument07 = RegexHelper.Business.IdentifyDocument("MMCU1234567");     // good
            BusinessDocument businessDocument08 = RegexHelper.Business.IdentifyDocument("CRXU1234567");     // good
            BusinessDocument businessDocument09 = RegexHelper.Business.IdentifyDocument("TRIU1234567");     // good
            BusinessDocument businessDocument10 = RegexHelper.Business.IdentifyDocument("187888335");       // good
            BusinessDocument businessDocument11 = RegexHelper.Business.IdentifyDocument("187903434");       // good
            BusinessDocument businessDocument12 = RegexHelper.Business.IdentifyDocument("1234567890");      // wrong, unknown
            BusinessDocument businessDocument13 = RegexHelper.Business.IdentifyDocument("");                // wrong, empty string
            BusinessDocument businessDocument14 = RegexHelper.Business.IdentifyDocument(null);              // wrong, null value

            // assert
            Assert.AreEqual(BusinessDocument.BillOfLadding, businessDocument01, "Assert 01");
            Assert.AreEqual(BusinessDocument.BillOfLadding, businessDocument02, "Assert 02");
            Assert.AreEqual(BusinessDocument.Booking, businessDocument03, "Assert 03");
            Assert.AreEqual(BusinessDocument.Booking, businessDocument04, "Assert 04");
            Assert.AreEqual(BusinessDocument.Booking, businessDocument05, "Assert 05");
            Assert.AreEqual(BusinessDocument.Booking, businessDocument06, "Assert 06");
            Assert.AreEqual(BusinessDocument.Container, businessDocument07, "Assert 07");
            Assert.AreEqual(BusinessDocument.Container, businessDocument08, "Assert 08");
            Assert.AreEqual(BusinessDocument.Container, businessDocument09, "Assert 09");
            Assert.AreEqual(BusinessDocument.ShippingInstruction, businessDocument10, "Assert 10");
            Assert.AreEqual(BusinessDocument.ShippingInstruction, businessDocument11, "Assert 11");
            Assert.AreEqual(BusinessDocument.None, businessDocument12, "Assert 12");
            Assert.AreEqual(BusinessDocument.None, businessDocument13, "Assert 13");
            Assert.AreEqual(BusinessDocument.None, businessDocument14, "Assert 14");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Business_Properties()
        {
            // assert
            Assert.IsFalse(RegexHelper.Business.BILL_OF_LADDING.IsNullOrEmpty(), "Assert 01");
            Assert.IsFalse(RegexHelper.Business.BOOKING.IsNullOrEmpty(), "Assert 02");
            Assert.IsFalse(RegexHelper.Business.CONTAINER.IsNullOrEmpty(), "Assert 03");
            Assert.IsFalse(RegexHelper.Business.SHIPPING_INSTRUCTION.IsNullOrEmpty(), "Assert 04");
            Assert.IsFalse(RegexHelper.Business.FORM_NUMBER.IsNullOrEmpty(), "Assert 05");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Miscellaneous_IsNumeric()
        {
            // arrange & act
            bool result01 = RegexHelper.Miscellaneous.IsNumeric("1234567890");      // good
            bool result02 = RegexHelper.Miscellaneous.IsNumeric(null);              // wrong, null
            bool result03 = RegexHelper.Miscellaneous.IsNumeric("");                // wrong, empty
            bool result04 = RegexHelper.Miscellaneous.IsNumeric("12 34567890");     // wrong, space char
            bool result05 = RegexHelper.Miscellaneous.IsNumeric("12-34567890");     // wrong, dash char
            bool result06 = RegexHelper.Miscellaneous.IsNumeric("1234567890A");     // wrong, 'A' char
            bool result07 = RegexHelper.Miscellaneous.IsNumeric("This is a test string."); // wrong

            // assert
            Assert.AreEqual(true, result01, "Assert 01");
            Assert.AreEqual(false, result02, "Assert 02");
            Assert.AreEqual(false, result03, "Assert 03");
            Assert.AreEqual(false, result04, "Assert 04");
            Assert.AreEqual(false, result05, "Assert 05");
            Assert.AreEqual(false, result06, "Assert 06");
            Assert.AreEqual(false, result07, "Assert 07");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Miscellaneous_IsValidCuitNumber()
        {
            // arrange & act
            bool result01 = RegexHelper.Miscellaneous.IsValidCuitNumber("30-52671272-9");   // good
            bool result02 = RegexHelper.Miscellaneous.IsValidCuitNumber("30-50287435-3");   // good
            bool result03 = RegexHelper.Miscellaneous.IsValidCuitNumber("11-11111111-1");   // wrong, invalid check digit
            bool result04 = RegexHelper.Miscellaneous.IsValidCuitNumber("30 52671272 9");   // wrong, invalid separator digit (dashes)
            bool result05 = RegexHelper.Miscellaneous.IsValidCuitNumber("30.52671272.9");   // wrong, invalid separator digit (dashes)
            bool result06 = RegexHelper.Miscellaneous.IsValidCuitNumber("30526712729");     // wrong, missing separator digit (dashes)
            bool result07 = RegexHelper.Miscellaneous.IsValidCuitNumber("3A-52671272-9");   // wrong, invalid charater
            bool result08 = RegexHelper.Miscellaneous.IsValidCuitNumber("30-5267127A-9");   // wrong, invalid charater
            bool result09 = RegexHelper.Miscellaneous.IsValidCuitNumber("30-52671272-A");   // wrong, invalid charater

            // assert
            Assert.AreEqual(true, result01, "Assert 01");
            Assert.AreEqual(true, result02, "Assert 02");
            Assert.AreEqual(false, result03, "Assert 03");
            Assert.AreEqual(false, result04, "Assert 04");
            Assert.AreEqual(false, result05, "Assert 05");
            Assert.AreEqual(false, result06, "Assert 06");
            Assert.AreEqual(false, result07, "Assert 07");
            Assert.AreEqual(false, result08, "Assert 08");
            Assert.AreEqual(false, result09, "Assert 09");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Miscellaneous_IsValidCuitNumber_NoDashes()
        {
            // arrange & act
            bool result01 = RegexHelper.Miscellaneous.IsValidCuitNumber("30526712729", false);     // good
            bool result02 = RegexHelper.Miscellaneous.IsValidCuitNumber("30502874353", false);     // good
            bool result03 = RegexHelper.Miscellaneous.IsValidCuitNumber("11111111111", false);     // wrong, invalid check digit
            bool result04 = RegexHelper.Miscellaneous.IsValidCuitNumber("30 52671272 9", false);   // wrong, invalid separator digit (dashes)
            bool result05 = RegexHelper.Miscellaneous.IsValidCuitNumber("30.52671272.9", false);   // wrong, invalid separator digit (dashes)
            bool result06 = RegexHelper.Miscellaneous.IsValidCuitNumber("30-52671272-9", false);   // wrong, included separator digit (dashes)
            bool result07 = RegexHelper.Miscellaneous.IsValidCuitNumber("3A526712729", false);     // wrong, invalid charater
            bool result08 = RegexHelper.Miscellaneous.IsValidCuitNumber("305267127A9", false);     // wrong, invalid charater
            bool result09 = RegexHelper.Miscellaneous.IsValidCuitNumber("3052671272A", false);     // wrong, invalid charater

            // assert
            Assert.AreEqual(true, result01, "Assert 01");
            Assert.AreEqual(true, result02, "Assert 02");
            Assert.AreEqual(false, result03, "Assert 03");
            Assert.AreEqual(false, result04, "Assert 04");
            Assert.AreEqual(false, result05, "Assert 05");
            Assert.AreEqual(false, result06, "Assert 06");
            Assert.AreEqual(false, result07, "Assert 07");
            Assert.AreEqual(false, result08, "Assert 08");
            Assert.AreEqual(false, result09, "Assert 09");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Miscellaneous_Properties()
        {
            // assert
            Assert.IsFalse(RegexHelper.Miscellaneous.CREDIT_CARD.IsNullOrEmpty(), "Assert 01");
            Assert.IsFalse(RegexHelper.Miscellaneous.CUIT_NUMBER.IsNullOrEmpty(), "Assert 02");
            Assert.IsFalse(RegexHelper.Miscellaneous.EMAIL.IsNullOrEmpty(), "Assert 03");
            Assert.IsFalse(RegexHelper.Miscellaneous.IP.IsNullOrEmpty(), "Assert 04");
            Assert.IsFalse(RegexHelper.Miscellaneous.NUMERIC.IsNullOrEmpty(), "Assert 05");
            Assert.IsFalse(RegexHelper.Miscellaneous.STRONG_PASSWORD.IsNullOrEmpty(), "Assert 06");
            Assert.IsFalse(RegexHelper.Miscellaneous.TELEPHONE.IsNullOrEmpty(), "Assert 07");
            Assert.IsFalse(RegexHelper.Miscellaneous.URL.IsNullOrEmpty(), "Assert 08");
            Assert.IsFalse(RegexHelper.Miscellaneous.USER_NAME.IsNullOrEmpty(), "Assert 09");
            Assert.IsFalse(RegexHelper.Miscellaneous.ZIP_CODE.IsNullOrEmpty(), "Assert 10");
        }
    }
}
