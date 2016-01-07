using System;
using System.IO;

namespace Tests.Mockaroo.NET
{
    public class Api
    {
        public static string GetKey()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "API_Keys", "mockaroo.txt");
            return File.ReadAllText(path).Trim();
        }
    }
}