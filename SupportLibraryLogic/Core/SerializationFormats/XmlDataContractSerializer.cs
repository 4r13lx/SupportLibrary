using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace SupportLibrary.Core
{
    internal class XmlDataContractSerializer
    {
        /// <summary>
        /// Serialize an object using DataContractSerializer.
        /// </summary>
        /// <typeparam name="T">T-Type of the object to serialize.</typeparam>
        /// <param name="value">Object to serialize.</param>
        /// <returns>String representation of serialized object.</returns>
        internal string Serialize<T>(T value)
        {
            try
            {
                if (!typeof(T).IsSerializable) { throw new SerializationException($"Object '{ typeof(T).Name }' lacks [Serializable] attribute"); }

                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented })
                    {
                        DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
                        dataContractSerializer.WriteObject(xmlTextWriter, value);

                        return stringWriter.GetStringBuilder().ToString();
                    }
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Deserializes an object using DataContractSerializer.
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

                    DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(TResult));
                    return (TResult)dataContractSerializer.ReadObject(stream);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
