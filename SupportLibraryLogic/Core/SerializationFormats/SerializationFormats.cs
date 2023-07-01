namespace SupportLibrary.Core
{
    internal static class SerializationFormats
    {
        /// <summary>
        /// Serialize/Deserialize an object using BinaryFormatter.
        /// </summary>
        public static readonly BinaryFormatterSerializer BinaryFormatter = new BinaryFormatterSerializer();

        /// <summary>
        /// Serialize/Deserialize an object using DataContractSerializer.
        /// </summary>
        public static readonly XmlDataContractSerializer DataContract = new XmlDataContractSerializer();

        /// <summary>
        /// Serialize/Deserialize an object using XmlSerializer.
        /// </summary>
        public static readonly XmlStandardSerializer XmlSerializer = new XmlStandardSerializer();
    }
}
