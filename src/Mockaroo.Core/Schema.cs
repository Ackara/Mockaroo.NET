using Gigobyte.Mockaroo.Fields;
using Gigobyte.Mockaroo.Fields.Factory;
using Gigobyte.Mockaroo.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Gigobyte.Mockaroo
{
    [DataContract]
    public class Schema : List<IField>, ISerializable
    {
        #region Static Members

        public static Schema Parse(string json)
        {
            return Load(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)));
        }

        public static Schema Load(Stream stream)
        {
            using (stream)
            {
                var schema = new Schema();
                schema.Deserialize(stream);
                return schema;
            }
        }

        public static IEnumerable<IField> GetFields(Type type)
        {
            return new ClrSchemaSerializer().ConvertToSchema(type);
        }

        #endregion Static Members

        public Schema() : this(new FieldFactory(), new IField[0])
        {
        }

        public Schema(Type type) : this(new FieldFactory(), GetFields(type))
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
            Factory = factory;
        }

        protected readonly IFieldFactory<DataType> Factory;

        public void Assign(string fieldName, DataType dataType)
        {
            Assign(fieldName, Factory.CreateInstance(dataType));
        }

        public void Assign(string fieldName, IField field)
        {
            field.Name = fieldName;
            for (int i = 0; i < Count; i++)
                if (this[i].Name == fieldName)
                {
                    this[i] = field;
                }
        }

        public bool Remove(string fieldName)
        {
            for (int i = 0; i < Count; i++)
                if (this[i].Name == fieldName)
                {
                    base.RemoveAt(i);
                    return true;
                }
            return false;
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
    }

    public class Schema<T> : Schema
    {
        #region Static Members

        /// <summary>
        /// Gets a list of <see cref="IField"/> based of the specified type.
        /// </summary>
        /// <returns>A list of <see cref="IField"/> based of the specified type.</returns>
        public static IEnumerable<IField> GetFields()
        {
            return GetFields(typeof(T));
        }

        #endregion Static Members

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema{T}"/> class.
        /// </summary>
        public Schema() : base(typeof(T))
        {
        }

        /// <summary>
        /// Removes the specified <see cref="IField"/> by the property associated with it.
        /// </summary>
        /// <param name="property">The property associated to the <see cref="IField"/>.</param>
        public bool Remove(Expression<Func<T, object>> property)
        {
            Match match = _lambdaPattern.Match(property.ToString().Replace($"{nameof(Extensions.Item)}().", string.Empty));
            if (match.Success)
            {
                return Remove(match.Value.TrimStart('.'));
            }
            else return false;
        }

        /// <summary>
        /// Find and replace the specified <see cref="IField"/> by the property associated with it.
        /// </summary>
        /// <param name="property">The property associated to the <see cref="IField"/>.</param>
        /// <param name="dataType">The Mockaroo data type.</param>
        public void Assign(Expression<Func<T, object>> property, DataType dataType)
        {
            Assign(property, Factory.CreateInstance(dataType));
        }

        /// <summary>
        /// Find and replace the specified <see cref="IField"/> by the property associated with it.
        /// </summary>
        /// <param name="property">The property associated to the <see cref="IField"/>.</param>
        /// <param name="field">The Mockaroo field.</param>
        public void Assign(Expression<Func<T, object>> property, IField field)
        {
            Match match = _lambdaPattern.Match(property.ToString().Replace($"{nameof(Extensions.Item)}().", string.Empty));
            if (match.Success)
            {
                Assign(match.Value.TrimStart('.'), field);
            }
        }

        #region Private Member

        private readonly Regex _lambdaPattern = new Regex(@"(\.[a-zA-Z0-9]+)+");

        #endregion Private Member
    }
}