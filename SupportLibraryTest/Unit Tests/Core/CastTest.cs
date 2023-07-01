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
    public class CastTest
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Cast")]
        public void Cast_To_Source()
        {
            // arrange
            Person inputPerson = BuildPerson();
            PersonEx inputPersonEx = BuildPersonExtended();

            // act
            Person outputPerson = Cast.To<Person>(inputPersonEx, true);
            PersonEx outputPersonEx = Cast.To<PersonEx>(inputPerson, false);

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
        public void Cast_To_SourceDest()
        {
            // arrange
            Person inputPerson = BuildPerson();
            PersonEx inputPersonEx = BuildPersonExtended();

            // act
            Person outputPerson = Cast.To(inputPersonEx, new Person(), true);
            PersonEx outputPersonEx = Cast.To(inputPerson, new PersonEx(), false);

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
        public void Cast_To_TestDateNull()
        {
            // arrange
            DateAndTime inputDate = new DateAndTime();
            inputDate.DateTest = null;

            // act
            DateAndTime inputDateDest = Cast.To(inputDate, new DateAndTime(), true);

            // assert
            Assert.AreEqual(null, inputDateDest.DateTest, "Assert DateTest");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Cast")]
        public void Cast_To_TestDate()
        {
            // arrange
            DateAndTime inputDate = new DateAndTime();
            inputDate.DateTest = DateTime.Now;

            // act
            DateAndTime inputDateDest = Cast.To(inputDate, new DateAndTime(), true);

            // assert
            Assert.AreEqual(inputDate.DateTest, inputDateDest.DateTest, "Assert DateTest");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Cast")]
        public void Cast_To_FailsOnNullArgument()
        {
            // arrange
            Action action1 = () => Cast.To<Person>(null);
            Action action2 = () => Cast.To(null, new Person());
            Action action3 = () => Cast.To<Person>(null, null);

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
            ArgumentNullException ex3 = AssertEx.Exceptions.Throws<ArgumentNullException>(action3);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Core.Cast")]
        public void Cast_CollectionTo_Source()
        {
            // arrange
            List<Person> inputPersons = new List<Person>();
            inputPersons.Add(BuildPerson());
            inputPersons.Add(BuildPerson());

            // act
            List<PersonEx> outputPersonsMainLevel = Cast.CollectionTo<Person, PersonEx>(inputPersons, false);
            List<PersonEx> outputPersonsAllLevel = Cast.CollectionTo<Person, PersonEx>(inputPersons, true);

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
        public void Cast_CollectionTo_SourceDest()
        {
            // arrange
            List<Person> inputPersons = new List<Person>();
            inputPersons.Add(BuildPerson());
            inputPersons.Add(BuildPerson());

            // act
            List<PersonEx> outputPersonsMainLevel = Cast.CollectionTo(inputPersons, new PersonEx(), false);
            List<PersonEx> outputPersonsAllLevel = Cast.CollectionTo(inputPersons, new PersonEx(), true);

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

        private Person BuildPerson()
        {
            Person person = new Person();
            person.Id = new Random().Next();
            person.FirstName = "Pablo";
            person.LastName = "Gutierrez";
            person.Age = 20;
            person.Tall = 1.75F;
            person.Address = new Address() { StreetName = "Saraza", StreetNumber = 1234, City = "CABA", State = "CABA", ZipCode = "1000" };

            return person;
        }

        private PersonEx BuildPersonExtended()
        {
            PersonEx personExtended = new PersonEx();
            personExtended.Id = 1;
            personExtended.FirstName = "Pablo";
            personExtended.LastName = "Gutierrez";
            personExtended.Age = 20;
            personExtended.Tall = 1.75F;
            personExtended.Address = new Address() { StreetName = "Saraza", StreetNumber = 1234, City = "CABA", State = "CABA", ZipCode = "1000" };

            personExtended.Weight = 80.50F;

            return personExtended;
        }
    }
}
