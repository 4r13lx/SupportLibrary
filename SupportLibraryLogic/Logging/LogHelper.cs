using System;
using System.Text;

namespace SupportLibrary.Logging
{
    /// <summary>
    /// Base class for Log related tasks.<para/>
    /// </summary>
    public abstract class LogHelper
    {
        /// <summary>
        /// Build a description message from the given exception.
        /// </summary>
        /// <param name="exception">Exception source from which build a description message.</param>
        /// <returns>A string with description message from the given exception</returns>
        protected string BuildExceptionMessage(Exception exception)
        {
            try
            {
                if (exception == null) { throw new ArgumentNullException("exception", "Exception is null."); }

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("An exception has occurred ->");
                stringBuilder.AppendLine(" - Exeption    : " + exception.GetType().ToString());
                stringBuilder.AppendLine(" - Message     : " + exception.Message.Replace(Environment.NewLine, " "));
                stringBuilder.Append(" - Stack Trace : " + exception.StackTrace);

                return stringBuilder.ToString();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Logs an exception.
        /// </summary>
        /// <param name="exception">Exception to log.</param>
        public abstract void Log(Exception exception);

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="entryType">EntryType for the log entry. For example: 'TextLogEntryType.Information'.</param>
        /// <param name="message">The buffer containing text to write on the log file.</param>
        public abstract void Log(EntryType entryType, string message);
    }
}
