using Gigobyte.Mockaroo.Fields;
using Gigobyte.Mockaroo.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Gigobyte.Mockaroo
{
    [JsonArray]
    public class Schema : List<IField>, ISerializable
    {
        #region Static Members

        public static Schema Load(Stream stream)
        {
            var schema = new Schema();
            schema.Deserialize(stream);
            return schema;
        }

        public static Schema LoadFrom(Type type)
        {
            throw new System.NotImplementedException();
        }

        #endregion Static Members

        public Schema() : base()
        {
        }

        public Schema(int capacity) : base(capacity)
        {
        }

        public Schema(params IField[] fields) : base(fields)
        {
        }

        public Schema(IEnumerable<IField> collection) : base(collection)
        {
        }

        public Stream Serialize()
        {
            var memory = new MemoryStream();
            var writer = new StreamWriter(memory);
            var separator = string.Empty;
            writer.Write('[');
            for (int i = 0; i < Count; i++)
            {
                separator = (i < (Count - 1) ? "," : string.Empty);
                writer.Write(string.Format("{0}{1}", base[i].ToJson(), separator));
            }
            writer.Write(']');
            writer.Flush();
            memory.Seek(0, SeekOrigin.Begin);

            return memory;
        }

        public void Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }

        public string ToJson()
        {
            using (var reader = new StreamReader(Serialize()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}