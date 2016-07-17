using System.IO;

namespace Gigobyte.Mockaroo.Serialization
{
    /// <summary>
    /// Defines methods to serialize and deserialize an object.
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// Serializes this instance into an byte array.
        /// </summary>
        /// <returns>Stream.</returns>
        byte[] Serialize();

        /// <summary>
        /// Deserializes the specified bytes into a .NET object.
        /// </summary>
        /// <param name="data">The bytes.</param>
        void Deserialize(byte[] data);

        /// <summary>
        /// Deserializes the specified bytes into a .NET object.
        /// </summary>
        /// <param name="stream">The stream.</param>
        void Deserialize(Stream stream);
    }
}