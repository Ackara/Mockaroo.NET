using System;
using System.Runtime.CompilerServices;

namespace Tests.Mockaroo
{
    public class Person
    {
        public static Person GetSample([CallerMemberName]string name = null)
        {
            return new Person()
            {
                Age = 21,
                FullName = name,
                NetWorth = 123.4M,
                Dob = new DateTime(2012, 12, 12),
            };
        }

        public string FullName { get; set; }

        public int Age { get; set; }

        public double Height { get; set; }

        public decimal NetWorth { get; set; }

        public DateTime Dob { get; set; }

        public string ToJson()
        {
            string
                age = $"\"{nameof(Age)}\": \"{Age}\"",
                height = $"\"{nameof(Height)}\": \"{Height}\"",
                net = $"\"{nameof(NetWorth)}\": \"{NetWorth}\"",
                name = $"\"{nameof(FullName)}\": \"{FullName}\"",
                dob = $"\"{nameof(Dob)}\": \"{Dob.ToString("yyyy-MM-dd HH:mm:ss")}\"";

            return $"{{{name}, {age}, {height}, {net}, {dob}}}";
        }
    }
}