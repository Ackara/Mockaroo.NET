using System.IO;

namespace Acklann.Mockaroo.Serialization
{
    /// <summary>
    /// Defines methods to serialize and deserialize an object.
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// Serializes this instance into an byte array.
        /// </summary>
        /// <returns>A System.Byte[].</returns>
        byte[] ToBytes();

        /// <summary>
        /// Deserializes the specified bytes into a instance.
        /// </summary>
        /// <param name="data">The bytes.</param>
        void ReadBytes(byte[] data);

        /// <summary>
        /// Deserializes the specified bytes into a instance.
        /// </summary>
        /// <param name="stream">The stream.</param>
        void ReadBytes(Stream stream);
    }
}