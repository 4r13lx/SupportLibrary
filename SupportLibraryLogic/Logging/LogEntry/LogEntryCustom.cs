using System;
using System.Text.RegularExpressions;

namespace SupportLibrary.Logging.LogEntry
{
    sealed class LogEntryCustom : ILogEntryProvider
    {
        private EntryType entryType;
        private string customHeader;
        private string customTitle;
        private string customFooter;
        private int customAutoId;

        public LogEntryCustom(EntryType type, string header, string title, string footer, int autoId)
        {
            this.entryType = type;
            this.customHeader = header;
            this.customTitle = title;
            this.customFooter = footer;
            this.customAutoId = autoId;
        }

        public string BuildHeader()
        {
            return this.customHeader + Environment.NewLine;
        }

        public string BuildTitle()
        {
            string title = this.customTitle;

            title = Regex.Replace(title, @"{id{1}[^{]*}{1}", new MatchEvaluator(ReplaceId));
            title = Regex.Replace(title, @"{level{1}[^{]*}{1}", new MatchEvaluator(ReplaceLevel));
            title = Regex.Replace(title, @"{datetime{1}[^{]*}{1}", new MatchEvaluator(ReplaceDate));

            return title + Environment.NewLine;
        }

        /// <summary>
        /// Some allowed paterns: {id}, {id:}, {id:0}, {id:00}, {id:000}, etc
        /// </summary>
        private string ReplaceId(Match match)
        {
            string format = match.Value.Replace("id", "0");
            return String.Format(format, this.customAutoId);
        }

        /// <summary>
        /// Some allowed paterns: {level}, {level,10}, {level,-10}, etc
        /// </summary>
        private string ReplaceLevel(Match match)
        {
            string format = match.Value.Replace("level", "0");
            return String.Format(format, this.entryType.ToString().ToUpper());
        }

        /// <summary>
        /// Some allowed paterns: {datetime}, {datetime:dd/MM/yy}, {datetime:yyyy-MM-dd}, {datetime:hh:mm:ss}, etc
        /// </summary>
        private string ReplaceDate(Match match)
        {
            string format = match.Value.Replace("datetime", "0");
            return String.Format(format, DateTime.Now);
        }

        public string BuildContent(string message)
        {
            return message + Environment.NewLine;
        }

        public string BuildFooter()
        {
            return this.customFooter + Environment.NewLine;
        }
    }
}
