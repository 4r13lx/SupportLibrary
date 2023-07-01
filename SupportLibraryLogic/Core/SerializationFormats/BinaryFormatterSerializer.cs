using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SupportLibrary.Core
{
    internal class BinaryFormatterSerializer
    {
        /// <summary>
        /// Serialize an object using BinaryFormatter.
        /// </summary>
        /// <typeparam name="T">T-Type of the object to serialize.</typeparam>
        /// <param name="value">Object to serialize.</param>
        /// <returns>Serialized object.</returns>
        internal byte[] Serialize<T>(T value)
        {
            try
            {
                if (!typeof(T).IsSerializable) { throw new SerializationException($"Object '{ typeof(T).Name }' lacks [Serializable] attribute"); }

                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(stream, value);
                    stream.Flush();

                    return stream.ToArray();
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Deserializes an object using BinaryFormatter.
        /// </summary>
        /// <typeparam name="TResult">T-Type of the object to deserialize.</typeparam>
        /// <param name="value">Object to deserialize.</param>
        /// <returns>Deserialized object.</returns>
        internal TResult Deserialize<TResult>(byte[] value)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(value))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    return (TResult)serializer.Deserialize(stream);
                }
            }
            catch (Exception) { throw; }
        }
    }
}
