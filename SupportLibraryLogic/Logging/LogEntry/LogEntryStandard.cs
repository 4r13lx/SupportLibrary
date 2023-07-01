using System;

namespace SupportLibrary.Logging.LogEntry
{
    sealed class LogEntryStandard : ILogEntryProvider
    {
        private EntryType entryType;

        public LogEntryStandard(EntryType type)
        {
            this.entryType = type;
        }

        public string BuildHeader()
        {
            string header = new String('-', 40);
            return header + Environment.NewLine;
        }

        public string BuildTitle()
        {
            string title = String.Format("{0} - {1}", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), this.entryType.ToString().ToUpper());
            return title + Environment.NewLine;
        }

        public string BuildContent(string message)
        {
            return message + Environment.NewLine;
        }

        public string BuildFooter()
        {
            string footer = new String('-', 40);
            return footer + Environment.NewLine;
        }
    }
}
