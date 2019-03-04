using Acklann.Mockaroo.Fields;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Acklann.Mockaroo.Serialization
{
    /// Type  :::>  <see cref="Schema"/>
    /// ======================================================================
    partial class MockarooConvert
    {
        internal const string Item = "_item_";
        private static readonly Regex _pattern = new Regex(@"\.\w+");

        internal static IEnumerable<IField> ConvertToSchema(Type template, int maxDepth)
        {
            if (maxDepth < 1) maxDepth = 1;
            else if (maxDepth > sbyte.MaxValue) maxDepth = sbyte.MaxValue;

            var fields = new LinkedList<IField>();
            PopulateFields(fields, template, string.Empty, 0, maxDepth);

            if (fields.Count > 0)
                return fields;
            else
                throw new ArgumentOutOfRangeException($"Cannot convert {template.Name} to {nameof(Schema)} because {template.Name} do not have any public properties or fields.");
        }

        internal static string ToFieldName<T>(Expression<Func<T, object>> property)
        {
            string tPath = null;
            foreach (Match match in _pattern.Matches(property.ToString()))
                foreach (Group group in match.Groups)
                {
                    tPath = string.Concat(tPath, group.Value == ".get_Item" ? $".{Item}" : group.Value);
                }
            if (string.IsNullOrEmpty(tPath)) throw new ArgumentException($"Expression '{property.ToString()}' must refer to a field or property.");

            if (property.Body is MemberExpression member)
                if (member.Type.IsArray)
                {
                    tPath = $"{tPath}.{Item}";
                }

            return tPath.TrimStart('.');
        }

        private static void PopulateFields(ICollection<IField> schema, Type template, string parentName, int currentDepth, int maxDepth)
        {
            if (currentDepth >= maxDepth) return;
            string concat(params string[] args) => string.Join(".", args).TrimStart('.');

            foreach (MemberInfo member in GetPublicFieldsAndPropertiesFrom(template))
            {
                string temp;
                IField field;
                Type valueType = GetValueType(member);
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"{concat(parentName, member.Name)}: <{valueType.Name}>");
#endif
                switch (GetKindOfType(valueType, out Type collectionElementType))
                {
                    case KindOfType.Primitive:
                        field = FieldFactory.CreateInstance(valueType);
                        field.Name = concat(parentName, member.Name);
                        schema.Add(field);
                        break;

                    case KindOfType.CollectionOfPrimitives:
                        field = FieldFactory.CreateInstance(valueType);
                        field.Name = concat(parentName, member.Name);
                        schema.Add(field);

                        field = FieldFactory.CreateInstance(collectionElementType);
                        field.Name = concat(parentName, member.Name, Item);
                        schema.Add(field);
                        break;

                    case KindOfType.Object:
                        temp = concat(parentName, member.Name);
                        PopulateFields(schema, valueType, temp, (currentDepth + 1), maxDepth);
                        break;

                    case KindOfType.CollectionOfObjects:
                        field = FieldFactory.CreateInstance(valueType);
                        field.Name = concat(parentName, member.Name);
                        schema.Add(field);

                        temp = concat(parentName, member.Name, Item);
                        PopulateFields(schema, collectionElementType, temp, (currentDepth + 1), maxDepth);
                        break;
                }
            }
        }

        private static KindOfType GetKindOfType(Type valueType, out Type collectionElementType)
        {
            KindOfType kind;
            bool isaCollection = false;
            collectionElementType = null;

            if (valueType.IsEnum) return KindOfType.Primitive;
            else if (valueType != typeof(string) && (valueType.IsArray || typeof(IEnumerable).IsAssignableFrom(valueType)))
            {
                isaCollection = true;
                collectionElementType = (valueType.IsArray ? valueType.GetElementType() : valueType.GenericTypeArguments[0]);
                valueType = collectionElementType;
            }

            switch (valueType.Name)
            {
                default:
                    kind = (isaCollection ? KindOfType.CollectionOfObjects : KindOfType.Object);
                    break;

                case nameof(Boolean):

                case nameof(Byte):
                case nameof(SByte):

                case nameof(Int16):
                case nameof(UInt16):

                case nameof(Int32):
                case nameof(UInt32):

                case nameof(Int64):
                case nameof(UInt64):

                case nameof(Single):
                case nameof(Double):
                case nameof(Decimal):

                case nameof(Char):
                case nameof(String):

                case nameof(TimeSpan):
                case nameof(DateTime):
                    kind = (isaCollection ? KindOfType.CollectionOfPrimitives : KindOfType.Primitive);
                    break;
            }

            return kind;
        }

        private static ICollection<MemberInfo> GetPublicFieldsAndPropertiesFrom(Type type)
        {
            if (type.IsClass)
            {
                bool doNotHaveParameterLessConstructor = type.GetConstructors().Where(x => x.GetParameters().Length == 0).Count() == 0;
                if (doNotHaveParameterLessConstructor) throw new ArgumentException($"Cannot convert {type.Name} to {nameof(Schema)} because {type.Name} do not have a constructor with zero parameters.");
            }

            var list = new LinkedList<MemberInfo>();

            foreach (PropertyInfo member in type.GetRuntimeProperties().Where(x => x.CanWrite))
                list.AddLast(member);

            foreach (FieldInfo member in type.GetRuntimeFields().Where(x => x.IsPublic && x.IsInitOnly == false))
                list.AddLast(member);

            return list;
        }

        private static Type GetValueType(MemberInfo member)
        {
            if (member is PropertyInfo prop) return prop.PropertyType;
            else if (member is FieldInfo field) return field.FieldType;
            else return null;
        }
    }
}