using System;
using System.IO;

namespace Tests.Mockaroo
{
    public static class ApiKey
    {
        public static string GetValue()
        {
            string key = null;

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "apikey.txt");
            if (File.Exists(path)) key = File.ReadAllText(path).Trim();
            else throw new FileNotFoundException($"Cannot find '{path}'.");

            if (string.IsNullOrEmpty(key)) throw new NullReferenceException("The api key cannot be null.");
            else return key;
        }
    }
}