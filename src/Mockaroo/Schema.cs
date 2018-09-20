using Acklann.Mockaroo.Fields;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Acklann.Mockaroo
{
    /// <summary>
    /// Represents a Mockaroo schema.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{IField}"/>
    /// <seealso cref="ISerializable"/>
    public class Schema : List<IField>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        public Schema()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schema"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Schema(Type type, int depth = DEFAULT_DEPTH)
        {
            AddRange(Serialization.MockarooConvert.ConvertToSchema(type, 2));
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
        /// Replace the <see cref="IField"/> object within this instance with a <see cref="IField"/>
        /// that is associated with the specified <see cref="DataType"/>.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="dataType">Type of the data.</param>
        public void Assign(string fieldName, DataType dataType)
        {
            Assign(fieldName, FieldFactory.CreateInstance(dataType));
        }

        /// <summary>
        /// Replace the <see cref="IField"/> object within this instance with specified <see
        /// cref="IField"/>. specified instance.
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
                json.Append(base[i].ToJson());
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
            Match match = _lambdaPattern.Match(property.ToString().Replace($"{nameof(Extensions.Item)}().", string.Empty));
            if (match.Success)
            {
                return Remove(match.Value.TrimStart('.'));
            }
            else return false;
        }

        /// <summary>
        /// Replace the <see cref="IField"/> object within this instance with a <see cref="IField"/>
        /// that is associated with the specified <see cref="DataType"/>.
        /// </summary>
        /// <param name="property">The property associated to the <see cref="IField"/>.</param>
        /// <param name="dataType">The Mockaroo data type.</param>
        public void Assign(Expression<Func<T, object>> property, DataType dataType)
        {
            Assign(property, FieldFactory.CreateInstance(dataType));
        }

        /// <summary>
        /// Replace the <see cref="IField"/> object within this instance with specified <see cref="IField"/>.
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