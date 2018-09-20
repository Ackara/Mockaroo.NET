using System;

namespace Acklann.Mockaroo.Fakes
{
    public struct Name
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class Person
    {
        public Name Name { get; set; }

        public int Age { get; set; }

        public DateTime DOB { get; set; }

        public string JobTitle { get; set; }
    }
}