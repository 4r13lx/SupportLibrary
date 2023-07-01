using System;
using System.Diagnostics;

namespace SupportLibrary.Logging
{
    /// <summary>
    /// Extension Methods for Log related tasks.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts this instance to an equivalent value of EventLogEntryType enum.<para/>
        /// </summary>
        /// <param name="value">Object instance to convert.</param>
        /// <returns>An EventLogEntryType enum value.</returns>
        public static EventLogEntryType ToEventLog(this EntryType value)
        {
            try
            {
                switch (value)
                {
                    case EntryType.Info:    return EventLogEntryType.Information;
                    case EntryType.Warning: return EventLogEntryType.Warning;
                    case EntryType.Error:   return EventLogEntryType.Error;
                    default:                throw new InvalidCastException(String.Format("Convertion failed from enum EntryType.{0} to a valid value on enum EventLogEntryType.", value));
                }
            }
            catch (Exception) { throw; }
        }
    }
}
