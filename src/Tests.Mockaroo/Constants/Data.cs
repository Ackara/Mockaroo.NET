using System;
using System.IO;
using System.Linq;

namespace Tests.Mockaroo.Constants
{
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