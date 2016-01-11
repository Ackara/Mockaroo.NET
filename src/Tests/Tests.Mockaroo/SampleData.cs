using System;
using System.IO;
using System.Linq;

namespace Tests.Mockaroo
{
    public class SampleData
    {
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
    }
}