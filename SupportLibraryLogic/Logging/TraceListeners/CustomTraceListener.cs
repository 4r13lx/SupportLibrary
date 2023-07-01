using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SupportLibrary.Logging.TraceListeners
{
    /// <summary>
    /// Provides a base class for the custom listeners who monitor trace and debug output.
    /// </summary>
    public abstract class CustomTraceListener : TraceListener
    {
        private TraceOption traceOption = TraceOption.None;

        /// <summary>
        /// Initializes a new instance of the CustomTraceListener class with trace output written to the standard output stream.
        /// </summary>
        public CustomTraceListener()
        {
            this.traceOption = TraceOption.None;
        }

        /// <summary>
        /// Initializes a new instance of the CustomTraceListener class with trace output written to the standard output stream.
        /// Aditionally sets a TraceOutputOptions enum to insert aditional data before standard trace message.
        /// </summary>
        /// <param name="option">Flag for aditional data to insert before standard trace message</param>
        public CustomTraceListener(TraceOption option)
        {
            this.traceOption = option;
        }

        /// <summary>
        /// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Builds aditional data to insert before standard trace message.
        /// </summary>
        /// <returns>The aditional data to insert before standard trace message.</returns>
        [MethodImplAttribute(MethodImplOptions.NoInlining)]
        protected string BuildOptionsOutputText()
        {
            try
            {
                MethodBase methodBase = this.GetCallingMethod();

                string typeFullName = methodBase?.DeclaringType.FullName;
                string typeName = methodBase?.DeclaringType.Name;
                string methodName = methodBase?.Name;
                string dateTime = DateTime.Now.ToString();

                if (this.traceOption == TraceOption.None)
                    return "";

                else if (this.traceOption == TraceOption.DateTimeOnly)
                    return $"{ dateTime } - ";

                else if (this.traceOption == TraceOption.MethodOnly)
                    return $"{ methodName }() - ";

                else if (this.traceOption == TraceOption.ClassAndMethod)
                    return $"{ typeName }.{ methodName }() - ";

                else if (this.traceOption == TraceOption.FullClassAndMethod)
                    return $"{ typeFullName }.{ methodName }(){ Environment.NewLine } - ";

                else if (this.traceOption == TraceOption.DateTimeAndMethod)
                    return $"{ dateTime }, { methodName }() - ";

                else if (this.traceOption == TraceOption.DateTimeAndClassAndMethod)
                    return $"{ dateTime }, { typeName }.{ methodName }() - ";

                else if (this.traceOption == TraceOption.DateTimeAndFullClassAndMethod)
                    return $"{ dateTime }, { typeFullName }.{ methodName }(){ Environment.NewLine } - ";

                else return "";
            }
            catch (Exception) { throw; }
        }

        private MethodBase GetCallingMethod()
        {
            try
            {
                StackFrame[] stackFrames = new StackTrace().GetFrames();    // Current StackTrace
                string supportLibraryNamespace = this.GetType().Namespace;  // SupportLibrary.Logging.TraceListeners

                foreach (StackFrame stackFrame in stackFrames)
                {
                    MethodBase methodBase = stackFrame.GetMethod();

                    if (methodBase.DeclaringType.Namespace != supportLibraryNamespace &&
                        methodBase.DeclaringType.Namespace.StartsWith("System") == false)
                    {
                        return methodBase;
                    }
                }

                return null;
            }
            catch (Exception) { throw; }
        }
    }
}
