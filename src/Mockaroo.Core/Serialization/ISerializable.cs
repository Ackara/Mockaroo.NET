using System.IO;

namespace Gigobyte.Mockaroo.Serialization
{
    public interface ISerializable
    {
        Stream Serialize();

        void Deserialize(byte[] bytes);

        void Deserialize(Stream stream);
    }
}