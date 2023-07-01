using System;
using System.ComponentModel;

namespace SupportLibrary.Testing
{
    /// <summary>
    /// Cause of failure in the assertion.
    /// </summary>
    [DefaultValue(NoExceptionThrown)]
    public enum AssertFailedExceptionCause
    {
        /// <summary>
        /// Means an unknown assert fail cause.
        /// </summary>
        None = 0,

        /// <summary>
        /// Means that no exception was thrown.
        /// </summary>
        NoExceptionThrown = 1,

        /// <summary>
        /// Means that expected exception was not thrown. Instead, a different type of exception was thrown.
        /// </summary>
        DiferentExceptionThrown = 2
    }
}
