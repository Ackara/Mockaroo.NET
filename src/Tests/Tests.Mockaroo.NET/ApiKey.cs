using System;
using System.IO;

namespace Tests.Mockaroo
{
    public class ApiKey
    {
        public static string GetValue()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "API_Keys", "mockaroo.txt");
            return File.ReadAllText(path).Trim();
        }
    }
}