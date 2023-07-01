using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.IO;

namespace SupportLibraryTest.IO
{
    /// <summary>
    /// Testing of IO namespace classes.<para/>
    /// This tests uses the path 'C:\Users\UserName\AppData\Local\Temp\GUID' for testing purpose.
    /// </summary>
    [TestClass]
    public class FileHelperTests
    {
        private readonly string basePath = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User);

        [TestMethod, TestPropertyAttribute("Unit Tests", "IO")]
        public void FileHelper_BinaryFile_SaveRead()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "test.bin";

            MemoryStream memoryStream = new MemoryStream();
            new BinaryFormatter().Serialize(memoryStream, new DateTime(2016, 12, 31, 23, 59, 59));
            byte[] inputContent = memoryStream.ToArray(); // any secuence, dont matter what
            
            // act
            new FileHelper().SaveBinary(filePath, fileName, inputContent);
            byte[] fileContent = new FileHelper().ReadBinary(filePath, fileName);

            // assert
            CollectionAssert.AreEqual(inputContent, fileContent, "Assert 01");

            // cleanup
            File.Delete(Path.Combine(filePath, fileName));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "IO")]
        public void FileHelper_TextFile_Create_SaveRead()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "test.txt";
            string inputContent = "This is a test string." + Environment.NewLine + "This is another test string." + Environment.NewLine;

            // act
            new FileHelper().SaveText(filePath, fileName, inputContent);
            string fileContent = new FileHelper().ReadText(filePath, fileName);

            // assert
            Assert.AreEqual(inputContent, fileContent, "Assert 01");

            // cleanup
            File.Delete(Path.Combine(filePath, fileName));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "IO")]
        public void FileHelper_TextFile_Append_SaveRead()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "test.txt";
            string inputContent1 = "This is a test string." + Environment.NewLine;
            string inputContent2 = "This is another test string." + Environment.NewLine;

            // act
            new FileHelper().SaveText(filePath, fileName, TextFileMode.Append, inputContent1, false);
            new FileHelper().SaveText(filePath, fileName, TextFileMode.Append, inputContent2, false);
            string fileContent = new FileHelper().ReadText(filePath, fileName);

            // assert
            Assert.AreEqual(inputContent1 + inputContent2, fileContent, "Assert 01");

            // cleanup
            File.Delete(Path.Combine(filePath, fileName));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "IO")]
        public void FileHelper_DeleteOldFiles()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(filePath);

            string fileFullName1 = Path.Combine(filePath, "File1.txt");
            string fileFullName2 = Path.Combine(filePath, "File2.txt");
            string fileFullName3 = Path.Combine(filePath, "File3.txt");
            string fileFullName4 = Path.Combine(filePath, "File4.doc");

            File.Create(fileFullName1).Close();
            File.Create(fileFullName2).Close();
            File.Create(fileFullName3).Close();
            File.Create(fileFullName4).Close();
            
            File.SetCreationTime(fileFullName1, DateTime.Now);
            File.SetCreationTime(fileFullName2, DateTime.Now.AddDays(-10));
            File.SetCreationTime(fileFullName3, DateTime.Now.AddDays(-10));
            File.SetCreationTime(fileFullName4, DateTime.Now.AddDays(-10));

            // act
            new FileHelper().DeleteOldFiles(filePath, "*.txt", 7);
            string[] fileList1 = Directory.GetFiles(filePath);

            new FileHelper().DeleteOldFiles(filePath, "File4.*", 7);
            string[] fileList2 = Directory.GetFiles(filePath);

            new FileHelper().DeleteOldFiles(filePath, null, 0);
            string[] fileList3 = Directory.GetFiles(filePath);

            // assert
            Assert.IsTrue(fileList1.Length == 2, "Assert 01");
            CollectionAssert.Contains(fileList1, fileFullName1, "Assert 02");
            CollectionAssert.Contains(fileList1, fileFullName4, "Assert 03");

            Assert.IsTrue(fileList2.Length == 1, "Assert 04");
            CollectionAssert.Contains(fileList2, fileFullName1, "Assert 05");
            
            Assert.IsTrue(fileList3.Length == 0, "Assert 06");

            // cleanup
            Directory.Delete(filePath);
        }
    }
}
