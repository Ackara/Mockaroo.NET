namespace Tests.Mockaroo
{
    public struct Dev
    {
        public const string Ackara = "ackara.dev@outlook.com";
    }

    public struct Artifact
    {
        public const string
            ApprovalsDir = "Approvals",
            SampleDataDir = "SampleData\\",
            DataXLSX = (SampleDataDir + "data.xlsx"),
            ResponseBody = (SampleDataDir + "response_body_list.csv");
    }

    public struct Data
    {
        public const string
            FieldColumn = "Field",
            ValueColumn = "Value",
            TypeColumn = "Data Type",
            ODBC = "System.Data.Odbc",
            BuiltInDataSheet = "Built-In_Data_Types$",
            CSV = "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            ExcelConnectiongString = "Dsn=Excel Files;dbq=|DataDirectory|\\data.xlsx";

    }
}