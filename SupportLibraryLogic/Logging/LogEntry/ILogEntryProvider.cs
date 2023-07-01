using System;

namespace SupportLibrary.Logging.LogEntry
{
    interface ILogEntryProvider
    {
        string BuildHeader();

        string BuildTitle();

        string BuildContent(string message);

        string BuildFooter();
    }
}
