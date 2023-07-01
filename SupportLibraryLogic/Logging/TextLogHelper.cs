using System;
using System.Text;

using SupportLibrary.IO;
using SupportLibrary.Text;
using SupportLibrary.Logging.LogEntry;

namespace SupportLibrary.Logging
{
    /// <summary>
    /// Helper class for Windows Event Log related tasks.<para/>
    /// For example: Log messages and exceptions.
    /// </summary>
    public sealed class TextLogHelper : LogHelper, ITextLogHelper
    {
        #region Private Members

        private int logAutoId = 1;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets current LogPath setting.
        /// </summary>
        public string LogFilePath { get; private set; }

        /// <summary>
        /// Gets current LogName setting.
        /// </summary>
        public string LogFileName { get; private set; }

        /// <summary>
        /// Gets current RevertImpersonation setting.
        /// </summary>
        public bool RevertImpersonation { get; private set; }

        /// <summary>
        /// Gets current EntryTemplate setting.
        /// </summary>
        public EntryTemplate EntryTemplate { get; private set; }

        /// <summary>
        /// Gets current string to use as Header of log entries.
        /// </summary>
        public string CustomHeader { get; private set; }

        /// <summary>
        /// Get current string to use as Title of log entries.
        /// </summary>
        public string CustomTitle { get; private set; }

