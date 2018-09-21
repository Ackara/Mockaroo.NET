﻿using System;
using System.Collections;
using System.Linq;

namespace Acklann.Mockaroo.Fields
{
    /// <summary>
	/// Provides a method to create an <see cref="IField"/> instance.
	/// </summary>
    public static partial class FieldFactory
    {
        /// <summary>
        /// Creates a new <see cref="IField"/> instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IField CreateInstance(Type type)
        {
            if (type.IsEnum)
            {
                var values = Enum.GetValues(type).Cast<int>().Select(x => (x.ToString()));
                return new CustomListField() { Values = values.ToArray() };
            }
            else if (type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type))
            {
                return new JSONArrayField();
            }
            else switch (type.Name)
                {
                    case nameof(Byte):
                        return new NumberField(type.Name);

                    case nameof(SByte):
                        return new NumberField() { Max = sbyte.MaxValue };

                    case nameof(Int32):
                    case nameof(Int64):
                        return new NumberField();

                    case nameof(UInt32):
                    case nameof(UInt64):
                        return new NumberField();

                    case nameof(Int16):
                        return new NumberField() { Max = short.MaxValue };

                    case nameof(UInt16):
                        return new NumberField() { Max = ushort.MaxValue };

                    case nameof(Single):
                    case nameof(Double):
                    case nameof(Decimal):
                        return new NumberField() { Decimals = 2 };

                    case nameof(Char):
                        return new CustomListField() { Sequence = Arrangement.Random, Values = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" } };

                    case nameof(Boolean):
                        return new BooleanField();

                    case nameof(String):
                        return new WordsField();

                    case nameof(DateTime):
                        return new DateField();

                    case nameof(TimeSpan):
                        return new TimeField();

                    default:
                        throw new ArgumentException(Exceptions.ExceptionMessage.CannotMapToDataType(type));
                }
        }
    }
}