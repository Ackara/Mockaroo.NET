using System;
using System.IO;
using System.Linq;

namespace Tests.Mockaroo
{
    public class Test
    {
        public struct Trait
        {
            public const string Integration = nameof(IntegrationTest);
        }

        public struct Property
        {
            public const string Records = "records";
        }

        public struct File
        {
            public const string ApiKey = "apikey.txt";
            public const string SchemaJson = "basic_schema.json";
            public const string BasicResponse = "basic_server_response.json";
        }

        public class Data
        {
            public const string DirectoryName = "SampleData";

            public const string CsvProvider = "Microsoft.VisualStudio.TestTools.DataSource.CSV";

            public static FileInfo GetFile(string filename)
            {
                string searchPattern = "*" + Path.GetExtension(filename);
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                return new DirectoryInfo(baseDirectory).GetFiles(searchPattern, SearchOption.AllDirectories)
                    .First(x => x.Name == filename);
            }

            public struct Connection
            {
                public const string DataTypes = ("|DataDirectory|\\");
            }

            public struct Column
            {
                public const string Field = "Field";
                public const string Value = "Value";
                public const string Type = "Data Type";
            }
        }
    }
}