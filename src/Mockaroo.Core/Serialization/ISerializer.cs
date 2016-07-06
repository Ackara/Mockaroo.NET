using System.IO;

namespace Gigobyte.Mockaroo.Serialization
{
    public interface ISerializer
    {
        T ReadObject<T>(Stream stream);

        object ReadObject(Stream stream);

        void WriteObject(object obj, Stream stream);
    }
}