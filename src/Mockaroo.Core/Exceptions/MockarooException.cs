using System;

namespace Gigobyte.Mockaroo.Exceptions
{
    public class MockarooException : System.Net.WebException
    {
        public MockarooException(string message) : base(message)
        {
        }
    }
}