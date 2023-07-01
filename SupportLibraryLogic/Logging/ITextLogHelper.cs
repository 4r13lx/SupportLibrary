using System;
namespace SupportLibrary.Logging
{
    /// <summary>
    /// Interface for File Log related tasks.<para/>
    /// For example: Log messages and exceptions.
    /// </summary>
    public interface ITextLogHelper
    {
        /// <summary>
        /// Gets current LogPath setting.
        /// </summary>
        string LogFileName { get; }

        /// <summary>
        /// Gets current LogName setting.
        /// </summary>
        string LogFilePath { get; }

        /// <summary>
        /// Gets current RevertImpersonation setting.
        /// </summary>
        bool RevertImpersonation { get; }

        /// <summary>
        /// Logs a exception into the log file.
        /// </summary>
        /// <param name="exception">Exception to log.</param>       
        void Log(Exception exception);

        /// <summary>
        /// Logs a message into the log file.
        /// </summary>
        /// <param name="entryType">EntryType for the log entry. For example: 'TextLogEntryType.Information'.</param>
        /// <param name="message">Message to log.</param>
        void Log(EntryType entryType, string message);
    }
}
