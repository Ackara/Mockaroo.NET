using System.IO;

namespace Acklann.Mockaroo
{
    public class Config
    {
        public static string GetApikey()
        {
            string file = Path.Combine(Path.GetDirectoryName(TestData.DirectoryName), "your_mockaroo_key.txt");
            string apikey = File.ReadAllText(file);
            if (string.IsNullOrEmpty(apikey)) throw new System.NullReferenceException($"You must paste a mockaroo api-key in the '{file}' file.");
            else return apikey;
        }
    }
}