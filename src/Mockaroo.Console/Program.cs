using Mockaroo.Command;
using System;

namespace Mockaroo
{
    internal class Program
    {
        private static int ExitCode;

        private static void Main(string[] args)
        {
            PrintHeader();
            CreateEventBindings();

            CommandLine.Parser.Default.ParseArgumentsStrict(args, new Verbs(),
                onVerbCommand: (verb, options) =>
                {
                    ICommand command = new CommandFactory().CreateInstance(options);
                    ExitCode = command.Execute(options);
                },
                onFail: () =>
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("The previous command was not valid, please enter a valid command.");
                    Console.ResetColor();
                });
#if DEBUG
            Console.WriteLine("EXIT: {0}", ExitCode);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
#endif
            Environment.Exit(ExitCode);
        }

        private static void PrintHeader()
        {
            Console.Title = "Mockaroo.NET";
            // TODO: Print headers
        }

        private static void CreateEventBindings()
        {
            // TODO: Subscribe to exit events
        }
    }
}