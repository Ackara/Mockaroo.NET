using System;

namespace Acklann.Mockaroo.Exceptions
{
    /// <summary>
    /// The exception that is thrown while accessing the https://mockaroo.com RESTful API.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class MockarooException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockarooException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public MockarooException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockarooException"/> class.
        /// </summary>
        /// <param name="message">The text of the error message.</param>
        /// <param name="innerException">A nested exception.</param>
        public MockarooException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}