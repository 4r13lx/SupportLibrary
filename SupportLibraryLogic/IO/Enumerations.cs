using System.ComponentModel;

namespace SupportLibrary.IO
{
    /// <summary>
    /// Identifies the file Operation Mode. For example: 'Create' or 'Append'.
    /// </summary>
    [DefaultValue(None)]
    public enum TextFileMode
    {
        /// <summary>
        /// Means an unknown file mode.
        /// </summary>
        None = 0,

        /// <summary>
        /// Means Create file mode. If file exits it will be overwritten.
        /// </summary>
        Create = 1,

        /// <summary>
        /// Means Append file mode. If file not exits it will be created.
        /// </summary>
        Append = 2
    }
}
