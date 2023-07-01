using System.ComponentModel;

namespace SupportLibrary.Core
{
    /// <summary>
    /// Identifies a XML serialization mode.
    /// </summary>
    [DefaultValue(None)]
    public enum XmlSerialization
    {
        /// <summary>
        /// Means an unknown serialization format.
        /// </summary>
        None = 0,

        /// <summary>
        /// Means XML format, using DataContract serializer.
        /// </summary>
        DataContract = 1,

            /// <summary>
        /// Means XML format, using XmlSerializer serializer.
        /// </summary>
        XmlSerializer = 2,
    }

    /// <summary>
    /// Identifies a serialization mode.
    /// </summary>
    [DefaultValue(None)]
    public enum Serialization
    {
        /// <summary>
        /// Means an unknown serialization format.
        /// </summary>
        None = 0,

        /// <summary>
        /// Means Binary format, using BinaryFormatter serializer.
        /// </summary>
        BinaryFormatter = 1,

        /// <summary>
        /// Means XML format, using DataContract serializer.
        /// </summary>
        DataContract = 2,

        /// <summary>
        /// Means XML format, using XmlSerializer serializer.
        /// </summary>
        XmlSerializer = 3
    }
}
