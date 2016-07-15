using System.IO;

namespace Gigobyte.Mockaroo.Serialization
{
    public interface ISerializable
    {
        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns>Stream.</returns>
        Stream Serialize();

        void Deserialize(byte[] bytes);

        void Deserialize(Stream stream);
    }
}