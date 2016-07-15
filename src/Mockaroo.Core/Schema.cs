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
    /// <summary>
    /// Represents a Mockaroo schema.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{IField}" />
    /// <seealso cref="ISerializable" />
    [DataContract]
    public class Schema : List<IField>, ISerializable
    {
        #region Static Members

        /// <summary>
        /// Convert the string representation of a <see cref="Schema"/> object into its <see
        /// cref="Schema"/> equivalent.
        /// </summary>
        /// <param name="json">The json string.</param>
        /// <returns>A <see cref="Schema"/> instance.</returns>
        public static Schema Parse(string json)
        {
            return Load(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)));
        }

        /// <summary>
        /// Creates a new <see cref="Schema"/> instance from the specified <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A <see cref="Schema"/> instance.</returns>
        public static Schema Load(Stream stream)
        {
            using (stream)
            {
                var schema = new Schema();
                schema.Deserialize(stream);
                return schema;
            }
        }

        /// <summary>
        /// Gets all list of <see cref="IField"/> objects that represents the specified <see
        /// cref="Type"/> properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A list of <see cref="IField"/> based of the specified type.</returns>
        public static IEnumerable<IField> GetFields(Type type)
        {
            return new ClrSchemaSerializer().ConvertToSchema(type);
        }

        #endregion Static Members

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        public Schema() : this(new FieldFactory(), new IField[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Schema(Type type) : this(new FieldFactory(), GetFields(type))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public Schema(params IField[] fields) : this(new FieldFactory(), fields)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public Schema(IEnumerable<IField> fields) : this(null, fields)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="fields">The fields.</param>
        public Schema(IFieldFactory<DataType> factory, IEnumerable<IField> fields) : base(fields)
        {
            Factory = factory;
        }

        /// <summary>
        /// An instance of a factory object used to create <see cref="IField"/> objects.
        /// </summary>
        protected readonly IFieldFactory<DataType> Factory;

        /// <summary>
        /// Find and replace the first <see cref="IField"/> object within this instance with a <see
        /// cref="IField"/> that corresponds with specified <see cref="DataType"/>.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="dataType">Type of the data.</param>
        public void Assign(string fieldName, DataType dataType)
        {
            Assign(fieldName, Factory.CreateInstance(dataType));
        }

        /// <summary>
        /// Find and replace the first <see cref="IField"/> object within this instance with the
        /// specified instance.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="field">The field.</param>
        public void Assign(string fieldName, IField field)
        {
            field.Name = fieldName;
            for (int i = 0; i < Count; i++)
                if (this[i].Name == fieldName)
                {
                    this[i] = field;
                }
        }

        /// <summary>
        /// Removes a <see cref="IField"/> from this instance with the specified name.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns><c>true</c> if item was found, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns>Stream.</returns>
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

        /// <summary>
        /// Deserializes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Deserialize(byte[] data)
        {
            Deserialize(new MemoryStream(data));
        }

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
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

        /// <summary>
        /// Convert this instance to its JSON representation.
        /// </summary>
        /// <returns>The JSON representation of the instance.</returns>
        public string ToJson()
        {
            using (var reader = new StreamReader(Serialize()))
            {
                return reader.ReadToEnd();
            }
        }
    }

    /// <summary>
    /// Represents a Mockaroo schema.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Gigobyte.Mockaroo.Schema"/>
    public class Schema<T> : Schema
    {
        #region Static Members

        /// <summary>
        /// Gets all list of <see cref="IField"/> objects that represents the specified type properties.
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