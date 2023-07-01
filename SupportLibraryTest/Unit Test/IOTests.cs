using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.IO;

namespace SupportLibraryTest
{
    /// <summary>
    /// Testing of Math namespace classes.<para/>
    /// This tests uses the path 'C:\Users\UserName\AppData\Local\Temp\GUID' for testing purpose.
    /// </summary>
    [TestClass]
    public class IOTests
    {
        private readonly string basePath = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User);

        [TestMethod, TestPropertyAttribute("Unit Tests", "IO")]
        public void FileHelper_SaveReadBinary_Valid()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());

            MemoryStream memoryStream = new MemoryStream();
            new BinaryFormatter().Serialize(memoryStream, new DateTime(2016, 12, 31, 23, 59, 59));
            byte[] inputContent = memoryStream.ToArray(); // any secuence, dont matter what
            
            // act
            new FileHelper().SaveBinary(filePath, "test.bin", inputContent);
            byte[] fileContent = new FileHelper().ReadBinary(filePath, "test.bin");

            // assert
            CollectionAssert.AreEqual(inputContent, fileContent, "Assert 01");

            // clean
            File.Delete(Path.Combine(filePath, "test.bin"));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "IO")]
        public void FileHelper_SaveReadText_Valid()
        {
            // arrange
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string inputContent = "This is a test string." + Environment.NewLine + "This is another test string.";

            // act
            new FileHelper().SaveText(filePath, "test.txt", inputContent);
            string fileContent = new FileHelper().ReadText(filePath, "test.txt");

            // assert
            Assert.AreEqual(inputContent, fileContent, "Assert 01");

            // clean
            File.Delete(Path.Combine(filePath, "test.txt"));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "IO")]
        public void FileHelper_DeleteOldFiles_Valid()
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

            // clean
            Directory.Delete(filePath);
        }
    }
}
