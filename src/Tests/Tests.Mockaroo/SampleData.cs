using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Fields;
using System;
using System.IO;
using System.Linq;
using Tests.Mockaroo.Fakes;

namespace Tests.Mockaroo
{
    public class SampleData
    {
        public const string DirectoryName = "SampleData";

        public static FileInfo GetFile(string filename)
        {
            string searchPattern = "*" + Path.GetExtension(filename);
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            return new DirectoryInfo(baseDirectory).GetFiles(searchPattern, SearchOption.AllDirectories)
                .First(x => x.Name == filename);
        }

        public static string GetFileContent(string filename)
        {
            return File.ReadAllText(GetFile(filename).FullName);
        }

        public static Schema CreateSchema()
        {
            return new Schema(
                 new NumberField()
                 {
                     Name = nameof(SimpleObject.IntegerValue),
                     Min = 3,
                     Max = 1000
                 },
                new NumberField()
                {
                    Name = nameof(SimpleObject.DecimalValue),
                    Min = 10,
                    Max = 100
                },
                new WordsField()
                {
                    Name = nameof(SimpleObject.StringValue),
                    Min = 3,
                    Max = 5
                },

                new CustomListField()
                {
                    Name = nameof(SimpleObject.CharValue),
                    Values = new string[] { "a", "b", "c" }
                },
                new DateField()
                {
                    Name = nameof(SimpleObject.DateValue),
                    Min = new DateTime(2000, 01, 01),
                    Max = new DateTime(2010, 01, 01)
                });
        }

        public static Schema CreateSchemaWithDefaults()
        {
            return new Schema(
                new NumberField() { Name = nameof(SimpleObject.IntegerValue) },
                new NumberField() { Name = nameof(SimpleObject.DecimalValue) },
                new CustomListField() { Name = nameof(SimpleObject.CharValue) },
                new WordsField() { Name = nameof(SimpleObject.StringValue) },
                new DateField() { Name = nameof(SimpleObject.DateValue) }
                );
        }
    }
}