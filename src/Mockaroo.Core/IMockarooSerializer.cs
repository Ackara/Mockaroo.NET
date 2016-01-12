using System;

namespace Gigobyte.Mockaroo
{
    /// <summary>
    /// Provide methods to deserialize a string in JSON into an instance type.
    /// </summary>
    public interface IMockarooSerializer
    {
        /// <summary>
        /// Deserialize the specified string data into the specified return type.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="returnType">Type of the return.</param>
        /// <returns></returns>
        object Deserialize(string data, Type returnType);

        /// <summary>
        /// Deserialize the specified JSON object into the specified return type.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="returnType">Type of the return.</param>
        /// <returns></returns>
        object Deserialize(Newtonsoft.Json.Linq.JObject json, Type returnType);
    }
}