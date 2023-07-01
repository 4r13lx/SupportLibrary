using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Core;
using SupportLibrary.IO;
using SupportLibrary.Logging;
using SupportLibrary.Testing;

namespace SupportLibraryTest.Core
{
    /// <summary>
    /// Testing of Core namespace classes.
    /// </summary>
    [TestClass]
    public class DebugTests
    {
        private readonly string basePath = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User);
        private readonly string fileName = "SupportLibrary.log";

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Debug")]
        public void Debug_GetVariableName()
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
            string varName1 = Debug.GetVarName(() => boolVariable);
            string varName2 = Debug.GetVarName(() => intVariable);
            string varName3 = Debug.GetVarName(() => floatVariable);
            string varName4 = Debug.GetVarName(() => doubleVariable);
            string varName5 = Debug.GetVarName(() => decimalVariable);
            string varName6 = Debug.GetVarName(() => queryVariable);
            string varName7 = Debug.GetVarName(() => objectDateTime);
            string varName8 = Debug.GetVarName(() => objectStringBuilder);

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

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Debug")]
        public void Debug_GetVariableName_FailsOnLiteral()
        {
            try
            {
                // arrange & act
                string varName = Debug.GetVarName(() => 1234);

                // assert
                Assert.Fail("DebugHelper.GetVariableName() parameter was not properly validated.");
            }
            catch (ArgumentException ex) { Assert.AreEqual("expr", ex.ParamName); }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Debug")]
        public void Debug_GetVariableName_FailsOnNewObject()
        {
            try
            {
                // arrange & act
                string varName = Debug.GetVarName(() => new StringBuilder());

                // assert
                Assert.Fail("DebugHelper.GetVariableName() parameter was not properly validated.");
            }
            catch (ArgumentException ex) { Assert.AreEqual("expr", ex.ParamName); }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Debug")]
        public void Debug_LoggedExecution()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            TextLogHelper logger = new TextLogHelper(filePath, fileName, EntryTemplate.Minimal);

            // act
            //Debug.LoggedExecution(() => this.TestMethod1(), logger);
            //String result1 = Debug.LoggedExecution(() => this.TestMethod2("Test"), logger);
            String result2 = Debug.LoggedExecution(() => this.TestMethod3("Test", 123), logger);
            DateTime result3 =  Debug.LoggedExecution(() => this.TestMethod4(), logger);

            // assert
            string fileContent = new FileHelper().ReadText(filePath, fileName);
            Assert.IsTrue(fileContent.Contains("dawewfa"), "Assert 01");
            Assert.IsTrue(fileContent.Contains("fwfqwccqc"), "Assert 02");
            Assert.IsTrue(fileContent.Contains("weewceaw"), "Assert 03");

            // cleanup
            File.Delete(Path.Combine(filePath, fileName));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Debug")]
        public void Debug_LoggedExecution_FailsOnNullArgs()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            TextLogHelper logger = new TextLogHelper(filePath, fileName, EntryTemplate.Minimal);

            Action action1 = () => Debug.LoggedExecution(null, logger);                             // action is null
            Action action2 = () => Debug.LoggedExecution<string>(null, logger);                     // function is null
            Action action3 = () => Debug.LoggedExecution(() => TestMethod1(), null);                // logger is null
            Action action4 = () => Debug.LoggedExecution<string>(() => TestMethod2("Test"), null);  // logger is null

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
            ArgumentNullException ex3 = AssertEx.Exceptions.Throws<ArgumentNullException>(action3);
            ArgumentNullException ex4 = AssertEx.Exceptions.Throws<ArgumentNullException>(action4);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Debug")]
        public void Debug_TimedExecution()
        {
            // arrange
            Action action = () => TestMethod1();
            Func<string> func1 = () => TestMethod2("Test");
            Func<string> func2 = () => TestMethod3("Test", 123);
            Func<DateTime> func3 = () => TestMethod4();
            Func<DateTime?> func4 = () => null;

            // act
            TimeSpan timeSpan1 = Debug.TimedExecution(action);
            TimeSpan timeSpan2 = Debug.TimedExecution(func1);
            TimeSpan timeSpan3 = Debug.TimedExecution(func2);
            TimeSpan timeSpan4 = Debug.TimedExecution(func3);
            TimeSpan timeSpan5 = Debug.TimedExecution(func4);

            // assert
            AssertEx.Condition.Greater(timeSpan1.Ticks, 0, "Assert 01");
            AssertEx.Condition.Greater(timeSpan2.Ticks, 0, "Assert 02");
            AssertEx.Condition.Greater(timeSpan3.Ticks, 0, "Assert 03");
            AssertEx.Condition.Greater(timeSpan4.Ticks, 0, "Assert 04");
            AssertEx.Condition.Greater(timeSpan4.Ticks, 0, "Assert 05");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Debug")]
        public void Debug_TimedExecution_FailsOnNullArgs()
        {
            // arrange
            Action action1 = () => Debug.TimedExecution(null);
            Action action2 = () => Debug.TimedExecution<string>(null);

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
        }

        #region Auxiliar Methods

        private void TestMethod1() { }

        private string TestMethod2(string p1) { return p1.ToUpper(); }

        private string TestMethod3(string p1, int p2) { return p1.ToUpper() + p2.ToString(); }

        private DateTime TestMethod4() { return DateTime.Now; }

        #endregion
    }
}
