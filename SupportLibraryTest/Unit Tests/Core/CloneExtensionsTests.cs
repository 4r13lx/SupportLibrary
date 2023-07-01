using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Core;
using SupportLibrary.Testing;
using SupportLibraryTest.Entities;

namespace SupportLibraryTest.Core
{
    /// <summary>
    /// Testing of Core namespace classes.
    /// </summary>
    [TestClass]
    public class CloneExtensionsTests
    {
        private readonly string basePath = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User);

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Clone")]
        public void Extensions_Instance_CloneShallowly()
        {
            // arrange
            Person person = Person.CreateSamplePerson();

            // act
            Person shallowClone = person.Clone();
            Person deepClone = person.Clone(true);

            // assert
            Assert.AreNotEqual(person, shallowClone, "Assert 01");
            Assert.AreNotEqual(person, deepClone, "Assert 02");

            Assert.AreEqual(person.Id, shallowClone.Id, "Assert 03");
            Assert.AreEqual(person.FirstName, shallowClone.FirstName, "Assert 04");
            Assert.AreEqual(person.LastName, shallowClone.LastName, "Assert 05");
            Assert.AreEqual(person.Age, shallowClone.Age, "Assert 06");
            Assert.AreEqual(person.Tall, shallowClone.Tall, "Assert 07");
            Assert.AreEqual(person.Address.StreetName, shallowClone.Address.StreetName, "Assert 08");
            Assert.AreEqual(person.Address.StreetNumber, shallowClone.Address.StreetNumber, "Assert 09");
            Assert.AreEqual(person.Address.City, shallowClone.Address.City, "Assert 10");
            Assert.AreEqual(person.Address.State, shallowClone.Address.State, "Assert 11");
            Assert.AreEqual(person.Address.ZipCode, shallowClone.Address.ZipCode, "Assert 12");

            Assert.AreEqual(person.Id, deepClone.Id, "Assert 13");
            Assert.AreEqual(person.FirstName, deepClone.FirstName, "Assert 14");
            Assert.AreEqual(person.LastName, deepClone.LastName, "Assert 15");
            Assert.AreEqual(person.Age, deepClone.Age, "Assert 16");
            Assert.AreEqual(person.Tall, deepClone.Tall, "Assert 17");
            Assert.AreEqual(person.Address.StreetName, deepClone.Address.StreetName, "Assert 18");
            Assert.AreEqual(person.Address.StreetNumber, deepClone.Address.StreetNumber, "Assert 19");
            Assert.AreEqual(person.Address.City, deepClone.Address.City, "Assert 20");
            Assert.AreEqual(person.Address.State, deepClone.Address.State, "Assert 21");
            Assert.AreEqual(person.Address.ZipCode, deepClone.Address.ZipCode, "Assert 22");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Clone")]
        public void Extensions_Instance_FailsOnNullArgument()
        {
            // arrange
            Person person = null;

            Action action1 = () => person.Clone();
            Action action2 = () => person.Clone(true);

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Clone")]
        public void Extensions_Collection_CloneDeeply()
        {
            // arrange
            Person person1 = Person.CreateSamplePerson();
            Person person2 = Person.CreateSamplePerson();

            List<Person> persons = new List<Person>() { person1, person2 };

            // act
            List<Person> shallowClones = persons.Clone<Person>().ToList();
            List<Person> deepClones = persons.Clone<Person>(true).ToList();

            // assert
            for (int i = 0; i < persons.Count; i++)
            {
                Assert.AreNotEqual(persons[i], shallowClones[i], "Assert 01");
                Assert.AreNotEqual(persons[i], deepClones[i], "Assert 02");

                Assert.AreEqual(persons[i].Id, shallowClones[i].Id, "Assert 03");
                Assert.AreEqual(persons[i].FirstName, shallowClones[i].FirstName, "Assert 04");
                Assert.AreEqual(persons[i].LastName, shallowClones[i].LastName, "Assert 05");
                Assert.AreEqual(persons[i].Age, shallowClones[i].Age, "Assert 06");
                Assert.AreEqual(persons[i].Tall, shallowClones[i].Tall, "Assert 07");
                Assert.AreEqual(persons[i].Address.StreetName, shallowClones[i].Address.StreetName, "Assert 08");
                Assert.AreEqual(persons[i].Address.StreetNumber, shallowClones[i].Address.StreetNumber, "Assert 09");
                Assert.AreEqual(persons[i].Address.City, shallowClones[i].Address.City, "Assert 10");
                Assert.AreEqual(persons[i].Address.State, shallowClones[i].Address.State, "Assert 11");
                Assert.AreEqual(persons[i].Address.ZipCode, shallowClones[i].Address.ZipCode, "Assert 12");

                Assert.AreEqual(persons[i].Id, deepClones[i].Id, "Assert 13");
                Assert.AreEqual(persons[i].FirstName, deepClones[i].FirstName, "Assert 14");
                Assert.AreEqual(persons[i].LastName, deepClones[i].LastName, "Assert 15");
                Assert.AreEqual(persons[i].Age, deepClones[i].Age, "Assert 16");
                Assert.AreEqual(persons[i].Tall, deepClones[i].Tall, "Assert 17");
                Assert.AreEqual(persons[i].Address.StreetName, deepClones[i].Address.StreetName, "Assert 18");
                Assert.AreEqual(persons[i].Address.StreetNumber, deepClones[i].Address.StreetNumber, "Assert 19");
                Assert.AreEqual(persons[i].Address.City, deepClones[i].Address.City, "Assert 20");
                Assert.AreEqual(persons[i].Address.State, deepClones[i].Address.State, "Assert 21");
                Assert.AreEqual(persons[i].Address.ZipCode, deepClones[i].Address.ZipCode, "Assert 22");
            }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Clone")]
        public void Extensions_Collection_FailsOnNullArgument()
        {
            // arrange
            List<Person> persons = null;

            Action action1 = () => persons.Clone<Person>();
            Action action2 = () => persons.Clone<Person>(true);

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
        }
    }
}
