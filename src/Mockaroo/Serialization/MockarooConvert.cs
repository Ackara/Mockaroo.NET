using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Acklann.Mockaroo.Serialization
{
    /// <summary>
    /// Contains methods to convert objects to <see cref="Schema"/> and data to objects.
    /// </summary>
    public static partial class MockarooConvert
    {
        /// <summary>
        /// Converts the <paramref name="type"/> to a <see cref="Schema"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="depth">The max-depth the serializer should traverse down the object tree.</param>
        /// <returns></returns>
        public static Schema ToSchema(Type type, int depth)
        {
            return new Schema(ConvertToSchema(type, depth).ToArray());
        }

        /// <summary>
        /// Converts the <paramref name="type"/> to a <see cref="Schema"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depth">The max-depth the serializer should traverse down the object tree.</param>
        /// <returns></returns>
        public static Schema ToSchema<T>(int depth)
        {
            return new Schema(ConvertToSchema(typeof(T), depth).ToArray());
        }

        /// <summary>
        /// Deserialize mockaroo JSON data to the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object[] FromJson(string json, Type type)
        {
            return Deserialize(json, type);
        }

        /// <summary>
        /// Deserialize mockaroo JSON data to the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="stream">The json stream.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object[] FromJson(Stream stream, Type type)
        {
            using (var reader = new StreamReader(stream))
            {
                object[] data = Deserialize(reader.ReadToEnd(), type);
                stream.Position = 0;

                return data;
            }
        }

        /// <summary>
        /// Deserialize mockaroo JSON data to the specified <paramref name="type"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        public static T[] FromJson<T>(string json)
        {
            IEnumerable<object> data = Deserialize(json, typeof(T));
            return data.Cast<T>().ToArray();
        }

        /// <summary>
        /// Deserialize mockaroo JSON data to the specified <paramref name="type"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The json stream.</param>
        /// <returns></returns>
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