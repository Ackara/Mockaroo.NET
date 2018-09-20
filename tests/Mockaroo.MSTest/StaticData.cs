using Acklann.Mockaroo.Fakes;
using Acklann.Mockaroo.Fields;
using System;

namespace Acklann.Mockaroo
{
    public partial class TestData
    {
        public static Schema CreateSchema()
        {
            return new Schema(new IField[]
            {
                new NumberField()
                {
                    Name = nameof(BasicObject.NumericValue),
                    Min = 10,
                    Max = 100,
                    Decimals = 2
                },
                new WordsField()
                {
                    Name = nameof(BasicObject.StringValue),
                    Min = 3,
                    Max = 5
                },

                new CustomListField()
                {
                    Name = nameof(BasicObject.CharValue),
                    Values = new string[] { "a", "b", "c" }
                },
                new DateField()
                {
                    Name = nameof(BasicObject.DateValue),
                    Min = new DateTime(2000, 01, 01),
                    Max = new DateTime(2010, 01, 01)
                }
            });
        }
    }
}