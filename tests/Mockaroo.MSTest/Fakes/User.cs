using System;

namespace Acklann.Mockaroo.Fakes
{
    public class User
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime DOB { get; set; }

        public bool Verified { get; set; }

        public int Subscribers { get; set; }

        public override string ToString()
        {
            return $"{{ user: '{Email}', rating: '{Subscribers}', dob: '{DOB.ToShortDateString()}' }}";
        }
    }
}