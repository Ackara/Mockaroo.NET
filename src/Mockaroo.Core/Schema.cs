using Gigobyte.Mockaroo.Fields;
using Gigobyte.Mockaroo.Fields.Factory;
using Gigobyte.Mockaroo.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Gigobyte.Mockaroo
{
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

        public Schema() : this(new FieldFactory(), new IField[0])
        {
        }

        public Schema(params IField[] fields) : this(new FieldFactory(), fields)
        {
        }

        public Schema(IEnumerable<IField> fields) : this(null, fields)
        {
        }

        public Schema(IFieldFactory<DataType> factory, IEnumerable<IField> fields) : base(fields)
        {
            _factory = factory;
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

        public void Deserialize(byte[] data)
        {
            Deserialize(new MemoryStream(data));
        }

        public void Deserialize(Stream stream)
        {
            using (var reader = new JsonTextReader(new StreamReader(stream)))
            {
                foreach (var json in JArray.Load(reader))
                {
                    var type = Type.GetType($"{nameof(Gigobyte)}.{nameof(Gigobyte.Mockaroo)}.{nameof(Fields)}.{json["type"].Value<string>().ToDataType()}Field");
                    IField field = (IField)JsonConvert.DeserializeObject(json.ToString(), type);
                    Add(field);
                }
            }
        }

        public string ToJson()
        {
            using (var reader = new StreamReader(Serialize()))
            {
                return reader.ReadToEnd();
            }
        }

        #region Private Members

        private readonly IFieldFactory<DataType> _factory;
        private readonly ISchemaSerializer _serializer;

        #endregion Private Members
    }
}