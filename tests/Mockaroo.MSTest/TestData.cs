using System;
using System.IO;
using System.Linq;

namespace Acklann.Mockaroo
{
	public static partial class TestData
	{
		public const string FOLDER_NAME = "test-data";

		public static string DirectoryName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FOLDER_NAME);
        
		public static FileInfo GetFile(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            string searchPattern = $"*{Path.GetExtension(fileName)}";

            string appDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FOLDER_NAME);
            return new DirectoryInfo(appDirectory).EnumerateFiles(searchPattern, SearchOption.AllDirectories)
                .First(x => x.Name.Equals(fileName, StringComparison.CurrentCultureIgnoreCase));
        }

		public static FileInfo GetBasicResponse() => GetFile(@"basic-response.json");

		public static FileInfo GetCollectionResponse() => GetFile(@"collection-response.json");

		public static FileInfo GetCompositeResponse() => GetFile(@"composite-response.json");

		public static FileInfo GetDictonaryResponse() => GetFile(@"dictonary-response.json");

		public static FileInfo GetImmutableResponse() => GetFile(@"immutable-response.json");

		public static FileInfo GetNestedResponse() => GetFile(@"nested-response.json");

	}
}
