using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Acklann.Mockaroo.Serialization
{
    public static partial class MockarooConvert
    {
        public static Schema ToSchema(Type type, int depth)
        {
            return new Schema(ConvertToSchema(type, depth).ToArray());
        }

        public static Schema ToSchema<T>(int depth)
        {
            return new Schema(ConvertToSchema(typeof(T), depth).ToArray());
        }

        public static object[] FromJson(string json, Type type)
        {
            return Deserialize(json, type);
        }

        public static object[] FromJson(Stream stream, Type type)
        {
            using (var reader = new StreamReader(stream))
            {
                object[] data = Deserialize(reader.ReadToEnd(), type);
                stream.Position = 0;

                return data;
            }
        }

        public static T[] FromJson<T>(string json)
        {
            IEnumerable<object> data = Deserialize(json, typeof(T));
            return data.Cast<T>().ToArray();
        }

        public static T[] FromJson<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                IEnumerable<object> data = Deserialize(reader.ReadToEnd(), typeof(T));
                stream.Position = 0;

                return data.Cast<T>().ToArray();
            }
        }
    }
}