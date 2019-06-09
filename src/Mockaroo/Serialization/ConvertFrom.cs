using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Acklann.Mockaroo.Serialization
{
    /// Mockaroo JSON-Data  :::>  Objects
    /// ======================================================================
    partial class MockarooConvert
    {
        internal static object[] Deserialize(string json, Type template)
        {
            int index = 0;
            var dataset = JArray.Parse(json);
            var results = new object[dataset.Count];

            foreach (JObject record in dataset)
            {
                object instance = null;
                CreateNew(ref instance, template, record, string.Empty);
                results[index++] = instance;
            }
            return results;
        }

        private static void CreateNew(ref object instance, Type template, JObject data, string parent)
        {
            if ((data?.HasValues ?? false) == false) return;
            if (instance == null) instance = Activator.CreateInstance(template);
            string jpath(params string[] args) => string.Join(".", args).TrimStart('.');

            foreach (MemberInfo member in GetPublicFieldsAndPropertiesFrom(template))
            {
                string temp;
                Type valueType = GetValueType(member);
                JToken value = data.SelectToken(member.Name);
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"{jpath(parent, member.Name)}: <{valueType.Name}>");
#endif
                if (value == null) continue;
                switch (GetKindOfType(valueType, out Type elementType))
                {
                    case KindOfType.Primitive:
                        SetValue(member, instance, value.ToObject(valueType));
                        break;

                    case KindOfType.CollectionOfPrimitives:
                        object[] valueList = (from v in value.Children()
                                              select v[Item].ToObject(elementType)).ToArray();

                        if (valueType.IsArray)
                            SetValue(member, instance, ToArray(valueList, elementType));
                        else
                            SetValue(member, instance, Activator.CreateInstance(valueType, args: new object[] { ToEnumerable(valueList, elementType) }));
                        break;

                    case KindOfType.Object:
                        temp = jpath(parent, member.Name);
                        object childInstance = null;
                        CreateNew(ref childInstance, valueType, (JObject)data.SelectToken(member.Name), temp);
                        SetValue(member, instance, childInstance);
                        break;

                    case KindOfType.CollectionOfObjects:
                        var itemList = new Stack();
                        temp = jpath(parent, member.Name, elementType.Name);

                        foreach (JObject record in ((JArray)value))
                        {
                            object item = null;
                            CreateNew(ref item, elementType, (JObject)record.SelectToken(Item), temp);
                            itemList.Push(item);
                        }

                        if (valueType.IsArray)
                            SetValue(member, instance, ToArray(itemList.ToArray(), elementType));
                        else
                            SetValue(member, instance, Activator.CreateInstance(valueType, args: new object[] { ToEnumerable(itemList.ToArray(), elementType) }));
                        break;
                }
            }
        }

        private static void SetValue(MemberInfo member, object instance, object value)
        {
            if (member is PropertyInfo prop)
                prop.SetValue(instance, value);
            else if (member is FieldInfo field)
                field.SetValue(instance, value);
        }

        private static object ToArray(object[] values, Type elementType)
        {
            Array array = Array.CreateInstance(elementType, values.Length);
            for (int i = 0; i < values.Length; i++)
                array.SetValue(Convert.ChangeType(values[i], elementType), i);

            return array;
        }

        private static object ToEnumerable(object[] values, Type elementType)
        {
            if (typeof(IKVSubstitue).IsAssignableFrom(elementType))
            {
                Type k = elementType.GenericTypeArguments[0];
                Type v = elementType.GenericTypeArguments[1];
                Type kv = typeof(KeyValuePair<,>).MakeGenericType(k, v);
                IDictionary dic = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(k, v));

                IKVSubstitue item;
                for (int i = 0; i < values.Length; i++)
                {
                    item = (IKVSubstitue)values[i];
                    dic[item.Key] = item.Value;
                }
                return dic;
            }
            else return typeof(Enumerable).GetMethod(nameof(Enumerable.Cast)).MakeGenericMethod(elementType)
                .Invoke(null, new object[] { values });
        }
    }
}