using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Debug;

namespace SupportLibraryTest
{
    /// <summary>
    /// Testing of Debug namespace classes.
    /// </summary>
    [TestClass]
    public class DebugTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Debug")]
        public void DebugHelper_GetVariableName_ValidData()
        {
            // arrange
            bool boolVariable = true;
            int intVariable = 1;
            float floatVariable = 10.1234F;
            double doubleVariable = 10.1234D;
            decimal decimalVariable = 10.1234M;
            var queryVariable = from str in (new List<string>() { "a", "b", "c" }) select str;
            DateTime objectDateTime = new DateTime();
            StringBuilder objectStringBuilder = null;
            
            // act
            string varName1 = DebugHelper.GetVariableName(() => boolVariable);
            string varName2 = DebugHelper.GetVariableName(() => intVariable);
            string varName3 = DebugHelper.GetVariableName(() => floatVariable);
            string varName4 = DebugHelper.GetVariableName(() => doubleVariable);
            string varName5 = DebugHelper.GetVariableName(() => decimalVariable);
            string varName6 = DebugHelper.GetVariableName(() => queryVariable);
            string varName7 = DebugHelper.GetVariableName(() => objectDateTime);
            string varName8 = DebugHelper.GetVariableName(() => objectStringBuilder);

            // assert
            Assert.AreEqual("boolVariable", varName1, "Assert 01");
            Assert.AreEqual("intVariable", varName2, "Assert 02");
            Assert.AreEqual("floatVariable", varName3, "Assert 03");
            Assert.AreEqual("doubleVariable", varName4, "Assert 04");
            Assert.AreEqual("decimalVariable", varName5, "Assert 05");
            Assert.AreEqual("queryVariable", varName6, "Assert 06");
            Assert.AreEqual("objectDateTime", varName7, "Assert 07");
            Assert.AreEqual("objectStringBuilder", varName8, "Assert 08");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Debug")]
        public void DebugHelper_GetVariableName_InvalidData_Literal()
        {
            try
            {
                // arrange & act
                string varName = DebugHelper.GetVariableName(() => 1234);

                // assert
                Assert.Fail("DebugHelper.GetVariableName() parameter was not properly validated.");
            }
            catch (ArgumentException ex) { Assert.AreEqual("expr", ex.ParamName); }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Debug")]
        public void DebugHelper_GetVariableName_InvalidData_NewObject()
        {
            try
            {
                // arrange & act
                string varName = DebugHelper.GetVariableName(() => new StringBuilder());

                // assert
                Assert.Fail("DebugHelper.GetVariableName() parameter was not properly validated.");
            }
            catch (ArgumentException ex) { Assert.AreEqual("expr", ex.ParamName); }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }
    }
}
