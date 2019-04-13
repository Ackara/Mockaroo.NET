using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Acklann.Mockaroo
{
    public static class Helper
    {
        public static string ComputeHash(this Schema schema)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(schema.ToString()));

            return BitConverter.ToString(hash);
        }

        internal static string ComputeHash(byte[] data)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(data);

            return BitConverter.ToString(hash);
        }

        internal static string ComputeHash(string filePath)
        {
            if (!File.Exists(filePath)) return string.Empty;

            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(File.ReadAllBytes(filePath));

            return BitConverter.ToString(hash);
        }

        internal static bool IsNullOrEmpty(this ICollection list)
        {
            return list == null || list.Count < 0;
        }

        internal static string CreateDirectory(string filePath)
        {
            string dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            return filePath;
        }
    }
}