using System;
using System.IO;
using System.Linq;

namespace Acklann.Mockaroo
{
	public static partial class SampleFile
	{
		public const string FOLDER_NAME = "SampleData";

		public const string ProjectDirectory = @"C:\Users\Ackeem\Projects\Ackara\Mockaroo.NET\tests\Mockaroo.MSTest\";

		public static string DirectoryName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FOLDER_NAME);
        
		public static FileInfo GetFile(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            string searchPattern = $"*{Path.GetExtension(fileName)}";

            string appDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FOLDER_NAME);
            return new DirectoryInfo(appDirectory).EnumerateFiles(searchPattern, SearchOption.AllDirectories)
                .First(x => x.Name.Equals(fileName, StringComparison.CurrentCultureIgnoreCase));
        }

        public static string GetContents(this string filePath)
		{
			return File.ReadAllText(filePath);
		}

		public static string GetContents(this FileInfo file)
		{
			return File.ReadAllText(file.FullName);
		}

		public static FileInfo GetBasic_Schema() => GetFile(@"basic_schema.json");

		public static FileInfo GetBasic_Server_Response() => GetFile(@"basic_server_response.json");

		public static FileInfo GetComplex_Server_Response() => GetFile(@"complex_server_response.json");

		public static FileInfo GetData() => GetFile(@"data.csv");

		public static FileInfo GetMockaroo_Type_List() => GetFile(@"mockaroo_type_list.csv");

		public static FileInfo GetMockaroo_Type_List() => GetFile(@"mockaroo_type_list.tt");

		public static FileInfo GetMore_Complex_Server_Response() => GetFile(@"more_complex_server_response.json");

		public static FileInfo GetMutable-Response() => GetFile(@"mutable-response.json");

		public static FileInfo GetNested-Response() => GetFile(@"nested-response.json");

	}
}
