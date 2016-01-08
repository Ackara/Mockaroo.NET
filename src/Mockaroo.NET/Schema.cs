using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gigobyte.Mockaroo
{
    public class Schema<T>
    {
        public Schema() : this(new IFieldInfo[0])
        {
        }

        public Schema(params IFieldInfo[] fields)
        {
            Fields = new List<IFieldInfo>(fields);
        }

        public IList<IFieldInfo> Fields { get; set; }

        public void Set(Expression<Func<T, object>> selector, Annotation.FieldInfoAttribute to)
        {
            
        }

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
}