using System;
using System.Collections.Generic;

namespace Gigobyte.Mockaroo
{
    public class Schema
    {
        #region Static Members

        public Schema Create(Type type)
        {
            throw new System.NotImplementedException();
        }

        public Schema Create<T>()
        {
            return Create(typeof(T));
        }

        #endregion Static Members

        public Schema() : this(new IFieldInfo[0])
        {
        }

        public Schema(params IFieldInfo[] fields)
        {
            Fields = new List<IFieldInfo>(fields);
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
}