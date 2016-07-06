using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gigobyte.Mockaroo.Serialization
{
    public class SchemaSerializer : ISerializer
    {
        public object ReadObject(Type type, Stream stream)
        {
            throw new NotImplementedException();
        }

        public T ReadObject<T>(Stream stream)
        {
            return (T)ReadObject(typeof(T), stream);
        }

        public void WriteObject(object obj, Stream stream)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            stream.Write(Encoding.UTF8.GetBytes(json), 0, json.Length);
            stream.Position = 0;
        }
    }
}
