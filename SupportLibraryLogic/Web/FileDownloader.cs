using System;
using System.Web;

using SupportLibrary.Text;

namespace SupportLibrary.Web
{
    /// <summary>
    /// Helper class for File Downloading related tasks.<para/>
    /// For example: download an excel file.
    /// </summary>
    public sealed class FileDownloader
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HttpResponseBase Response { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">HttpContext in wich to write the file.</param>
        public FileDownloader(HttpContext context)
        {
            if (context == null) { throw new ArgumentNullException(nameof(context), $"{ nameof(context) } is null."); }
            this.Response = new HttpResponseWrapper(context.Response);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">HttpContextBase in wich to write the file. Intended for testing purposes.</param>
        public FileDownloader(HttpContextBase context)
        {
            this.Response = context.Response;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response">HttpResponse in wich to write the file.</param>
        public FileDownloader(HttpResponse response)
        {
            if (response == null) { throw new ArgumentNullException(nameof(response), $"{ nameof(response) } is null."); }
            this.Response = new HttpResponseWrapper(response);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response">HttpResponseBase in wich to write the file. Intended for testing purposes.</param>
        public FileDownloader(HttpResponseBase response)
        {
            this.Response = response;
        }

        /// <summary>
        /// Download a binary file.
        /// </summary>
        /// <param name="fileType">Type for the downloaded file.</param>
        /// <param name="fileName">Name for the downloaded file.</param>
        /// <param name="fileContent">The buffer containing data to write on the downloaded file.</param>
        public void Download(FileType fileType, string fileName, byte[] fileContent)
        {
            try
            {
                if (fileType == FileType.None)  { throw new ArgumentException(nameof(fileType), $"{ nameof(fileType) } is unknown."); }
                if (fileName.IsNullOrEmpty())   { throw new ArgumentNullException(nameof(fileName), $"{ nameof(fileName) } is null."); }
                if (fileContent == null)        { throw new ArgumentNullException(nameof(fileContent), $"{ nameof(fileContent) } is null."); }

                this.Response.Clear();
                this.Response.ClearHeaders();
                this.Response.ContentType = this.GetContentType(fileType);
                this.Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", fileName));
                this.Response.BinaryWrite(fileContent);
                this.Response.End();
                this.Response.Flush();
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Download a text file.
        /// </summary>
        /// <param name="fileType">Type for the downloaded file.</param>
        /// <param name="fileName">Name for the downloaded file.</param>
        /// <param name="fileContent">The text containing data to write on the downloaded file.</param>
        public void Download(FileType fileType, string fileName, string fileContent)
        {
            try
            {
                if (fileType == FileType.None)  { throw new ArgumentException(nameof(fileType), $"{ nameof(fileType) } is unknown."); }
                if (fileName.IsNullOrEmpty())   { throw new ArgumentNullException(nameof(fileName), $"{ nameof(fileName) } is null."); }
                if (fileContent == null)        { throw new ArgumentNullException(nameof(fileContent), $"{ nameof(fileContent) } is null."); }

                this.Response.Clear();
                this.Response.ClearHeaders();
                this.Response.ContentType = this.GetContentType(fileType);
                this.Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", fileName));
                this.Response.Write(fileContent);
                this.Response.End();
                this.Response.Flush();
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Gets the MIME-Type for most common file types.
        /// </summary>
        /// <param name="fileType">File type for wich retrieve its MIME-Type.</param>
        /// <returns>MIME-Type for the given file type.</returns>
        public string GetContentType(FileType fileType)
        {
            try
            {
                switch (fileType)
                {
                    case FileType.TEXT:     return @"text/plain";
                    case FileType.CSV:      return @"text/csv";
                    case FileType.BINARY:   return @"application/octet-stream";
                    case FileType.PDF:      return @"application/pdf";
                    case FileType.DOC:      return @"application/msword";
                    case FileType.DOCX:     return @"application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    case FileType.XLS:      return @"application/vnd.ms-excel";
                    case FileType.XLSX:     return @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    case FileType.JPG:      return @"image/jpg";
                    case FileType.PNG:      return @"image/png";
                    default:                return @"";
                }
            }
            catch (Exception) { throw; }
        }
    }
}
