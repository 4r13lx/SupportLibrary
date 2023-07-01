using System;
using System.Text;
using System.Diagnostics;

using SupportLibrary.Text;

namespace SupportLibrary.Logging
{
    /// <summary>
    /// Helper class for Windows Event Log related tasks.<para/>
    /// For example: Log messages and exceptions.
    /// </summary>
    public sealed class EventLogHelper : LogHelper, IEventLogHelper
    {
        /// <summary>
        /// Gets current EventLog.LogName setting.
        /// </summary>
        public string LogName { get; private set; }

        /// <summary>
        /// Gets current EventLog.LogSource setting.
        /// </summary>
        public string LogSource { get; private set; }

        /// <summary>
        /// Constructor. Setups the LogName and LogSource for logging.
        /// </summary>
        /// <param name="logName">Name for the log entry. For example: 'SupportLibrary'.</param>
        /// <param name="logSource">Source for the log entry. For example: 'SupportLibraryLogSource'.</param>
        public EventLogHelper(string logName, string logSource)
        {
            this.LogName = logName;
            this.LogSource = logSource;
        }

        /// <summary>
        /// Logs an exception into the Windows Event Log.
        /// </summary>
        /// <param name="exception">Exception to log.</param>
        public override void Log(Exception exception)
        {
            try
            {
                string message = base.BuildExceptionMessage(exception);
                Log(this.LogName, this.LogSource, EntryType.Error, message);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Logs a message into the Windows Event Log.
        /// </summary>
        /// <param name="entryType">EntryType for the log entry. For example: 'EntryType.Info'.</param>
        /// <param name="message">Message to log.</param>
        public override void Log(EntryType entryType, string message)
        {
            try
            {
                Log(this.LogName, this.LogSource, entryType, message);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Logs a exception to the Windows Event Log.
        /// </summary>
        /// <param name="logName">Name for the log entry. For example: 'SupportLibrary'.</param>
        /// <param name="logSource">Source for the log entry. For example: 'SupportLibraryLogSource'.</param>
        /// <param name="exception">Exception to log.</param>
        public void Log(string logName, string logSource, Exception exception)
        {
            try
            {
                string message = base.BuildExceptionMessage(exception);
                Log(logName, logSource, EntryType.Error, message);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Logs a message to the Windows Event Log.
        /// </summary>
        /// <param name="logName">Name for the log entry. For example: 'SupportLibrary'.</param>
        /// <param name="logSource">Source for the log entry. For example: 'SupportLibraryLogSource'.</param>
        /// <param name="entryType">EntryType for the log entry. For example: 'EntryType.Info'.</param>
        /// <param name="message">Message to log.</param>
        public void Log(string logName, string logSource, EntryType entryType, string message)
        {
            try
            {
                // validation
                if (logName.IsNullOrEmpty())    { throw new ArgumentNullException("logName", "LogName is null or empty."); }
                if (logSource.IsNullOrEmpty())  { throw new ArgumentNullException("logSource", "LogSource is null or empty."); }

                // create a new EventSource
                if (!EventLog.SourceExists(logSource))  { EventLog.CreateEventSource(logSource, logName); }
                
                // create a new EventLogEntry
                EventLog eventLog = new EventLog(logName, ".", logSource);
                eventLog.WriteEntry(message, entryType.ToEventLog());
            }
            catch (Exception) { throw; }
        }
    }
}
