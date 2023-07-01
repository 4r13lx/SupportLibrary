using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Math;

namespace SupportLibraryTest
{
    /// <summary>
    /// Testing of Math namespace classes.
    /// </summary>
    [TestClass]
    public class MathTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Math")]
        public void MathHelper_Truncate_Valid()
        {
            // arrange & act
            float floatValue1 = MathHelper.Truncate(1234F, 0);
            float floatValue2 = MathHelper.Truncate(1234F, 2);
            float floatValue3 = MathHelper.Truncate(1234.5678F, 0);
            float floatValue4 = MathHelper.Truncate(1234.5678F, 2);
            float floatValue5 = MathHelper.Truncate(-1234.5678F, 2);

            double doubleValue1 = MathHelper.Truncate(1234D, 0);
            double doubleValue2 = MathHelper.Truncate(1234D, 2);
            double doubleValue3 = MathHelper.Truncate(1234.5678D, 0);
            double doubleValue4 = MathHelper.Truncate(1234.5678D, 2);
            double doubleValue5 = MathHelper.Truncate(-1234.5678D, 2);

            decimal decimalValue1 = MathHelper.Truncate(1234M, 0);
            decimal decimalValue2 = MathHelper.Truncate(1234M, 2);
            decimal decimalValue3 = MathHelper.Truncate(1234.5678M, 0);
            decimal decimalValue4 = MathHelper.Truncate(1234.5678M, 2);
            decimal decimalValue5 = MathHelper.Truncate(1234.5678M, 10);
            decimal decimalValue6 = MathHelper.Truncate(1234.789012345678M, 10);
            decimal decimalValue7 = MathHelper.Truncate(-1234.5678M, 2);

            // assert
            Assert.AreEqual(1234F, floatValue1, "Assert Float 01");
            Assert.AreEqual(1234F, floatValue2, "Assert Float 02");
            Assert.AreEqual(1234F, floatValue3, "Assert Float 03");
            Assert.AreEqual(1234.56F, floatValue4, "Assert Float 04");
            Assert.AreEqual(-1234.56F, floatValue5, "Assert Float 05");

            Assert.AreEqual(1234D, doubleValue1, "Assert Double 01");
            Assert.AreEqual(1234D, doubleValue2, "Assert Double 02");
            Assert.AreEqual(1234D, doubleValue3, "Assert Double 03");
            Assert.AreEqual(1234.56D, doubleValue4, "Assert Double 04");
            Assert.AreEqual(-1234.56D, doubleValue5, "Assert Double 05");

            Assert.AreEqual(1234M, decimalValue1, "Assert Decimal 01");
            Assert.AreEqual(1234M, decimalValue2, "Assert Decimal 02");
            Assert.AreEqual(1234M, decimalValue3, "Assert Decimal 03");
            Assert.AreEqual(1234.56M, decimalValue4, "Assert Decimal 04");
            Assert.AreEqual(1234.5678M, decimalValue5, "Assert Decimal 05");
            Assert.AreEqual(1234.7890123456M, decimalValue6, "Assert Decimal 06");
            Assert.AreEqual(-1234.56M, decimalValue7, "Assert Decimal 07");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Math")]
        public void MathHelper_Truncate_Invalid_DecimalsParam()
        {
            try
            {
                // arrange & act
                float floatValue = MathHelper.Truncate(1234.4567F, 3);
                double doubleValue = MathHelper.Truncate(1234.678901234567890D, 11);

                // assert
                Assert.Fail("MathHelper.Truncate() parameters were not properly validated.");
            }
            catch (ArgumentOutOfRangeException ex) { Assert.AreEqual("decimals", ex.ParamName); }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }
    }
}
