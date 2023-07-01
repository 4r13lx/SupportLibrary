using System;
using System.Web;

namespace SupportLibrary.Web
{
    /// <summary>
    /// Extension Methods for Web related tasks.<para/>
    /// For example: download an excel file.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Download a binary file.
        /// </summary>
        /// <param name="context">The current HttpContext.</param>
        /// <param name="fileType">Type for the downloaded file.</param>
        /// <param name="fileName">Name for the downloaded file.</param>
        /// <param name="fileContent">The buffer containing data to write on the downloaded file.</param>
        public static void DownloadFile(this HttpContext context, FileType fileType, string fileName, byte[] fileContent)
        {
            try
            {
                new FileDownloader(context).Download(fileType, fileName, fileContent);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Download a binary file.
        /// </summary>
        /// <param name="context">The current HttpContext.</param>
        /// <param name="fileType">Type for the downloaded file.</param>
        /// <param name="fileName">Name for the downloaded file.</param>
        /// <param name="fileContent">The text containing data to write on the downloaded file.</param>
        public static void DownloadFile(this HttpContext context, FileType fileType, string fileName, string fileContent)
        {
            try
            {
                new FileDownloader(context).Download(fileType, fileName, fileContent);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Gets a session value by name.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="context">The current HttpContext.</param>
        /// <param name="name">The key name of the session value.</param>
        /// <returns>Session value converted to T-Type.</returns>
        public static T RetrieveFromSession<T>(this HttpContext context, string name)
        {
            try
            {
                return new SessionHelper(context).Retrieve<T>(name);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Gets a session value by name, plus a error message in case that session key do not exists.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="context">The current HttpContext.</param>
        /// <param name="name">The key name of the session value.</param>
        /// <param name="errorMessage">Error message in case that the session key do not exists.</param>
        /// <returns>Session value converted to T-Type.</returns>
        public static T RetrieveFromSessionWithMessage<T>(this HttpContext context, string name, string errorMessage)
        {
            try
            {
                return new SessionHelper(context).RetrieveWithMessage<T>(name, errorMessage);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Gets a session value by name, plus a resource key in case that session key do not exists.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="context">The current HttpContext.</param>
        /// <param name="name">The key name of the session value.</param>
        /// <param name="errorResourceKey">Local resource key with the error message in case that the session key do not exists.</param>
        /// <returns>Session value converted to T-Type.</returns>
        public static T RetrieveFromSessionWithRecourceKey<T>(this HttpContext context, string name, string errorResourceKey)
        {
            try
            {
                return new SessionHelper(context).RetrieveWithRecourceKey<T>(name, errorResourceKey);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Try to retrieve a session value by name.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="context">The current HttpContext.</param>
        /// <param name="name">The key name of the session value.</param>
        /// <param name="result">The found value, otherwise null.</param>
        /// <returns>True if the given value was found.</returns>
        public static bool TryRetrieveFromSession<T>(this HttpContext context, string name, ref T result)
        {
            try
            {
                return new SessionHelper(context).TryRetrieve<T>(name, ref result);
            }
            catch (Exception) { throw; }
        }
    }
}
