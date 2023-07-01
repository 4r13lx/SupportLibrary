using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.IO;
using SupportLibrary.Logging;
using SupportLibrary.Testing;

namespace SupportLibraryTest.Logging
{
    /// <summary>
    /// Testing of Logging namespace classes.
    /// This tests uses the path 'C:\Users\UserName\AppData\Local\Temp\GUID' for testing purpose.
    /// </summary>
    [TestClass]
    public class TextLogHelperTests
    {
        private readonly string basePath = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User);
        private const string MSG_INFO = "Info message.", MSG_WARNING = "Warning message.", MSG_ERROR = "Error message.";

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void TextLogHelper_Contructor()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName1 = "SupportLibrary1.log", fileName2 = "SupportLibrary2.log", fileName3 = "SupportLibrary3.log";

            // act
            TextLogHelper logger1 = new TextLogHelper(filePath, fileName1, EntryTemplate.Minimal);
            TextLogHelper logger2 = new TextLogHelper(filePath, fileName2, true, EntryTemplate.Standard);
            TextLogHelper logger3 = new TextLogHelper(filePath, fileName3, false, EntryTemplate.Custom);

            // assert
            Assert.AreEqual(filePath, logger1.LogFilePath, "Assert 01");
            Assert.AreEqual(fileName1, logger1.LogFileName, "Assert 02");
            Assert.AreEqual(false, logger1.RevertImpersonation, "Assert 03");
            Assert.AreEqual(EntryTemplate.Minimal, logger1.EntryTemplate, "Assert 04");

            Assert.AreEqual(filePath, logger2.LogFilePath, "Assert 05");
            Assert.AreEqual(fileName2, logger2.LogFileName, "Assert 06");
            Assert.AreEqual(true, logger2.RevertImpersonation, "Assert 07");
            Assert.AreEqual(EntryTemplate.Standard, logger2.EntryTemplate, "Assert 07");

            Assert.AreEqual(filePath, logger3.LogFilePath, "Assert 09");
            Assert.AreEqual(fileName3, logger3.LogFileName, "Assert 10");
            Assert.AreEqual(false, logger3.RevertImpersonation, "Assert 11");
            Assert.AreEqual(EntryTemplate.Custom, logger3.EntryTemplate, "Assert 12");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void TextLogHelper_Log_Message_Minimal()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "SupportLibrary.log";

            string infoMessage = "Infomation entry with minimal profile enabled.";
            string warningMessage = "Warning entry with minimal profile enabled.";
            string errorMessage = "Error entry with minimal profile enabled.";

            // act
            TextLogHelper loggerMinimal = new TextLogHelper(filePath, fileName, EntryTemplate.Minimal);
            loggerMinimal.Log(EntryType.Info, infoMessage);
            loggerMinimal.Log(EntryType.Warning, warningMessage);
            loggerMinimal.Log(EntryType.Error, errorMessage);

            // assert
            string fileContent = new FileHelper().ReadText(filePath, fileName);
            Assert.IsTrue(fileContent.Contains(infoMessage), "Assert 01");
            Assert.IsTrue(fileContent.Contains(warningMessage), "Assert 02");
            Assert.IsTrue(fileContent.Contains(errorMessage), "Assert 03");
            
            // cleanup
            File.Delete(Path.Combine(filePath, fileName));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void TextLogHelper_Log_Message_Standard()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "SupportLibrary.log";

            string infoMessage = "Infomation entry with standard profile enabled.";
            string warningMessage = "Warning entry with standard profile enabled.";
            string errorMessage = "Error entry with standard profile enabled.";

            // act
            TextLogHelper loggerStandard = new TextLogHelper(filePath, fileName, EntryTemplate.Standard);
            loggerStandard.Log(EntryType.Info, infoMessage);
            loggerStandard.Log(EntryType.Warning, warningMessage);
            loggerStandard.Log(EntryType.Error, errorMessage);

            // assert
            string fileContent = new FileHelper().ReadText(filePath, fileName);
            Assert.IsTrue(fileContent.Contains(infoMessage), "Assert 01");
            Assert.IsTrue(fileContent.Contains(warningMessage), "Assert 02");
            Assert.IsTrue(fileContent.Contains(errorMessage), "Assert 03");

            // cleanup
            File.Delete(Path.Combine(filePath, fileName));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void TextLogHelper_Log_Message_Custom()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "SupportLibrary.log";

            string infoMessage = "Infomation entry with custom profile enabled.";
            string warningMessage = "Warning entry with custom profile enabled.";
            string errorMessage = "Error entry with custom profile enabled.";

            // act
            TextLogHelper loggerCustom = new TextLogHelper(filePath, fileName, EntryTemplate.Custom);
            loggerCustom.SetTitle("{datetime:yyyy-MM-dd hh:mm:ss} - {level,-12}  ({id:00})");
            loggerCustom.SetHeader('-', 40).SetFooter('-', 40);
            loggerCustom.Log(EntryType.Info, infoMessage);
            loggerCustom.Log(EntryType.Warning, warningMessage);
            loggerCustom.Log(EntryType.Error, errorMessage);

            // assert
            string fileContent = new FileHelper().ReadText(filePath, fileName);
            Assert.IsTrue(fileContent.Contains(infoMessage), "Assert 01");
            Assert.IsTrue(fileContent.Contains(warningMessage), "Assert 02");
            Assert.IsTrue(fileContent.Contains(errorMessage), "Assert 03");

            // cleanup
            File.Delete(Path.Combine(filePath, fileName));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void TextLogHelper_Log_Exception()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "SupportLibrary.log";

            // act
            TextLogHelper logger = new TextLogHelper(filePath, fileName, EntryTemplate.Standard);
            logger.Log(new ArgumentException("Param is null.", "param"));
            logger.Log(new DivideByZeroException());

            // assert
            string fileContent = new FileHelper().ReadText(filePath, fileName);
            Assert.IsTrue(fileContent.Contains("ArgumentException"), "Assert 01");
            Assert.IsTrue(fileContent.Contains("DivideByZeroException"), "Assert 02");

            // cleanup
            File.Delete(Path.Combine(filePath, fileName));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Logging")]
        public void TextLogHelper_Log_FailsOnEmptyParams()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "SupportLibrary.log";

            Action action1 = () => new TextLogHelper("", fileName).Log(EntryType.Info, ""); // invalid logFilePath
            Action action2 = () => new TextLogHelper(filePath, "").Log(EntryType.Info, ""); // invalid logFileName

            // act & assert
            ArgumentNullException exception1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            Assert.AreEqual("logFilePath", exception1.ParamName, "Assert 01");

            ArgumentNullException exception2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
            Assert.AreEqual("logFileName", exception2.ParamName, "Assert 02");
        }
    }
}
