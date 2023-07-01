using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SupportLibrary.Logging.TraceListeners
{
    /// <summary>
    /// Directs tracing or debugging output to either the standard output or the standard error stream.
    /// </summary>
    public class CustomConsoleTraceListener : CustomTraceListener
    {
        /// <summary>
        /// Initializes a new instance of the CustomConsoleTraceListener class with trace output written to the standard output stream.
        /// </summary>
        public CustomConsoleTraceListener() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CustomConsoleTraceListener class with trace output written to the standard output stream.
        /// Aditionally sets a TraceOutputOptions enum to insert aditional data before standard trace message.
        /// </summary>
        /// <param name="option">Flag for aditional data to insert before standard trace message</param>
        public CustomConsoleTraceListener(TraceOption option) : base(option)
        {
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
                Console.Write(output + message);
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
                Console.WriteLine(output + message);
            }
            catch (Exception) { throw; }
        }
    }
}
