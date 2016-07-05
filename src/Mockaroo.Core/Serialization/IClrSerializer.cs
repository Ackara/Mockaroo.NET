using System;
using System.IO;

namespace Gigobyte.Mockaroo.Serialization
{
    public interface IClrSerializer
    {
        void WriteObject(object obj, Stream stream);

        object ReadObject(Type type, Stream stream);

        object ReadObject<T>(Stream stream);
    }
}