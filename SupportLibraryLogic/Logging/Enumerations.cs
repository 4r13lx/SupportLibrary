using System;
using System.ComponentModel;

namespace SupportLibrary.Logging
{
    /// <summary>
    /// Identifies the Type of a log entry. For example: 'Information', 'Warning' or 'Error'.
    /// </summary>
    [DefaultValue(None)]
    public enum EntryType
    {
        /// <summary>
        /// Means an unknown type log entry.
        /// </summary>
        None = 0,

        /// <summary>
        /// Means a Information type log entry.
        /// </summary>
        Info = 1,

        /// <summary>
        /// Means a Warning type log entry.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Means a Error type log entry.
        /// </summary>
        Error = 3
    }

    /// <summary>
    /// Identifies how to format the output. For example: 'Minimal', 'Standard' or 'Custom'.
    /// </summary>
    [DefaultValue(Minimal)]
    public enum EntryTemplate
    {
        /// <summary>
        /// Do not includes header or footer. Includes a title with the current datetime.<para/>
        /// Standard title is '{datetime} ({level}) -> '.
        /// </summary>
        Minimal = 0,

        /// <summary>
        /// Includes a standard header and footer. Includes a title with the current datetime.<para/>
        /// Standard header format is '----------------------------------------'.<para/>
        /// Standard title is '{datetime} - {level}'.
        /// Standard footer format is '----------------------------------------'.<para/>
        /// </summary>
        Standard = 1,

        /// <summary>
        /// Custom entry template. Takes header, title and footer from namesakes Properties.
        /// </summary>
        Custom = 2
    }
}

namespace SupportLibrary.Logging.TraceListeners
{
    /// <summary>
    /// Identifies the trace option to insert aditional data before standard trace message. For example: 'DateTime'.
    /// </summary>
    public enum TraceOption
    {
        /// <summary>
        /// Insert nothing to standard trace output.
        /// </summary>
        None = 0,

        /// <summary>
        /// Insert current datetime to standard trace output.
        /// </summary>
        DateTimeOnly = 1,

        /// <summary>
        /// Insert method name to standard trace output.
        /// </summary>
        MethodOnly = 2,

        /// <summary>
        /// Insert class name and method name to standard trace output.
        /// </summary>
        ClassAndMethod = 3,

        /// <summary>
        /// Insert class full qualified name and method name to standard trace output.
        /// </summary>
        FullClassAndMethod = 4,

        /// <summary>
        /// Insert current datetime and method name to standard trace output.
        /// </summary>
        DateTimeAndMethod = 5,

        /// <summary>
        /// Insert current datetime, class name and method name to standard trace output.
        /// </summary>
        DateTimeAndClassAndMethod = 6,

        /// <summary>
        /// Insert current datetime, class full qualified name and method name to standard trace output.
        /// </summary>
        DateTimeAndFullClassAndMethod = 7
    };
}
