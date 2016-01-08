using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Gigobyte.Mockaroo
{
    public class Schema
    {
        public Schema()
        {
            Fields = new List<IFieldInfo>();
        }

        public IList<IFieldInfo> Fields { get; set; }

        public string ToJson()
        {
            var json = new System.Text.StringBuilder();
            json.AppendLine("[");
            foreach (var field in Fields)
            {
                json.AppendFormat("\t{{{0}}},\r\n", field.GetJson());
            }
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

        public static IEnumerable<IFieldInfo> GetFields()
        {
            Type t = typeof(T);
            var factory = new FieldFactory();

            foreach (var propertyInfo in t.GetRuntimeProperties())
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {

                }
            throw new System.NotImplementedException();
        }

        public void Remove(Expression<Func<T, object>> propertyExpr)
        {
            throw new System.NotImplementedException();
        }

        public void Set(Expression<Func<T, object>> propertyExpr, DataType dataType)
        {
            throw new System.NotImplementedException();
        }

        public void Set(Expression<Func<T, object>> propertyExpr, IFieldInfo fieldInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}