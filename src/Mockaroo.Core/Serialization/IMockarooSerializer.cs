using System;

namespace Gigobyte.Mockaroo.Serialization
{
    /// <summary>
    /// Defines methods to deserialize the data exported form https://mockaroo.com into an array of
    /// .NET objects.
    /// </summary>
    public interface IMockarooSerializer
    {
        /// <summary>
        /// Deserialize the specified bytes into the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="data">The data.</param>
        /// <returns>System.Object[].</returns>
        object[] ReadObject(Type type, byte[] data);

        /// <summary>
        /// Deserialize the specified bytes into the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns>T[].</returns>
        T[] ReadObject<T>(byte[] data);
    }
}