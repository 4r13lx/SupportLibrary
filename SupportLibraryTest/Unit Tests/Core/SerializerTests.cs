using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Core;
using SupportLibrary.IO;
using SupportLibrary.Testing;
using SupportLibraryTest.Entities;

namespace SupportLibraryTest.Unit_Tests.Core
{
    /// <summary>
    /// Testing of Core namespace classes.
    /// </summary>
    [TestClass]
    public class SerializerTests
    {
        private readonly string basePath = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User);

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Serializer")]
        public void Serializer_SerializeDeserialize_ToBinary()
        {
            try
            {
                // arrange
                Person person = Person.CreateSamplePerson();

                // act
                byte[] serializedPerson = Serializer.SerializeToBinary<Person>(person);
                Person deserializedPerson = Serializer.DeserializeFromBinary<Person>(serializedPerson);

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
                Assert.AreEqual(person.Address.StreetNumber, deserializedPerson.Address.StreetNumber, "Assert 13");
                Assert.AreEqual(person.Address.City, deserializedPerson.Address.City, "Assert 11");
                Assert.AreEqual(person.Address.State, deserializedPerson.Address.State, "Assert 12");
                Assert.AreEqual(person.Address.ZipCode, deserializedPerson.Address.ZipCode, "Assert 13");
            }
            catch (Exception) { throw; }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Serializer")]
        public void Serializer_SerializeDeserialize_ToBinary_FailsOnNullArgs()
        {
            // arrange
            Action action1 = () => Serializer.SerializeToBinary<Person>(null);
            Action action2 = () => Serializer.DeserializeFromBinary<Person>(null);

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Serializer")]
        public void Serializer_SerializeDeserialize_ToXml_DataContract()
        {
            // arrange
            Person person = Person.CreateSamplePerson();

            // act
            string serializedPerson = Serializer.SerializeToXml<Person>(person, XmlSerialization.DataContract);
            Person deserializedPerson = Serializer.DeserializeFromXml<Person>(serializedPerson, XmlSerialization.DataContract);

            // assert
            Assert.IsNotNull(serializedPerson, "Assert 01");
            Assert.IsNotNull(deserializedPerson, "Assert 02");
            AssertEx.Text.IsNotEmpty(serializedPerson, "Assert 03");

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
        public void Serializer_SerializeDeserialize_ToXml_XmlSerializer()
        {
            // arrange
            Person person = Person.CreateSamplePerson();

            // act
            string serializedPerson = Serializer.SerializeToXml<Person>(person, XmlSerialization.XmlSerializer);
            Person deserializedPerson = Serializer.DeserializeFromXml<Person>(serializedPerson, XmlSerialization.XmlSerializer);

            // assert
            Assert.IsNotNull(serializedPerson, "Assert 01");
            Assert.IsNotNull(deserializedPerson, "Assert 02");
            AssertEx.Text.IsNotEmpty(serializedPerson, "Assert 03");

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
        public void Serializer_SerializeDeserialize_ToXml_FailsOnNullArgs()
        {
            // arrange
            Person person = Person.CreateSamplePerson();

            Action action1 = () => Serializer.SerializeToXml<Person>(null, XmlSerialization.DataContract);
            Action action2 = () => Serializer.SerializeToXml<Person>(person, XmlSerialization.None);

            Action action3 = () => Serializer.DeserializeFromXml<Person>(null, XmlSerialization.DataContract);
            Action action4 = () => Serializer.DeserializeFromXml<Person>("xml", XmlSerialization.None);

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentException ex2 = AssertEx.Exceptions.Throws<ArgumentException>(action2);
            ArgumentNullException ex3 = AssertEx.Exceptions.Throws<ArgumentNullException>(action3);
            ArgumentException ex4 = AssertEx.Exceptions.Throws<ArgumentException>(action4);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Serializer")]
        public void Serializer_SerializeDeserialize_ToFile_BinaryFormatter()
        {
            // arrange
            Person person = Person.CreateSamplePerson();

            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "BinaryFormatter.bin";

            // act
            Serializer.SerializeToFile<Person>(person, filePath, fileName, Serialization.BinaryFormatter);
            Person deserializedPerson = Serializer.DeserializeFromFile<Person>(filePath, fileName, Serialization.BinaryFormatter);

            byte[] fileContent = new FileHelper().ReadBinary(filePath, fileName);

            // assert
            Assert.IsNotNull(deserializedPerson, "Assert 01");
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
        public void Serializer_SerializeDeserialize_ToFile_DataContract()
        {
            // arrange
            Person person = Person.CreateSamplePerson();

            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "DataContract.xml";

            // act
            Serializer.SerializeToFile<Person>(person, filePath, fileName, Serialization.DataContract);
            Person deserializedPerson = Serializer.DeserializeFromFile<Person>(filePath, fileName, Serialization.DataContract);

            string fileContent = new FileHelper().ReadText(filePath, fileName);

            // assert
            Assert.IsNotNull(deserializedPerson, "Assert 01");
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
        public void Serializer_SerializeDeserialize_ToFile_XmlSerializer()
        {
            // arrange
            Person person = Person.CreateSamplePerson();

            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "XmlSerializer.xml";

            // act
            Serializer.SerializeToFile<Person>(person, filePath, fileName, Serialization.XmlSerializer);
            Person deserializedPerson = Serializer.DeserializeFromFile<Person>(filePath, fileName, Serialization.XmlSerializer);

            string fileContent = new FileHelper().ReadText(filePath, fileName);

            // assert
            Assert.IsNotNull(deserializedPerson, "Assert 01");
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
        public void Serializer_SerializeDeserialize_ToFile_FailsOnNullArgs()
        {
            // arrange
            Person person = Person.CreateSamplePerson();

            string filePath = Path.Combine(basePath, Guid.NewGuid().ToString());
            string fileName = "DataContract.xml";

            Action action1 = () => Serializer.SerializeToFile<Person>(null, filePath, fileName, Serialization.DataContract);
            Action action2 = () => Serializer.SerializeToFile<Person>(person, null, fileName, Serialization.DataContract);
            Action action3 = () => Serializer.SerializeToFile<Person>(person, filePath, null, Serialization.DataContract);
            Action action4 = () => Serializer.SerializeToFile<Person>(person, filePath, fileName, Serialization.None);

            Action action5 = () => Serializer.DeserializeFromFile<Person>(null, fileName, Serialization.DataContract);
            Action action6 = () => Serializer.DeserializeFromFile<Person>(filePath, null, Serialization.DataContract);
            Action action7 = () => Serializer.DeserializeFromFile<Person>(filePath, fileName, Serialization.None);

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
            ArgumentNullException ex3 = AssertEx.Exceptions.Throws<ArgumentNullException>(action3);
            ArgumentException ex4 = AssertEx.Exceptions.Throws<ArgumentException>(action4);
            ArgumentNullException ex5 = AssertEx.Exceptions.Throws<ArgumentNullException>(action5);
            ArgumentNullException ex6 = AssertEx.Exceptions.Throws<ArgumentNullException>(action6);
            ArgumentException ex7 = AssertEx.Exceptions.Throws<ArgumentException>(action7);
        }
    }
}
