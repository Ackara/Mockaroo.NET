using System.IO;

namespace Gigobyte.Mockaroo.Serialization
{
    public interface ISerializable
    {
        Stream Serialize();

        void Deserialize(Stream stream);
    }
}