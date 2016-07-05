using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gigobyte.Mockaroo.Serialization
{
    public class ClrSerializer : IClrSerializer
    {
        public object ReadObject(Type type, Stream stream)
        {
            throw new NotImplementedException();
        }

        public object ReadObject<T>(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void WriteObject(object obj, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
