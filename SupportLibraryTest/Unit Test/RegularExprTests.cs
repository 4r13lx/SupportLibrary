using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.RegularExpressions;

namespace SupportLibraryTest
{
    /// <summary>
    /// Testing of RegularExpressions namespace classes.
    /// </summary>
    [TestClass]
    public class RegularExprTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Business_IsValidBillOfLaddingNumber()
        {
            // arrange & act
            bool result01 = RegexHelper.Business.IsValidBillOfLaddingNumber("MSCAA1234567");    // good
            bool result02 = RegexHelper.Business.IsValidBillOfLaddingNumber("MSCAAA123456");    // good
            bool result03 = RegexHelper.Business.IsValidBillOfLaddingNumber("AAABBB123456");    // wrong, must start with 'MSC'
            bool result04 = RegexHelper.Business.IsValidBillOfLaddingNumber("MSC123456789");    // wrong, too few letters
            bool result05 = RegexHelper.Business.IsValidBillOfLaddingNumber("MSCA12345678");    // wrong, too few letters
            bool result06 = RegexHelper.Business.IsValidBillOfLaddingNumber("MSCAAAA12345");    // wrong, too many letters
            bool result07 = RegexHelper.Business.IsValidBillOfLaddingNumber("MSCAAA12345");     // wrong, too few numbers
            bool result08 = RegexHelper.Business.IsValidBillOfLaddingNumber("MSCAAA1234567");   // wrong, too many numbers
            bool result09 = RegexHelper.Business.IsValidBillOfLaddingNumber("MSCAA*123456");    // wrong, just letters & numbers are allowed
            bool result10 = RegexHelper.Business.IsValidBillOfLaddingNumber("MSCAA123456 ");    // wrong, spaces are not allowed
            bool result11 = RegexHelper.Business.IsValidBillOfLaddingNumber("");                // wrong, empty string
            bool result12 = RegexHelper.Business.IsValidBillOfLaddingNumber(null);              // wrong, null value

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
            Assert.AreEqual(false, result10, "Assert 10");
            Assert.AreEqual(false, result11, "Assert 11");
            Assert.AreEqual(false, result12, "Assert 12");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Business_IsValidBookingNumber()
        {
            // arrange & act
            bool result01 = RegexHelper.Business.IsValidBookingNumber("AR1234567");             // good
            bool result02 = RegexHelper.Business.IsValidBookingNumber("IAR1234567");            // good
            bool result03 = RegexHelper.Business.IsValidBookingNumber("153AR1234567");          // good
            bool result04 = RegexHelper.Business.IsValidBookingNumber("153IAR1234567");         // good
            bool result05 = RegexHelper.Business.IsValidBookingNumber("AA1234567");             // wrong, must start with 'AR', 'IAR', '153AR', '153IAR'
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
        public void RegexHelper_Business_IdentifyDocument()
        {
            // arrange & act
            BusinessDocument businessDocument01 = RegexHelper.Business.IdentifyDocument("MSCAA1234567");    // good
            BusinessDocument businessDocument02 = RegexHelper.Business.IdentifyDocument("MSCAAA123456");    // good
            BusinessDocument businessDocument03 = RegexHelper.Business.IdentifyDocument("AR1234567");       // good
            BusinessDocument businessDocument04 = RegexHelper.Business.IdentifyDocument("IAR1234567");      // good
            BusinessDocument businessDocument05 = RegexHelper.Business.IdentifyDocument("153AR1234567");    // good
            BusinessDocument businessDocument06 = RegexHelper.Business.IdentifyDocument("153IAR1234567");   // good
            BusinessDocument businessDocument07 = RegexHelper.Business.IdentifyDocument("MSCU1234567");     // good
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
            Assert.AreEqual(BusinessDocument.Unknown, businessDocument12, "Assert 12");
            Assert.AreEqual(BusinessDocument.Unknown, businessDocument13, "Assert 13");
            Assert.AreEqual(BusinessDocument.Unknown, businessDocument14, "Assert 14");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Business_Properties()
        {
            // assert
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Business.BILL_OF_LADDING), "Assert 01");
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Business.BOOKING), "Assert 02");
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Business.CONTAINER), "Assert 03");
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Business.SHIPPING_INSTRUCTION), "Assert 04");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "RegularExpr")]
        public void RegexHelper_Miscellaneous_Properties()
        {
            // assert
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Miscellaneous.CREDIT_CARD), "Assert 01");
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Miscellaneous.EMAIL), "Assert 02");
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Miscellaneous.IP), "Assert 03");
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Miscellaneous.STRONG_PASSWORD), "Assert 04");
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Miscellaneous.TELEPHONE), "Assert 05");
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Miscellaneous.URL), "Assert 06");
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Miscellaneous.USER_NAME), "Assert 07");
            Assert.IsFalse(String.IsNullOrEmpty(RegexHelper.Miscellaneous.ZIP_CODE), "Assert 08");
        }
    }
}
