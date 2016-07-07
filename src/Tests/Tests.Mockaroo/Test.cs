namespace Tests.Mockaroo
{
    public class Test
    {
        public struct Trait
        {
            public const string Integration = nameof(IntegrationTest);
        }

        public struct File
        {
            public const string ApiKey = "apikey.txt";
            public const string SchemaJson = "basic_schema.json";
            public const string BasicResponse = "basic_server_response.json";
        }

        public struct Data
        {
            public const string CsvProvider = "Microsoft.VisualStudio.TestTools.DataSource.CSV";

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