using System;

namespace Acklann.Mockaroo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("Press any key...");
            Console.ReadKey();
#endif
        }

        private static void PrintHeader()
        {
            Console.Title = "Mockaroo.NET";
            // TODO: Print headers
        }
    }
}