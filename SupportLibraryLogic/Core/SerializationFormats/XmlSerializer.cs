using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace SupportLibrary.Core
{
    internal class XmlStandardSerializer
    {
        /// <summary>
        /// Serialize an object using XmlSerializer.
        /// </summary>
        /// <typeparam name="T">T-Type of the object to serialize.</typeparam>
        /// <param name="value">Object to serialize.</param>
        /// <returns>String representation of serialized object.</returns>
        internal string Serialize<T>(T value)
        {
            try
            {
                if (!typeof(T).IsSerializable) { throw new SerializationException($"Object '{ typeof(T).Name }' lacks [Serializable] attribute"); }

                using (MemoryStream stream = new MemoryStream())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stream, value); // XmlWriter.Create(stream)
                    stream.Flush();
                    stream.Position = 0;

                    return new StreamReader(stream).ReadToEnd();
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Deserializes an object using XmlSerializer.
        /// </summary>
        /// <typeparam name="TResult">T-Type of the object to deserialize.</typeparam>
        /// <param name="value">Object to deserialize.</param>
        /// <returns>Deserialized object.</returns>
        internal TResult Deserialize<TResult>(string value)
        {
            try
            {
                using (Stream stream = new MemoryStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(value);
                    stream.Write(data, 0, data.Length);
                    stream.Position = 0;

                    XmlSerializer serializer = new XmlSerializer(typeof(TResult));
                    return (TResult)serializer.Deserialize(stream);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
