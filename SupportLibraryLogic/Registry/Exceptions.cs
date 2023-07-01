using System;

namespace SupportLibrary.Registry
{
    /// <summary>
    /// The base class that represents errors that occur during Windows Registry operation.
    /// </summary>
    public class RegistryException : Exception
    {
        /// <summary>
        /// Gets the Name of the RegistryKey that caused the current exception.
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// The exception that is thrown when a Windows Registry SubKey was not found.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="keyName">The name of the RegistryKey that caused the current exception.</param>
        public RegistryException(string message, string keyName)
            : base(message) { this.KeyName = keyName; }
    }

    /// <summary>
    /// The exception that is thrown after an attemp to access a Windows Registry SubKey that no exists.
    /// </summary>
    public sealed class RegSubKeyNotFoundException : RegistryException
    {
        /// <summary>
        /// The exception that is thrown when a Windows Registry SubKey was not found.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="keyName">The name of the RegistryKey that caused the current exception.</param>
        public RegSubKeyNotFoundException(string message, string keyName)
            : base(message, keyName) { }
    }

    /// <summary>
    /// The exception that is thrown after an attemp to access a Windows Registry Value that no exists.
    /// </summary>
    public sealed class RegValueNotFoundException : RegistryException
    {
        /// <summary>
        /// Gets the name of the Value that caused the current exception.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The exception that is thrown when a Windows Registry Value was not found.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="keyName">The name of the RegistryKey that caused the current exception.</param>
        /// <param name="valueName">The name of the Value that caused the current exception.</param>
        public RegValueNotFoundException(string message, string keyName, string valueName)
            : base(message, keyName) { this.Value = valueName; }
    }
}
