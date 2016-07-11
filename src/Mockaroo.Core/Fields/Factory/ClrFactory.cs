using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Gigobyte.Mockaroo.Fields.Factory
{
    public class ClrFactory : IFieldFactory<Type>
    {
        public IField CreateInstance(Type type)
        {
            TypeInfo typeInfo = type.GetTypeInfo();
            if (typeInfo.IsEnum)
            {
                var values = Enum.GetValues(type).Cast<int>().Select(x => (x.ToString()));
                return new CustomListField() { Values = values.ToArray() };
            }
            else if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(typeInfo))
            {
                return new JSONArrayField() { Name = type.Name };
            }
            else switch (typeInfo.Name)
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
                        return new CustomListField() { Name = type.Name, Sequence = Arrangement.Random, Values = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" } };

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
    }
}