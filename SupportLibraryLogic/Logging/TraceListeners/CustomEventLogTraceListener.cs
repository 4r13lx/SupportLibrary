using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SupportLibrary.Logging.TraceListeners
{
    /// <summary>
    /// Provides a simple listener that directs tracing or debugging output to an System.Diagnostics.EventLog.
    /// </summary>
    public class CustomEventLogTraceListener : CustomTraceListener
    {
        private EventLog eventLog = null;

        /// <summary>
        /// Initializes a new instance of the CustomEventLogTraceListener class without a trace listener using the specified event log.
        /// </summary>
        /// <param name="eventLog">The event log to write to</param>
        public CustomEventLogTraceListener(EventLog eventLog) : base()
        {
            this.eventLog = eventLog;
        }

        /// <summary>
        /// Initializes a new instance of the CustomEventLogTraceListener class without a trace listener using the specified event log.
        /// Aditionally sets a TraceOutputOptions enum to insert aditional data before standard trace message.
        /// </summary>
        /// <param name="eventLog">The event log to write to</param>
        /// <param name="option">Flag for aditional data to insert before standard trace message</param>
        public CustomEventLogTraceListener(EventLog eventLog, TraceOption option) : base(option)
        {
            this.eventLog = eventLog;
        }

        /// <summary>
        /// Writes a message to this instance.
        /// </summary>
        /// <param name="message">A message to write.</param>
        [MethodImplAttribute(MethodImplOptions.NoInlining)]
        public override void Write(string message)
        {
            try
            {
                string output = base.BuildOptionsOutputText();
                this.eventLog.WriteEntry(output + message, EventLogEntryType.Information);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Writes a message to this instance followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        [MethodImplAttribute(MethodImplOptions.NoInlining)]
        public override void WriteLine(string message)
        {
            try
            {
                string output = base.BuildOptionsOutputText();
                this.eventLog.WriteEntry(output + message, EventLogEntryType.Information);
            }
            catch (Exception) { throw; }
        }
    }
}
