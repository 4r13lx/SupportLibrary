using System;

namespace SupportLibrary.Logging
{
    /// <summary>
    /// Interface for Windows Event Log related tasks.<para/>
    /// For example: Log messages and exceptions.
    /// </summary>
    public interface IEventLogHelper
    {
        /// <summary>
        /// Gets current EventLog.LogName setting.
        /// </summary>
        string LogName { get; }

        /// <summary>
        /// Gets current EventLog.LogSource setting.
        /// </summary>
        string LogSource { get; }

        /// <summary>
        /// Logs a exception into the Windows Event Log.
        /// </summary>
        /// <param name="exception">Exception to log.</param>
        void Log(Exception exception);

        /// <summary>
        /// Logs a message into the Windows Event Log.
        /// </summary>
        /// <param name="entryType">EntryType for the log entry. For example: 'EntryType.Info'.</param>
        /// <param name="message">Message to log.</param>
        void Log(EntryType entryType, string message);

        /// <summary>
        /// Logs a exception to the Windows Event Log.
        /// </summary>
        /// <param name="logName">Name for the log entry. For example: 'SupportLibrary'.</param>
        /// <param name="logSource">Source for the log entry. For example: 'SupportLibraryLogSource'.</param>
        /// <param name="exception">Exception to log.</param>
        void Log(string logName, string logSource, Exception exception);

        /// <summary>
        /// Logs a message to the Windows Event Log.
        /// </summary>
        /// <param name="logName">Name for the log entry. For example: 'SupportLibrary'.</param>
        /// <param name="logSource">Source for the log entry. For example: 'SupportLibraryLogSource'.</param>
        /// <param name="entryType">EntryType for the log entry. For example: 'EntryType.Info'.</param>
        /// <param name="message">Message to log.</param>
        void Log(string logName, string logSource, EntryType entryType, string message);
    }
}
