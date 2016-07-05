using Gigobyte.Mockaroo.Fields;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Gigobyte.Mockaroo
{
    /// <summary>
    /// Represents a Mockaroo Schema.
    /// </summary>
    public class Schema
    {
        #region Static Members

        //public static Schema Load(Stream stream)
        //{
        //    using (var reader = new StreamReader(stream))
        //    {
        //        return Parse(reader.ReadToEnd());
        //    }
        //}

        //public static Schema Parse(string json)
        //{
        //    return JsonConvert.DeserializeObject<Schema>(json);
        //}

        /// <summary>
        /// Gets a list of <see cref="IField"/> based of the specified type.
        /// </summary>
        /// <param name="type">The type to generate the list of <see cref="IField"/> from.</param>
        /// <returns>A list of <see cref="IField"/> based of the specified type.</returns>
        public static IEnumerable<IField> GetFields(Type type)
        {
            if (type.IsBuiltInType())
            {
                yield return GetMockarooFieldMappedToDoNetType(type);
            }
            else
            {
                IField field;
                foreach (var propertyInfo in type.GetRuntimeProperties())
                    if (propertyInfo.CanRead && propertyInfo.CanWrite)
                    {
                        field = GetMockarooFieldMappedToDoNetType(propertyInfo.PropertyType);
                        if (field != null)
                        {
                            field.Name = propertyInfo.Name;
                            yield return field;
                        }
                    }
            }
        }

        private static IField GetMockarooFieldMappedToDoNetType(Type type)
        {
            switch (type.Name)
            {
                case nameof(Byte):
                    return new NumberField() { Name = type.Name, Min = byte.MinValue, Max = byte.MaxValue };

                case nameof(SByte):
                    return new NumberField() { Name = type.Name, Min = sbyte.MinValue, Max = sbyte.MaxValue };

                case nameof(Int32):
                case nameof(Int64):
                    return new NumberField() { Name = type.Name, Min = int.MinValue, Max = int.MaxValue };

                case nameof(UInt32):
                case nameof(UInt64):
                    return new NumberField() { Name = type.Name, Min = 0, Max = int.MaxValue };

                case nameof(Int16):
                    return new NumberField() { Name = type.Name, Min = short.MinValue, Max = short.MaxValue };

                case nameof(UInt16):
                    return new NumberField() { Name = type.Name, Min = ushort.MinValue, Max = ushort.MaxValue };

                case nameof(Single):
                case nameof(Double):
                case nameof(Decimal):
                    return new NumberField() { Name = type.Name, Min = int.MinValue, Max = int.MaxValue, Decimals = 2 };

                case nameof(Char):
                    return new CustomListField() { Name = type.Name, Sequence = SelectionStyle.Random, Values = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" } };

                case nameof(Boolean):
                    return new BooleanField() { Name = type.Name };

                case nameof(String):
                    return new WordsField() { Name = type.Name };

                case nameof(DateTime):
                    return new DateField() { Name = type.Name };

                default:
                    return null;
            }
        }

        #endregion Static Members

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        public Schema() : this(new IField[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        /// <param name="type">The target type.</param>
        public Schema(Type type) : this(Schema.GetFields(type)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        /// <param name="mockarooFields">The Mockaroo fields.</param>
        public Schema(IEnumerable<IField> mockarooFields) : this(mockarooFields.ToArray())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        /// <param name="mockarooFields">The Mockaroo fields.</param>
        public Schema(params IField[] mockarooFields)
        {
            Fields = new List<IField>(mockarooFields);
        }

        /// <summary>
        /// Gets or sets the Mockaroo fields.
        /// </summary>
        /// <value>The fields.</value>
        public IList<IField> Fields { get; set; }

        /// <summary>
        /// Removes the specified <see cref="IField"/> by it's name.
        /// </summary>
        /// <param name="fieldName">Name of the <see cref="IField"/>.</param>
        public void Remove(string fieldName)
        {
            for (int i = 0; i < Fields.Count; i++)
                if (Fields[i].Name == fieldName)
                {
                    Fields.RemoveAt(i);
                }
        }

        /// <summary>
        /// Find and replace the specified <see cref="IField"/> by it's name.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="dataType">The Mockaroo data type.</param>
        public void Assign(string fieldName, DataType dataType)
        {
            Assign(fieldName, new FieldFactory().Create(dataType));
        }

        /// <summary>
        /// Find and replace the specified <see cref="IField"/> by it's name.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldInfo">The Mockaroo field.</param>
        public void Assign(string fieldName, IField fieldInfo)
        {
            fieldInfo.Name = fieldName;
            for (int i = 0; i < Fields.Count; i++)
                if (Fields[i].Name == fieldName)
                {
                    Fields[i] = fieldInfo;
                }
        }

        /// <summary>
        /// Converts the value of this instance to its JSON representation.
        /// </summary>
        /// <returns>This instance JSON representation.</returns>
        public string ToJson()
        {
            var json = new System.Text.StringBuilder();
            json.AppendLine("[");
            foreach (var field in Fields)
            {
                json.AppendFormat("{0},\r\n", field.ToJson());
            }

            json.RemoveLastComma();
            json.AppendLine("]");
            return json.ToString();
        }
    }

    /// <summary>
    /// Represents a Mockaroo Schema.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Schema<T> : Schema
    {
        #region Static Members

        /// <summary>
        /// Gets a list of <see cref="IField"/> based of the specified type.
        /// </summary>
        /// <returns>A list of <see cref="IField"/> based of the specified type.</returns>
        public static IEnumerable<IField> GetFields()
        {
            return Schema.GetFields(typeof(T));
        }

        #endregion Static Members

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema{T}"/> class.
        /// </summary>
        public Schema() : base()
        {
            foreach (var field in GetFields())
            {
                Fields.Add(field);
            }
        }

        /// <summary>
        /// Removes the specified <see cref="IField"/> by the property associated with it.
        /// </summary>
        /// <param name="property">The property associated to the <see cref="IField"/>.</param>
        public void Remove(Expression<Func<T, object>> property)
        {
            Match match = _lambdaPattern.Match(property.ToString());
            if (match.Success)
            {
                Remove(match.Groups["property"].Value);
            }
        }

        /// <summary>
        /// Find and replace the specified <see cref="IField"/> by the property associated with it.
        /// </summary>
        /// <param name="property">The property associated to the <see cref="IField"/>.</param>
        /// <param name="dataType">The Mockaroo data type.</param>
        public void Assign(Expression<Func<T, object>> property, DataType dataType)
        {
            Match match = _lambdaPattern.Match(property.ToString());
            if (match.Success)
            {
                Assign(match.Groups["property"].Value, new FieldFactory().Create(dataType));
            }
        }

        /// <summary>
        /// Find and replace the specified <see cref="IField"/> by the property associated with it.
        /// </summary>
        /// <param name="property">The property associated to the <see cref="IField"/>.</param>
        /// <param name="fieldInfo">The Mockaroo field.</param>
        public void Assign(Expression<Func<T, object>> property, IField fieldInfo)
        {
            Match match = _lambdaPattern.Match(property.ToString());
            if (match.Success)
            {
                Assign(match.Groups["property"].Value, fieldInfo);
            }
        }

        #region Private Member

        private Regex _lambdaPattern = new Regex(@"(?i)[_a-z0-9]+\.(?<property>[_a-z0-9]+)");

        #endregion Private Member
    }
}