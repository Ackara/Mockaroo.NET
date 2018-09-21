using Acklann.Mockaroo.Fields;
using Acklann.Mockaroo.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Acklann.Mockaroo
{
    /// <summary>
    /// Represents a Mockaroo schema.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{IField}"/>
    public class Schema : List<IField>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        public Schema()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="depth">The max-depth the serializer should traverse down the object tree.</param>
        public Schema(Type type, int depth = DEFAULT_DEPTH)
        {
            AddRange(MockarooConvert.ConvertToSchema(type, 2));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public Schema(params IField[] fields)
        {
            base.AddRange(fields);
        }

        internal const int DEFAULT_DEPTH = 2;

        /// <summary>
        /// Gets the <see cref="IField"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="IField"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <returns>A <see cref="IField"/></returns>
        /// <exception cref="KeyNotFoundException">Occurs when the <paramref name="name"/> do not match <see cref="IField"/> in the collection.</exception>
        public IField this[string name]
        {
            get
            {
                foreach (IField field in this)
                    if (field.Name == name)
                    {
                        return field;
                    }

                throw new KeyNotFoundException($"A field with the name '{name}' do not exist.");
            }
        }

        /// <summary>
        /// Replace the <see cref="IField"/> object within this instance with a <see cref="IField"/>
        /// that is associated with the specified <see cref="DataType"/>.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="dataType">Type of the data.</param>
        public void Reassign(string fieldName, DataType dataType)
        {
            Reassign(fieldName, FieldFactory.CreateInstance(dataType));
        }

        /// <summary>
        /// Replace the <see cref="IField"/> object within this instance with specified <see
        /// cref="IField"/>. specified instance.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="field">The field.</param>
        public void Reassign(string fieldName, IField field)
        {
            field.Name = fieldName;
            for (int i = 0; i < Count; i++)
                if (this[i].Name == fieldName)
                {
                    this[i] = field;
                    break;
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
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var json = new StringBuilder();

            json.AppendLine("[");
            for (int i = 0; i < Count; i++)
            {
                json.Append("  ");
                json.Append(this[i]);
                if (i < Count - 1) json.AppendLine(",");
            }
            json.AppendLine();
            json.AppendLine("]");

            return json.ToString();
        }
    }

    /// <summary>
    /// Represents a Mockaroo schema.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Acklann.Mockaroo.Schema"/>
    public class Schema<T> : Schema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Schema{T}"/> class.
        /// </summary>
        public Schema(int depth = DEFAULT_DEPTH) : base(typeof(T), depth)
        { }

        /// <summary>
        /// Removes the specified <see cref="IField"/> by the property associated with it.
        /// </summary>
        /// <param name="property">The property associated to the <see cref="IField"/>.</param>
        public bool Remove(Expression<Func<T, object>> property)
        {
            return Remove(MockarooConvert.ToFieldName(property));
        }

        /// <summary>
        /// Replace the <see cref="IField"/> object within this instance with a <see cref="IField"/>
        /// that is associated with the specified <see cref="DataType"/>.
        /// </summary>
        /// <param name="property">The property associated to the <see cref="IField"/>.</param>
        /// <param name="dataType">The Mockaroo data type.</param>
        public void Reassign(Expression<Func<T, object>> property, DataType dataType)
        {
            Reassign(MockarooConvert.ToFieldName(property), FieldFactory.CreateInstance(dataType));
        }

        /// <summary>
        /// Replace the <see cref="IField"/> object within this instance with specified <see cref="IField"/>.
        /// </summary>
        /// <param name="property">The property associated to the <see cref="IField"/>.</param>
        /// <param name="field">The Mockaroo field.</param>
        public void Reassign(Expression<Func<T, object>> property, IField field)
        {
            Reassign(MockarooConvert.ToFieldName(property), field);
        }
    }
}