using System;
using System.Runtime.CompilerServices;

namespace Tests.Mockaroo
{
    public class FakeObject
    {
        public int NoSetter { get { return 2016; } }

        public byte ByteValue { get; set; }

        public sbyte SByteValue { get; set; }

        public short Int16Value { get; set; }

        public ushort UInt16Value { get; set; }

        public int Int32Value { get; set; }

        public uint UInt32Value { get; set; }

        public long Int64Value { get; set; }

        public ulong UInt64Value { get; set; }

        public float SingleValue { get; set; }

        public double DoubleValue { get; set; }

        public decimal DecimalValue { get; set; }

        public char CharValue { get; set; }

        public string StringValue { get; set; }

        public DateTime DateValue { get; set; }

        public System.IO.Stream Stream { get; set; }

        public static FakeObject GetSample([CallerMemberName]string name = null)
        {
            return new FakeObject()
            {
                Int32Value = 21,
                StringValue = name,
                DecimalValue = 123.4M,
                DateValue = new DateTime(2012, 12, 12),
            };
        }

        public string ToJson()
        {
            string
                pair7 = $"\"{nameof(Stream)}\": \"{null}\"",
                pair6 = $"\"{nameof(NoSetter)}\": \"{NoSetter}\"",
                pair2 = $"\"{nameof(Int32Value)}\": \"{Int32Value}\"",
                pair3 = $"\"{nameof(DoubleValue)}\": \"{DoubleValue}\"",
                pair1 = $"\"{nameof(StringValue)}\": \"{StringValue}\"",
                pair4 = $"\"{nameof(DecimalValue)}\": \"{DecimalValue}\"",
                pair5 = $"\"{nameof(DateValue)}\": \"{DateValue.ToString("yyyy-MM-dd HH:mm:ss")}\"";

            return $"{{{pair1}, {pair2}, {pair3}, {pair4}, {pair5}, {pair6}, {pair7}}}";
        }
    }
}