using System;
using System.Reflection;

namespace Gigobyte.Mockaroo
{
    public class TypeLoader
    {
        #region Static Members

        public static T LoadData<T>(string data)
        {
            throw new NotImplementedException();
        }

        #endregion Static Members

        public T CreateInstance<T>(string data)
        {
            TypeInfo t = typeof(T).GetTypeInfo();
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (t.IsClass)
            {
                return foo<T>(data);
            }
            else
            {
                return (T)Convert.ChangeType(data, typeof(T));
            }
        }

        #region Private Members

        private T foo<T>(string data)
        {
            throw new NotImplementedException();
        }

        #endregion Private Members
    }
}