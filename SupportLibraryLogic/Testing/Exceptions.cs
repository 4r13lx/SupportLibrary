using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SupportLibrary.Testing
{
    /// <summary>
    /// The exception that is thrown if a call on AssertEx.Exceptions.Throws() fails.
    /// </summary>
    public sealed class AssertFailedExceptionEx : AssertFailedException
    {
        /// <summary>
        /// Gets the cause of failure in the assertion.
        /// </summary>
        public AssertFailedExceptionCause AssertFailedExceptionCause { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assertFailedExceptionCause">Cause of failure in the assertion.</param>
        /// <param name="message">The message that describes the error.</param>
        public AssertFailedExceptionEx(AssertFailedExceptionCause assertFailedExceptionCause, String message)
            : base(message)
        {
            this.AssertFailedExceptionCause = assertFailedExceptionCause;
        }
    }
}
