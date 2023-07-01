using System;

namespace SupportLibrary.Logging.LogEntry
{
    sealed class LogEntryMinimal : ILogEntryProvider
    {
        private EntryType entryType;

        public LogEntryMinimal(EntryType type)
        {
            this.entryType = type;
        }

        public string BuildHeader()
        {
            return "";
        }

        public string BuildTitle()
        {

            return String.Format("{0} {1,-9} -> ", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), "(" + this.entryType.ToString().ToUpper() + ")");
        }

        public string BuildContent(string message)
        {
            return message;
        }

        public string BuildFooter()
        {
            return Environment.NewLine;
        }
    }
}
