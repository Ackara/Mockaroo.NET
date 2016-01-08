using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Gigobyte.Mockaroo
{
    public class Serializer
    {
        public T Deserialize<T>(string data)
        {
            object obj = Activator.CreateInstance(typeof(T));
            IEnumerable<PropertyInfo> properties = typeof(T).GetRuntimeProperties();
            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            foreach (var member in properties)
                if (json.ContainsKey(member.Name) && member.CanWrite)
                {
                    member.SetValue(obj, Convert.ChangeType(json[member.Name], member.PropertyType));
                }

            return (T)obj;

            //TypeInfo t = typeof(T).GetTypeInfo();
            //if (typeof(T) == typeof(string))
            //{
            //    return (T)Convert.ChangeType(data, typeof(T));
            //}
            //else if (typeof(T) == typeof(DateTime))
            //{
            //    return (T)Convert.ChangeType(DateTime.Parse(data), typeof(T));
            //}
            //else if (t.IsClass)
            //{
            //    return Deserialize<T>(data);
            //}
            //else
            //{
            //    return (T)Convert.ChangeType(data, typeof(T));
            //}
        }

        #region Private Members

        private T foo<T>(string data)
        {
            object obj = Activator.CreateInstance(typeof(T));
            IEnumerable<PropertyInfo> properties = typeof(T).GetRuntimeProperties();
            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            foreach (var member in properties)
                if (json.ContainsKey(member.Name) && member.CanWrite)
                {
                    member.SetValue(obj, Convert.ChangeType(json[member.Name], member.PropertyType));
                }

            return (T)obj;
        }

        #endregion Private Members
    }


}