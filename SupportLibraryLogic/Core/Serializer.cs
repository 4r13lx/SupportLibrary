using System;
using SupportLibrary.IO;

namespace SupportLibrary.Core
{
    /// <summary>
    /// Helper class for Serializing related tasks.<para/>
    /// For example: SerializeToBinary(), SerializeToXml() or SerializeToFile().
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Serializes the specified object to Binary format.
        /// </summary>
        /// <typeparam name="T">T-Type of the object to serialize.</typeparam>
        /// <param name="value">Object to serialize.</param>
        /// <returns>Byte[] representation of serialized object.</returns>
        public static byte[] SerializeToBinary<T>(T value)
        {
            try
            {
                if (value == null)  { throw new ArgumentNullException(nameof(value), $"{ value } is null."); }

                return SerializationFormats.BinaryFormatter.Serialize<T>(value);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Deserializes the specified Binary byte[] to an object of the specified type T.
        /// </summary>
        /// <typeparam name="TResult">T-Type of the object to deserialize.</typeparam>
        /// <param name="value">Byte[] to deserialize.</param>
        /// <returns>Deserialized object.</returns>
        public static TResult DeserializeFromBinary<TResult>(byte[] value)
        {
            try
            {
                if (value == null) { throw new ArgumentNullException(nameof(value), $"{ value } is null."); }

                return SerializationFormats.BinaryFormatter.Deserialize<TResult>(value);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Serializes the specified object to XML format.
        /// </summary>
        /// <typeparam name="T">T-Type of the object to serialize.</typeparam>
        /// <param name="value">Object to serialize.</param>
        /// <param name="format">Identifies the XML serialization mode.</param>
        /// <returns>String representation of serialized object.</returns>
        public static string SerializeToXml<T>(T value, XmlSerialization format)
        {
            try
            {
                if (value == null)  { throw new ArgumentNullException(nameof(value), $"{ value } is null."); }
                if (format == XmlSerialization.None) { throw new ArgumentException(nameof(format), "Invalid xml serilization format."); }

                if (format == XmlSerialization.DataContract)        { return SerializationFormats.DataContract.Serialize<T>(value); }
                else if (format == XmlSerialization.XmlSerializer)  { return SerializationFormats.XmlSerializer.Serialize<T>(value); }

                throw new ArgumentException("Unknown xml serialization format.", nameof(format));
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Deserializes the specified XML string to an object of the specified type T.
        /// </summary>
        /// <typeparam name="TResult">T-Type of the object to deserialize.</typeparam>
        /// <param name="value">String to deserialize.</param>
        /// <param name="format">Identifies the XML deserialization mode.</param>
        /// <returns>Deserialized object.</returns>
        public static TResult DeserializeFromXml<TResult>(string value, XmlSerialization format)
        {
            try
            {
                if (value == null) { throw new ArgumentNullException(nameof(value), $"{ value } is null."); }
                if (format == XmlSerialization.None) { throw new ArgumentException(nameof(format), "Invalid xml serilization format."); }

                if (format == XmlSerialization.DataContract)        { return SerializationFormats.DataContract.Deserialize<TResult>(value); }
                else if (format == XmlSerialization.XmlSerializer)  { return SerializationFormats.XmlSerializer.Deserialize<TResult>(value); }

                throw new ArgumentException("Unknown xml serialization format.", nameof(format));
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Serializes the specified object to the chosen format and then saves it to disk.
        /// </summary>
        /// <typeparam name="T">T-Type of the object to serialize.</typeparam>
        /// <param name="value">Object to serialize.</param>
        /// <param name="filePath">Directory path to the file. If not exits it will be created.</param>
        /// <param name="fileName">Name of the file. If exits it will be overwritten.</param>
        /// <param name="format">Identifies the serialization mode.</param>
        public static void SerializeToFile<T>(T value, string filePath, string fileName, Serialization format)
        {
            try
            {
                if (value == null)      { throw new ArgumentNullException(nameof(value), "Value is null."); }
                if (filePath == null)   { throw new ArgumentNullException(nameof(filePath), "FilePath is null."); }
                if (fileName == null)   { throw new ArgumentNullException(nameof(fileName), "FileName is null."); }
                if (format == Serialization.None) { throw new ArgumentException(nameof(format), "Invalid serilization format."); }

                if (format == Serialization.BinaryFormatter)
                {
                    byte[] fileContent = SerializationFormats.BinaryFormatter.Serialize<T>(value);
                    new FileHelper().SaveBinary(filePath, fileName, fileContent);
                }
                else if (format == Serialization.DataContract)
                {
                    string fileContent = SerializationFormats.DataContract.Serialize<T>(value);
                    new FileHelper().SaveText(filePath, fileName, fileContent);
                }
                else if (format == Serialization.XmlSerializer)
                {
                    string fileContent = SerializationFormats.XmlSerializer.Serialize<T>(value);
                    new FileHelper().SaveText(filePath, fileName, fileContent);
                }
                else
                {
                    throw new ArgumentException("Unknown serialization format.", nameof(format));
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Deserializes from the specified file to an object of the specified type T.
        /// </summary>
        /// <typeparam name="TResult">T-Type of the object to deserialize.</typeparam>
        /// <param name="filePath">Directory path to the file. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="fileName">Name of the file. If not exits a FileNotFoundException will be thrown.</param>
        /// <param name="format">Identifies the serialization mode.</param>
        /// <returns>Deserialized object.</returns>
        public static TResult DeserializeFromFile<TResult>(string filePath, string fileName, Serialization format)
        {
            try
            {
                if (filePath == null)   { throw new ArgumentNullException(nameof(filePath), "FilePath is null."); }
                if (fileName == null)   { throw new ArgumentNullException(nameof(fileName), "FileName is null."); }
                if (format == Serialization.None) { throw new ArgumentException(nameof(format), "Invalid serilization format."); }

                if (format == Serialization.BinaryFormatter)
                {
                    byte[] fileContent = new FileHelper().ReadBinary(filePath, fileName);
                    return SerializationFormats.BinaryFormatter.Deserialize<TResult>(fileContent);
                }
                else if (format == Serialization.DataContract)
                {
                    string fileContent = new FileHelper().ReadText(filePath, fileName);
                    return SerializationFormats.DataContract.Deserialize<TResult>(fileContent);
                }
                else if (format == Serialization.XmlSerializer)
                {
                    string fileContent = new FileHelper().ReadText(filePath, fileName);
                    return SerializationFormats.XmlSerializer.Deserialize<TResult>(fileContent);
                }
                else
                {
                    throw new ArgumentException("Unknown serialization format.", nameof(format));
                }
            }
            catch (Exception) { throw; }
        }
    }
}
