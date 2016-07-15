using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gigobyte.Mockaroo.Serialization
{
    public class ClrMockarooSerializer : IMockarooSerializer
    {
        public object[] ReadObject(Type type, byte[] data)
        {
            throw new NotImplementedException();
        }

        public T[] ReadObject<T>(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
