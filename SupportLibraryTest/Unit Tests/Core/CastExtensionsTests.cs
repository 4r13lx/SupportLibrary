using System;
using System.Collections.Generic;
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
    public class CaseExtensionsTests
    {
        private readonly string basePath = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User);

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Cast")]
        public void Extensions_CastTo()
        {
            // arrange
            Person inputPerson = Person.CreateSamplePerson();
            PersonEx inputPersonEx = PersonEx.CreateSamplePersonEx();

            // act
            Person outputPerson = inputPersonEx.CastTo<Person>(true);
            PersonEx outputPersonEx = inputPerson.CastTo<PersonEx>(false);

            // assert
            Assert.AreEqual(inputPersonEx.Id, outputPerson.Id, "Assert 01");
            Assert.AreEqual(inputPersonEx.FirstName, outputPerson.FirstName, "Assert 02");
            Assert.AreEqual(inputPersonEx.LastName, outputPerson.LastName, "Assert 03");
            Assert.AreEqual(inputPersonEx.Age, outputPerson.Age, "Assert 04");
            Assert.AreEqual(inputPersonEx.Tall, outputPerson.Tall, "Assert 05");
            Assert.AreEqual(inputPersonEx.Address.StreetName, outputPerson.Address.StreetName, "Assert 06");
            Assert.AreEqual(inputPersonEx.Address.StreetNumber, outputPerson.Address.StreetNumber, "Assert 07");
            Assert.AreEqual(inputPersonEx.Address.City, outputPerson.Address.City, "Assert 08");
            Assert.AreEqual(inputPersonEx.Address.State, outputPerson.Address.State, "Assert 09");
            Assert.AreEqual(inputPersonEx.Address.ZipCode, outputPerson.Address.ZipCode, "Assert 10");

            Assert.AreEqual(inputPerson.Id, outputPersonEx.Id, "Assert 01");
            Assert.AreEqual(inputPerson.FirstName, outputPersonEx.FirstName, "Assert 02");
            Assert.AreEqual(inputPerson.LastName, outputPersonEx.LastName, "Assert 03");
            Assert.AreEqual(inputPerson.Age, outputPersonEx.Age, "Assert 04");
            Assert.AreEqual(inputPerson.Tall, outputPersonEx.Tall, "Assert 05");
            Assert.AreEqual(default(float), outputPersonEx.Weight, "Assert 06");
            Assert.AreEqual(null, outputPersonEx.Address, "Assert 07");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Cast")]
        public void Extensions_CastTo_Dest()
        {
            // arrange
            Person inputPerson = Person.CreateSamplePerson();
            PersonEx inputPersonEx = PersonEx.CreateSamplePersonEx();

            // act
            Person outputPerson = inputPersonEx.CastTo(new Person(), true);
            PersonEx outputPersonEx = inputPerson.CastTo(new PersonEx(), false);

            // assert
            Assert.AreEqual(inputPersonEx.Id, outputPerson.Id, "Assert 01");
            Assert.AreEqual(inputPersonEx.FirstName, outputPerson.FirstName, "Assert 02");
            Assert.AreEqual(inputPersonEx.LastName, outputPerson.LastName, "Assert 03");
            Assert.AreEqual(inputPersonEx.Age, outputPerson.Age, "Assert 04");
            Assert.AreEqual(inputPersonEx.Tall, outputPerson.Tall, "Assert 05");
            Assert.AreEqual(inputPersonEx.Address.StreetName, outputPerson.Address.StreetName, "Assert 06");
            Assert.AreEqual(inputPersonEx.Address.StreetNumber, outputPerson.Address.StreetNumber, "Assert 07");
            Assert.AreEqual(inputPersonEx.Address.City, outputPerson.Address.City, "Assert 08");
            Assert.AreEqual(inputPersonEx.Address.State, outputPerson.Address.State, "Assert 09");
            Assert.AreEqual(inputPersonEx.Address.ZipCode, outputPerson.Address.ZipCode, "Assert 10");

            Assert.AreEqual(inputPerson.Id, outputPersonEx.Id, "Assert 01");
            Assert.AreEqual(inputPerson.FirstName, outputPersonEx.FirstName, "Assert 02");
            Assert.AreEqual(inputPerson.LastName, outputPersonEx.LastName, "Assert 03");
            Assert.AreEqual(inputPerson.Age, outputPersonEx.Age, "Assert 04");
            Assert.AreEqual(inputPerson.Tall, outputPersonEx.Tall, "Assert 05");
            Assert.AreEqual(default(float), outputPersonEx.Weight, "Assert 06");
            Assert.AreEqual(null, outputPersonEx.Address, "Assert 07");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Cast")]
        public void Extensions_CastTo_FailsOnNullArgument()
        {
            // arrange
            Person person = null;
            Action action1 = () => person.CastTo<Person>();
            Action action2 = () => person.CastTo(new Person());

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Cast")]
        public void Extensions_CastCollectionTo()
        {
            // arrange
            List<Person> inputPersons = new List<Person>();
            inputPersons.Add(Person.CreateSamplePerson());
            inputPersons.Add(Person.CreateSamplePerson());
            inputPersons[1].Address.StreetName += ".";

            // act
            List<PersonEx> outputPersonsMainLevel = inputPersons.CastCollectionTo<Person, PersonEx>(false);
            List<PersonEx> outputPersonsAllLevel = inputPersons.CastCollectionTo<Person, PersonEx>(true);

            // assert
            for (int i = 0; i < outputPersonsMainLevel.Count; i++)
            {
                Assert.AreEqual(inputPersons[i].Id, outputPersonsMainLevel[i].Id, "Assert Id");
                Assert.AreEqual(inputPersons[i].FirstName, outputPersonsMainLevel[i].FirstName, "Assert FirstName");
                Assert.AreEqual(inputPersons[i].LastName, outputPersonsMainLevel[i].LastName, "Assert LastName");
                Assert.AreEqual(inputPersons[i].Age, outputPersonsMainLevel[i].Age, "Assert Age");
                Assert.AreEqual(inputPersons[i].Tall, outputPersonsMainLevel[i].Tall, "Assert Tall");
                Assert.AreEqual(default(float), outputPersonsMainLevel[i].Weight, "Assert Weight");
                Assert.AreEqual(null, outputPersonsMainLevel[i].Address, "Assert Address");
            }

            for (int i = 0; i < outputPersonsAllLevel.Count; i++)
            {
                Assert.AreEqual(inputPersons[i].Id, outputPersonsAllLevel[i].Id, "Assert Id");
                Assert.AreEqual(inputPersons[i].FirstName, outputPersonsAllLevel[i].FirstName, "Assert FirstName");
                Assert.AreEqual(inputPersons[i].LastName, outputPersonsAllLevel[i].LastName, "Assert LastName");
                Assert.AreEqual(inputPersons[i].Age, outputPersonsAllLevel[i].Age, "Assert Age");
                Assert.AreEqual(inputPersons[i].Tall, outputPersonsAllLevel[i].Tall, "Assert Tall");
                Assert.AreEqual(default(float), outputPersonsAllLevel[i].Weight, "Assert Weight");
                Assert.AreEqual(inputPersons[i].Address.StreetName, outputPersonsAllLevel[i].Address.StreetName, "Assert StreetName");
                Assert.AreEqual(inputPersons[i].Address.StreetNumber, outputPersonsAllLevel[i].Address.StreetNumber, "Assert StreetNumber");
                Assert.AreEqual(inputPersons[i].Address.City, outputPersonsAllLevel[i].Address.City, "Assert City");
                Assert.AreEqual(inputPersons[i].Address.State, outputPersonsAllLevel[i].Address.State, "Assert State");
                Assert.AreEqual(inputPersons[i].Address.ZipCode, outputPersonsAllLevel[i].Address.ZipCode, "Assert ZipCode");
            }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Cast")]
        public void Extensions_CastCollectionTo_Dest()
        {
            // arrange
            List<Person> inputPersons = new List<Person>();
            inputPersons.Add(Person.CreateSamplePerson());
            inputPersons.Add(Person.CreateSamplePerson());

            // act
            List<PersonEx> outputPersonsMainLevel = inputPersons.CastCollectionTo(new PersonEx(), false);
            List<PersonEx> outputPersonsAllLevel = inputPersons.CastCollectionTo(new PersonEx(), true);

            // assert
            for (int i = 0; i < outputPersonsMainLevel.Count; i++)
            {
                Assert.AreEqual(inputPersons[i].Id, outputPersonsMainLevel[i].Id, "Assert Id");
                Assert.AreEqual(inputPersons[i].FirstName, outputPersonsMainLevel[i].FirstName, "Assert FirstName");
                Assert.AreEqual(inputPersons[i].LastName, outputPersonsMainLevel[i].LastName, "Assert LastName");
                Assert.AreEqual(inputPersons[i].Age, outputPersonsMainLevel[i].Age, "Assert Age");
                Assert.AreEqual(inputPersons[i].Tall, outputPersonsMainLevel[i].Tall, "Assert Tall");
                Assert.AreEqual(default(float), outputPersonsMainLevel[i].Weight, "Assert Weight");
                Assert.AreEqual(null, outputPersonsMainLevel[i].Address, "Assert Address");
            }

            for (int i = 0; i < outputPersonsAllLevel.Count; i++)
            {
                Assert.AreEqual(inputPersons[i].Id, outputPersonsAllLevel[i].Id, "Assert Id");
                Assert.AreEqual(inputPersons[i].FirstName, outputPersonsAllLevel[i].FirstName, "Assert FirstName");
                Assert.AreEqual(inputPersons[i].LastName, outputPersonsAllLevel[i].LastName, "Assert LastName");
                Assert.AreEqual(inputPersons[i].Age, outputPersonsAllLevel[i].Age, "Assert Age");
                Assert.AreEqual(inputPersons[i].Tall, outputPersonsAllLevel[i].Tall, "Assert Tall");
                Assert.AreEqual(default(float), outputPersonsAllLevel[i].Weight, "Assert Weight");
                Assert.AreEqual(inputPersons[i].Address.StreetName, outputPersonsAllLevel[i].Address.StreetName, "Assert StreetName");
                Assert.AreEqual(inputPersons[i].Address.StreetNumber, outputPersonsAllLevel[i].Address.StreetNumber, "Assert StreetNumber");
                Assert.AreEqual(inputPersons[i].Address.City, outputPersonsAllLevel[i].Address.City, "Assert City");
                Assert.AreEqual(inputPersons[i].Address.State, outputPersonsAllLevel[i].Address.State, "Assert State");
                Assert.AreEqual(inputPersons[i].Address.ZipCode, outputPersonsAllLevel[i].Address.ZipCode, "Assert ZipCode");
            }
        }
    }
}
