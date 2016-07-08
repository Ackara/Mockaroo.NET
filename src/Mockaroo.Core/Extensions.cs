using System;

namespace Gigobyte.Mockaroo
{
    /// <summary>
    /// Extension methods for <see cref="Gigobyte.Mockaroo"/> name space.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Checks the value is between the specified minimum and maximum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minInclusive">The minimum inclusive.</param>
        /// <param name="maxInclusive">The maximum inclusive.</param>
        /// <returns></returns>
        public static int Between(this int value, int minInclusive, int maxInclusive)
        {
            if (value >= maxInclusive) return maxInclusive;
            else if (value <= minInclusive) return minInclusive;
            else return value;
        }

        public static bool IsBasicType(this Type type)
        {
            switch (type.Name)
            {
                default:
                    return false;

                case nameof(Boolean):

                case nameof(Char):
                case nameof(String):

                case nameof(Byte):
                case nameof(SByte):

                case nameof(Int16):
                case nameof(UInt16):

                case nameof(Int32):
                case nameof(UInt32):

                case nameof(Int64):
                case nameof(UInt64):

                case nameof(Single):
                case nameof(Double):
                case nameof(Decimal):

                case nameof(TimeSpan):
                case nameof(DateTime):
                    return true;
            }
        }
    }
}