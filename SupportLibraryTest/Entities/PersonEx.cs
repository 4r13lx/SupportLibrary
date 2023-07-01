using System;

namespace SupportLibraryTest.Entities
{
    internal class PersonEx : Person
    {
        public float Weight { get; set; }

        public PersonEx() { }

        public static PersonEx CreateSamplePersonEx()
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
