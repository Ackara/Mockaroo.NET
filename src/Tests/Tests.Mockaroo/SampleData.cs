using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Fields;
using System;
using System.IO;
using System.Linq;

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
                new WordsField()
                {
                    Name = "Title",
                    Min = 3,
                    Max = 5
                },
                new NumberField()
                {
                    Name = "Views",
                    Min = 3,
                    Max = 1000
                },
                new DateField()
                {
                    Name = "PostDate",
                    Min = new DateTime(2000, 01, 01),
                    Max = new DateTime(2010, 01, 01)
                });
        }
    }
}