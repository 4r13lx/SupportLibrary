using System;
using SupportLibrary.Core;

namespace SupportLibraryTest.Entities
{
    [Serializable]
    public class Person : ICloneableExtended
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        public float Tall { get; set; }
        public Address Address { get; set; }

        public Person() { }

        public static Person CreateSamplePerson()
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

        public object ShallowClone()
        {
            return this.MemberwiseClone();
        }

        public object DeepClone()
        {
            return this.MemberwiseClone();
        }
    }
}
