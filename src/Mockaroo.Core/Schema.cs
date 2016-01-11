using Gigobyte.Mockaroo.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Gigobyte.Mockaroo
{
    public class Schema
    {
        #region Static Members

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

        public Schema() : this(new IField[0])
        {
        }

        public Schema(IEnumerable<IField> mockarooFields) : this(mockarooFields.ToArray())
        {
        }

        public Schema(params IField[] mockarooFields)
        {
            Fields = new List<IField>(mockarooFields);
        }

        public IList<IField> Fields { get; set; }

        public void Remove(string propertyName)
        {
            for (int i = 0; i < Fields.Count; i++)
                if (Fields[i].Name == propertyName)
                {
                    Fields.RemoveAt(i);
                }
        }

        public void Replace(string fieldName, DataType dataType)
        {
            Replace(fieldName, new FieldFactory().Create(dataType));
        }

        public void Replace(string fieldName, IField fieldInfo)
        {
            fieldInfo.Name = fieldName;
            for (int i = 0; i < Fields.Count; i++)
                if (Fields[i].Name == fieldName)
                {
                    Fields[i] = fieldInfo;
                }
        }

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

    public class Schema<T> : Schema
    {
        public Schema() : base()
        {
            foreach (var field in GetFields())
            {
                Fields.Add(field);
            }
        }

        public static IEnumerable<IField> GetFields()
        {
            return Schema.GetFields(typeof(T));
        }

        public void Remove(Expression<Func<T, object>> property)
        {
            Match match = _lambdaPattern.Match(property.ToString());
            if (match.Success)
            {
                Remove(match.Groups["property"].Value);
            }
        }

        public void Replace(Expression<Func<T, object>> property, DataType dataType)
        {
            Match match = _lambdaPattern.Match(property.ToString());
            if (match.Success)
            {
                Replace(match.Groups["property"].Value, new FieldFactory().Create(dataType));
            }
        }

        public void Replace(Expression<Func<T, object>> property, IField fieldInfo)
        {
            Match match = _lambdaPattern.Match(property.ToString());
            if (match.Success)
            {
                Replace(match.Groups["property"].Value, fieldInfo);
            }
        }

        #region Private Member

        private Regex _lambdaPattern = new Regex(@"(?i)[_a-z0-9]+\.(?<property>[_a-z0-9]+)");

        #endregion Private Member
    }
}