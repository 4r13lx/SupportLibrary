using System;

namespace SupportLibraryTest.Entities
{
    [Serializable]
    public class Address
    {
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address() { }
    }
}
