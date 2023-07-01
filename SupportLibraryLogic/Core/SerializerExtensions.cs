using System;

namespace SupportLibrary.Core
{
    /// <summary>
    /// Extension Methods for Serializing related tasks.<para/>
    /// For example: SerializeToBinary(), SerializeToXml() or SerializeToFile().
    /// </summary>
    public static class SerializerExtensions
    {
        /// <summary>
        /// Serializes this instance to Binary format.
        /// </summary>
        /// <typeparam name="T">T-Type of the object to serialize.</typeparam>
        /// <param name="value">Object to serialize.</param>
        /// <returns>Byte[] representation of the serialized object.</returns>
        public static byte[] SerializeToBinary<T>(this T value)
        {
            try
            {
                return Serializer.SerializeToBinary<T>(value);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Serializes this instance to XML format.
        /// </summary>
        /// <typeparam name="T">T-Type of the object to serialize.</typeparam>
        /// <param name="value">Object to serialize.</param>
        /// <param name="format">Identifies the XML serialization mode.</param>
        /// <returns>String representation of the serialized object.</returns>
        public static string SerializeToXml<T>(this T value, XmlSerialization format)
        {
            try
            {
                return Serializer.SerializeToXml<T>(value, format);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Serializes this instance to the chosen format and then saves it to disk.
        /// </summary>
        /// <typeparam name="T">T-Type of the object to serialize.</typeparam>
        /// <param name="value">Object to serialize.</param>
        /// <param name="filePath">Directory path to the file. If not exits it will be created.</param>
        /// <param name="fileName">Name of the file. If exits it will be overwritten.</param>
        /// <param name="format">Identifies the serialization mode.</param>
        public static void SerializeToFile<T>(this T value, string filePath, string fileName, Serialization format)
        {
            try
            {
                Serializer.SerializeToFile<T>(value, filePath, fileName, format);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Deserializes this instance from Binary format to an object of the specified Type-T.
        /// </summary>
        /// <typeparam name="TResult">T-Type of the object to deserialize.</typeparam>
        /// <param name="value">Byte[] to deserialize.</param>
        /// <returns>Deserialized object.</returns>
        public static TResult DeserializeFromBinary<TResult>(this byte[] value)
        {
            try
            {
                return Serializer.DeserializeFromBinary<TResult>(value);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Deserializes this instance from XML format to an object of the specified Type-T.
        /// </summary>
        /// <typeparam name="TResult">T-Type of the object to deserialize.</typeparam>
        /// <param name="value">String to deserialize.</param>
        /// <param name="format">Identifies the XML deserialization mode.</param>
        /// <returns>Deserialized object.</returns>
        public static TResult DeserializeFromXml<TResult>(this string value, XmlSerialization format)
        {
            try
            {
                return Serializer.DeserializeFromXml<TResult>(value, format);
            }
            catch (Exception) { throw; }
        }
    }
}
