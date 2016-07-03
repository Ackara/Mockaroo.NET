namespace Tests.Mockaroo
{
    public struct DDT
    {
        public const string CSV = "Microsoft.VisualStudio.TestTools.DataSource.CSV";

        public struct Connection
        {
            public const string DataTypes = ("|DataDirectory|\\" + TestFile.DataTypeMap);
        }

        public struct Column
        {
            public const string Field = "Field";
            public const string Value = "Value";
            public const string Type = "Data Type";
        }
    }
}