        /// <summary>
        /// Gets current string to use as Footer of log entries.
        /// </summary>
        public string CustomFooter { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor. Setups the LogFile for logging. Uses EntryTemplate.Minimal.
        /// </summary>
        /// <param name="logFilePath">Directory path to the log file. If not exits it will be created. For example: 'C:\Logs\'.</param>
        /// <param name="logFileName">Name of the log file. If exits it will be overwritten. For example: 'SupportLibrary.log'.</param>
        /// <remarks>Uses the current WindowsIdentity.ImpersonationLevel settings.</remarks>
        public TextLogHelper(string logFilePath, string logFileName)
            : this(logFilePath, logFileName, false, EntryTemplate.Minimal)
        {
        }

        /// <summary>
        /// Constructor. Setups the LogFile and EntryTemplate for logging.
        /// </summary>
        /// <param name="logFilePath">Directory path to the log file. If not exits it will be created. For example: 'C:\Logs\'.</param>
        /// <param name="logFileName">Name of the log file. If exits it will be overwritten. For example: 'SupportLibrary.log'.</param>
        /// <param name="entryTemplate">Identifies how TextLogHelper must format the output. For example: 'Minimal', 'Standard' or 'Custom'.</param>
        /// <remarks>Uses the current WindowsIdentity.ImpersonationLevel settings.</remarks>
        public TextLogHelper(string logFilePath, string logFileName, EntryTemplate entryTemplate)
            : this(logFilePath, logFileName, false, entryTemplate)
        {
        }
        
        /// <summary>
        /// Constructor. Setups the LogFile, EntryTemplate and the use of Impersonation for logging.
        /// </summary>
        /// <param name="logFilePath">Directory path to the log file. If not exits it will be created. For example: 'C:\Logs\'.</param>
        /// <param name="logFileName">Name of the log file. If exits it will be overwritten. For example: 'SupportLibrary.log'.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while saving the file.</param>
        /// <param name="entryTemplate">Identifies how to format the output. For example: 'Minimal', 'Standard' or 'Custom'.</param>
        public TextLogHelper(string logFilePath, string logFileName, bool revertImpersonation, EntryTemplate entryTemplate)
        {
            this.LogFilePath = logFilePath;
            this.LogFileName = logFileName;
            this.RevertImpersonation = revertImpersonation;
            this.EntryTemplate = entryTemplate;
        }

        #endregion

        #region Public Setters methods

        /// <summary>
        /// Sets a string to use as Header of log entries. Also sets EntryTemplate to Custom mode.
        /// </summary>
        /// <param name="entryHeader">String to use as header of log entries.</param>
        /// <returns>A reference to this instance.</returns>
        public TextLogHelper SetHeader(string entryHeader)
        {
            try
	        {
                this.EntryTemplate = EntryTemplate.Custom;
                this.CustomHeader = entryHeader;
                
                return this;
	        }
	        catch (Exception) { throw; }
        }

        /// <summary>
        /// Sets a character repetition to use as Header of log entries. Also sets EntryTemplate to Custom mode.
        /// </summary>
        /// <param name="c">An Unicode character.</param>
        /// <param name="count">The number of times 'c' occurs.</param>
        /// <returns>A reference to this instance.</returns>
        public TextLogHelper SetHeader(char c, byte count)
        {
            try
            {
                this.EntryTemplate = EntryTemplate.Custom;
                this.CustomHeader = new String(c, count);

                return this;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Sets a string to use as Title of log entries. Replacement literals allowed: {id}, {level} or {datetime}.<para/>
        /// - Literal {id} will be replaced with a incremental number. For example: '1', '2', '3', '4'.<para/>
        /// - Literal {level} will be replaced with the current entrytype. For example: 'INFO', 'WARNING' or 'ERROR'.<para/>
        /// - Literal {datetime} will be replaced with the current datetime. For example: '2016-12-31 12:00:00'<para/>
        /// All String.Format() format expressions are allowed.<para/>
        /// Some examples: {id:0000} --> '0001', {level,8} --> '&#160;&#160;&#160;&#160;INFO', {datetime:dd-MM-yyyy} --> '31-12-2016'.<para/>
        /// For more information see: https://msdn.microsoft.com/es-es/library/system.string.format.aspx/
        /// </summary>
        /// <param name="entryTitle">String to use as title of log entries.</param>
        /// <returns>A reference to this instance.</returns>
        public TextLogHelper SetTitle(string entryTitle)
        {
            try
            {
                this.EntryTemplate = EntryTemplate.Custom;
                this.CustomTitle = entryTitle;

                return this;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Sets a string to use as Footer of log entries. Also sets EntryTemplate to Custom mode.
        /// </summary>
        /// <param name="entryFooter">String to use as header of log entries.</param>
        /// <returns>A reference to this instance.</returns>
        public TextLogHelper SetFooter(string entryFooter)
        {
            try
            {
                this.EntryTemplate = EntryTemplate.Custom;
                this.CustomFooter = entryFooter;

                return this;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Sets a character repetition to use as Footer of log entries. Also sets EntryTemplate to Custom mode.
        /// </summary>
        /// <param name="c">An Unicode character.</param>
        /// <param name="count">The number of times 'c' occurs.</param>
        /// <returns>A reference to this instance.</returns>
        public TextLogHelper SetFooter(char c, byte count)
        {
            try
            {
                this.EntryTemplate = EntryTemplate.Custom;
                this.CustomFooter = new String(c, count);

                return this;
            }
            catch (Exception) { throw; }
        }

        #endregion

        #region Public Log methods

        /// <summary>
        /// Logs an exception into the log file.
        /// </summary>
        /// <param name="exception">Exception to log.</param>
        public override void Log(Exception exception)
        {
            try
            {
                string message = base.BuildExceptionMessage(exception);
                Log(EntryType.Error, message);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Logs a message into the Log File.
        /// </summary>
        /// <param name="entryType">EntryType for the log entry. For example: 'TextLogEntryType.Information'.</param>
        /// <param name="message">The buffer containing text to write on the log file.</param>
        public override void Log(EntryType entryType, string message)
        {
            try
            {
                if (this.LogFilePath.IsNullOrEmpty())   { throw new ArgumentNullException("logFilePath", "LogFilePath is null or empty."); }
                if (this.LogFileName.IsNullOrEmpty())   { throw new ArgumentNullException("logFileName", "LogFileName is null or empty."); }
                if (entryType == EntryType.None)        { throw new ArgumentNullException("logType", "LogType is Unknown."); }
                if (message == null)                    { throw new ArgumentNullException("message", "Message is null."); }

                ILogEntryProvider logEntryProvider = null;
                if (this.EntryTemplate == EntryTemplate.Minimal)    { logEntryProvider = new LogEntryMinimal(entryType); }
                if (this.EntryTemplate == EntryTemplate.Standard)   { logEntryProvider = new LogEntryStandard(entryType); }
                if (this.EntryTemplate == EntryTemplate.Custom)     { logEntryProvider = new LogEntryCustom(entryType, this.CustomHeader, this.CustomTitle, this.CustomFooter, this.logAutoId++); }

                // build the string content for a log entry.
                StringBuilder sb = new StringBuilder();
                sb.Append(logEntryProvider.BuildHeader());
                sb.Append(logEntryProvider.BuildTitle());
                sb.Append(logEntryProvider.BuildContent(message));
                sb.Append(logEntryProvider.BuildFooter());

                new FileHelper().SaveText(this.LogFilePath, this.LogFileName, TextFileMode.Append, sb.ToString(), this.RevertImpersonation);
            }
            catch (Exception) { throw; }
        }

        #endregion
    }
}
