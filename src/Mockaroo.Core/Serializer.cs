using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Gigobyte.Mockaroo
{
    public class Serializer : IMockarooSerializer
    {
        public object Deserialize(string data, Type returnType)
        {
            return Deserialize(JObject.Parse(data), returnType);
        }

        public object Deserialize(JObject json, Type returnType)
        {
            if (returnType.IsBuiltInType())
            {
                return Convert.ChangeType((json[returnType.Name]), returnType);
            }
            else
            {
                return CreateInstance(json, returnType);
            }
        }

        #region Private Members

        private object CreateInstance(JObject json, Type returnType)
        {
            object obj = Activator.CreateInstance(returnType);

            foreach (var member in returnType.GetRuntimeProperties())
                if (member.CanWrite && member.PropertyType.IsBuiltInType())
                {
                    JToken token = json.GetValue(member.Name);
                    if (token != null)
                    {
                        member.SetValue(obj, Convert.ChangeType(token.ToString(), member.PropertyType));
                    }
                }

            return obj;
        }

        #endregion Private Members
    }
}