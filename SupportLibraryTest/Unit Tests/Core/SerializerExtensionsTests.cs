using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Core;
using SupportLibrary.IO;
using SupportLibrary.Testing;
using SupportLibraryTest.Entities;

namespace SupportLibraryTest.Core
{
    /// <summary>
    /// Testing of Core namespace classes.
    /// </summary>
    [TestClass]
    public class SerializerExtensionsTests
    {
        private readonly string basePath = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User);

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Serializer")]
        public void Extensions_SerializeDeserialize_ToBinary()
        {
            // arrange
            Person person = Person.CreateSamplePerson();

            // act
            byte[] serializedPerson = person.SerializeToBinary();
            Person deserializedPerson = serializedPerson.DeserializeFromBinary<Person>();

            // assert
            Assert.IsNotNull(serializedPerson, "Assert 01");
            Assert.IsNotNull(deserializedPerson, "Assert 02");
            AssertEx.Collections.IsNotEmpty(serializedPerson, "Assert 03");

            Assert.AreEqual(person.Id, deserializedPerson.Id, "Assert 04");
            Assert.AreEqual(person.FirstName, deserializedPerson.FirstName, "Assert 05");
            Assert.AreEqual(person.LastName, deserializedPerson.LastName, "Assert 06");
            Assert.AreEqual(person.Age, deserializedPerson.Age, "Assert 07");
            Assert.AreEqual(person.Tall, deserializedPerson.Tall, "Assert 08");
            Assert.AreEqual(person.Address.StreetName, deserializedPerson.Address.StreetName, "Assert 09");
            Assert.AreEqual(person.Address.StreetNumber, deserializedPerson.Address.StreetNumber, "Assert 10");
            Assert.AreEqual(person.Address.City, deserializedPerson.Address.City, "Assert 11");
            Assert.AreEqual(person.Address.State, deserializedPerson.Address.State, "Assert 12");
            Assert.AreEqual(person.Address.ZipCode, deserializedPerson.Address.ZipCode, "Assert 13");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Serializer")]
        public void Extensions_SerializeDeserialize_ToXml_DataContract()
        {
            // arrange
            Person person = Person.CreateSamplePerson();

            // act
            string serializedPerson = person.SerializeToXml(XmlSerialization.DataContract);
            Person deserializedPerson = serializedPerson.DeserializeFromXml<Person>(XmlSerialization.DataContract);

            // assert
            Assert.IsNotNull(serializedPerson, "Assert 01");
            AssertEx.Text.IsNotEmpty(serializedPerson, "Assert 02");

            Assert.AreEqual(person.Id, deserializedPerson.Id, "Assert 04");
            Assert.AreEqual(person.FirstName, deserializedPerson.FirstName, "Assert 05");
            Assert.AreEqual(person.LastName, deserializedPerson.LastName, "Assert 06");
            Assert.AreEqual(person.Age, deserializedPerson.Age, "Assert 07");
            Assert.AreEqual(person.Tall, deserializedPerson.Tall, "Assert 08");
            Assert.AreEqual(person.Address.StreetName, deserializedPerson.Address.StreetName, "Assert 09");
            Assert.AreEqual(person.Address.StreetNumber, deserializedPerson.Address.StreetNumber, "Assert 13");
            Assert.AreEqual(person.Address.City, deserializedPerson.Address.City, "Assert 11");
            Assert.AreEqual(person.Address.State, deserializedPerson.Address.State, "Assert 12");
            Assert.AreEqual(person.Address.ZipCode, deserializedPerson.Address.ZipCode, "Assert 13");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Serializer")]
        public void Extensions_SerializeDeserialize_ToXml_XmlSerializer()
        {
            // arrange
            Person person = Person.CreateSamplePerson();

            // act
            string serializedPerson = person.SerializeToXml(XmlSerialization.XmlSerializer);
            Person deserializedPerson = serializedPerson.DeserializeFromXml<Person>(XmlSerialization.XmlSerializer);

            // assert
            Assert.IsNotNull(serializedPerson, "Assert 01");
            AssertEx.Text.IsNotEmpty(serializedPerson, "Assert 02");

            Assert.AreEqual(person.Id, deserializedPerson.Id, "Assert 04");
            Assert.AreEqual(person.FirstName, deserializedPerson.FirstName, "Assert 05");
            Assert.AreEqual(person.LastName, deserializedPerson.LastName, "Assert 06");
            Assert.AreEqual(person.Age, deserializedPerson.Age, "Assert 07");
            Assert.AreEqual(person.Tall, deserializedPerson.Tall, "Assert 08");
            Assert.AreEqual(person.Address.StreetName, deserializedPerson.Address.StreetName, "Assert 09");
            Assert.AreEqual(person.Address.StreetNumber, deserializedPerson.Address.StreetNumber, "Assert 13");
            Assert.AreEqual(person.Address.City, deserializedPerson.Address.City, "Assert 11");
            Assert.AreEqual(person.Address.State, deserializedPerson.Address.State, "Assert 12");
            Assert.AreEqual(person.Address.ZipCode, deserializedPerson.Address.ZipCode, "Assert 13");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Serializer")]
        public void Extensions_SerializeDeserialize_ToFile_BinaryFormatter()
        {
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "BinaryFormatter.bin";

            // arrange
            Person person = Person.CreateSamplePerson();

            // act
            person.SerializeToFile(filePath, fileName, Serialization.BinaryFormatter);
            Person deserializedPerson = Serializer.DeserializeFromFile<Person>(filePath, fileName, Serialization.BinaryFormatter);

            byte[] fileContent = new FileHelper().ReadBinary(filePath, fileName);

            // assert
            Assert.IsNotNull(fileContent, "Assert 01");
            AssertEx.Collections.IsNotEmpty(fileContent, "Assert 02");

            Assert.AreEqual(person.Id, deserializedPerson.Id, "Assert 03");
            Assert.AreEqual(person.FirstName, deserializedPerson.FirstName, "Assert 04");
            Assert.AreEqual(person.LastName, deserializedPerson.LastName, "Assert 05");
            Assert.AreEqual(person.Age, deserializedPerson.Age, "Assert 06");
            Assert.AreEqual(person.Tall, deserializedPerson.Tall, "Assert 07");
            Assert.AreEqual(person.Address.StreetName, deserializedPerson.Address.StreetName, "Assert 08");
            Assert.AreEqual(person.Address.StreetNumber, deserializedPerson.Address.StreetNumber, "Assert 09");
            Assert.AreEqual(person.Address.City, deserializedPerson.Address.City, "Assert 10");
            Assert.AreEqual(person.Address.State, deserializedPerson.Address.State, "Assert 11");
            Assert.AreEqual(person.Address.ZipCode, deserializedPerson.Address.ZipCode, "Assert 12");

            // cleanup
            File.Delete(Path.Combine(filePath, fileName));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Serializer")]
        public void Extensions_SerializeDeserialize_ToFile_DataContract()
        {
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "DataContract.xml";

            // arrange
            Person person = Person.CreateSamplePerson();

            // act
            person.SerializeToFile(filePath, fileName, Serialization.DataContract);
            Person deserializedPerson = Serializer.DeserializeFromFile<Person>(filePath, fileName, Serialization.DataContract);

            string fileContent = new FileHelper().ReadText(filePath, fileName);

            // assert
            Assert.IsNotNull(fileContent, "Assert 01");
            AssertEx.Text.IsNotEmpty(fileContent, "Assert 02");

            Assert.AreEqual(person.Id, deserializedPerson.Id, "Assert 03");
            Assert.AreEqual(person.FirstName, deserializedPerson.FirstName, "Assert 04");
            Assert.AreEqual(person.LastName, deserializedPerson.LastName, "Assert 05");
            Assert.AreEqual(person.Age, deserializedPerson.Age, "Assert 06");
            Assert.AreEqual(person.Tall, deserializedPerson.Tall, "Assert 07");
            Assert.AreEqual(person.Address.StreetName, deserializedPerson.Address.StreetName, "Assert 08");
            Assert.AreEqual(person.Address.StreetNumber, deserializedPerson.Address.StreetNumber, "Assert 09");
            Assert.AreEqual(person.Address.City, deserializedPerson.Address.City, "Assert 10");
            Assert.AreEqual(person.Address.State, deserializedPerson.Address.State, "Assert 11");
            Assert.AreEqual(person.Address.ZipCode, deserializedPerson.Address.ZipCode, "Assert 12");

            // cleanup
            File.Delete(Path.Combine(filePath, fileName));
            Directory.Delete(filePath);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Serializer")]
        public void Extensions_SerializeDeserialize_ToFile_XmlSerializer()
        {
            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "XmlSerializer.xml";

            // arrange
            Person person = Person.CreateSamplePerson();

            // act
            person.SerializeToFile(filePath, fileName, Serialization.XmlSerializer);
            Person deserializedPerson = Serializer.DeserializeFromFile<Person>(filePath, fileName, Serialization.XmlSerializer);

            string fileContent = new FileHelper().ReadText(filePath, fileName);

            // assert
            Assert.IsNotNull(fileContent, "Assert 01");
            AssertEx.Text.IsNotEmpty(fileContent, "Assert 02");

            Assert.AreEqual(person.Id, deserializedPerson.Id, "Assert 03");
            Assert.AreEqual(person.FirstName, deserializedPerson.FirstName, "Assert 04");
            Assert.AreEqual(person.LastName, deserializedPerson.LastName, "Assert 05");
            Assert.AreEqual(person.Age, deserializedPerson.Age, "Assert 06");
            Assert.AreEqual(person.Tall, deserializedPerson.Tall, "Assert 07");
            Assert.AreEqual(person.Address.StreetName, deserializedPerson.Address.StreetName, "Assert 08");
            Assert.AreEqual(person.Address.StreetNumber, deserializedPerson.Address.StreetNumber, "Assert 09");
            Assert.AreEqual(person.Address.City, deserializedPerson.Address.City, "Assert 10");
            Assert.AreEqual(person.Address.State, deserializedPerson.Address.State, "Assert 11");
            Assert.AreEqual(person.Address.ZipCode, deserializedPerson.Address.ZipCode, "Assert 12");

            // cleanup
            File.Delete(Path.Combine(filePath, fileName));
            Directory.Delete(filePath);
        }
    }
}
