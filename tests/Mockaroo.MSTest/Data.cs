using System;
using System.IO;
using System.Linq;

namespace Acklann.Mockaroo
{
    public static class Data
    {
        public const string DirectoryName = "SampleData";

        public static FileInfo GetFile(string filename)
        {
            string searchPattern = "*" + Path.GetExtension(filename);
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            return new DirectoryInfo(baseDirectory).GetFiles(searchPattern, SearchOption.AllDirectories)
                .First(x => x.Name == filename);
        }
    }
}