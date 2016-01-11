using System;
using System.Reflection;

namespace Gigobyte.Mockaroo
{
    public static partial class Extensions
    {
        internal static bool IsBuiltInType(this Type type)
        {
            return (typeof(string) == type || type.GetTypeInfo().IsPrimitive) || type == typeof(DateTime) || type == typeof(decimal);
        }

        internal static void RemoveLastComma(this System.Text.StringBuilder builder)
        {
            int comma = builder.ToString().LastIndexOf(',');
            builder.Remove(comma, 1);
        }
    }
}