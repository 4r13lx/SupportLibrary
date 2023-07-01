using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

using SupportLibrary.Web;
using SupportLibrary.Testing;

namespace SupportLibraryTest.Web
{
    /// <summary>
    /// Testing of Web namespace classes.
    /// </summary>
    [TestClass]
    public class FileDownloaderTests
    {
        string fileName = "file.txt";
        string fileContentText = "test";
        byte[] fileContentBuffer = new byte[1];

        [TestMethod, TestPropertyAttribute("Unit Tests", "Web")]
        public void FileDownloader_DownloadBinary()
        {
            // arrange
            HttpResponseBase response = Substitute.For<HttpResponseBase>();

            // act
            FileDownloader fileDownloader = new FileDownloader(response);
            fileDownloader.Download(FileType.BINARY, fileName, fileContentBuffer);

            // assert
            Assert.AreEqual(@"application/octet-stream", fileDownloader.Response.ContentType, "Assert 01");
            response.Received().AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", fileName));
            response.Received().BinaryWrite(fileContentBuffer);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Web")]
        public void FileDownloader_DownloadText()
        {
            // arrange
            HttpResponseBase response = Substitute.For<HttpResponseBase>();

            // act
            FileDownloader fileDownloader = new FileDownloader(response);
            fileDownloader.Download(FileType.TEXT, fileName, fileContentText);

            // assert
            Assert.AreEqual(@"text/plain", fileDownloader.Response.ContentType, "Assert 01");
            response.Received().AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", fileName));
            response.Received().Write(fileContentText);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Web")]
        public void FileDownloader_Download_FailsOnInvalidArgs()
        {
            // arrange
            HttpResponseBase response = Substitute.For<HttpResponseBase>();

            Action action1 = () => new FileDownloader(response).Download(FileType.None, fileName, "");              // 1st param empty
            Action action2 = () => new FileDownloader(response).Download(FileType.TEXT, null, "");                  // 2nd param null
            Action action3 = () => new FileDownloader(response).Download(FileType.TEXT, "", "");                    // 2nd param empty
            Action action4 = () => new FileDownloader(response).Download(FileType.TEXT, fileName, (string)null);    // 3rd param null

            // act & assert
            ArgumentException ex1 = AssertEx.Exceptions.Throws<ArgumentException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
            ArgumentNullException ex3 = AssertEx.Exceptions.Throws<ArgumentNullException>(action3);
            ArgumentNullException ex4 = AssertEx.Exceptions.Throws<ArgumentNullException>(action4);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Web")]
        public void Extensions_Download_FailsOnInvalidArgs()
        {
            // arrange
            HttpRequest request = new HttpRequest("", "http://tempuri.org", "");
            HttpResponse response = new HttpResponse(null);
            HttpContext context = new HttpContext(request, response);

            Action action1 = () => context.DownloadFile(FileType.None, fileName, "");               // 1st param empty
            Action action2 = () => context.DownloadFile(FileType.TEXT, null, "");                   // 2nd param null
            Action action3 = () => context.DownloadFile(FileType.TEXT, "", "");                     // 2nd param empty
            Action action4 = () => context.DownloadFile(FileType.TEXT, fileName, (string)null);     // 3rd param null

            // act & assert
            ArgumentException ex1 = AssertEx.Exceptions.Throws<ArgumentException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
            ArgumentNullException ex3 = AssertEx.Exceptions.Throws<ArgumentNullException>(action3);
            ArgumentNullException ex4 = AssertEx.Exceptions.Throws<ArgumentNullException>(action4);
        }
    }
}
