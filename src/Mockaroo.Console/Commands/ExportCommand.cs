using Gigobyte.Mockaroo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Mockaroo.Commands
{
    [OptionsBinding(typeof(ExportClrVerbOptions))]
    public class ExportCommand : CommandTemplate<ExportClrVerbOptions>
    {
        protected override bool Validate()
        {
            // The following validates the mandatory arguments.
            if (!File.Exists(Options.AssemblyPath) || string.IsNullOrEmpty(Options.TypeName))
            {
                PrintGenericErrorMessage();
                return false;
            }

            // The following sets default values if not specified.
            string outputFile = Environment.ExpandEnvironmentVariables((Options.OutputPath ?? string.Empty));

            if (string.IsNullOrEmpty(outputFile))
            {
                string filename = (Options.TypeName);
                outputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, (($"{filename}.xml").ToLower()));
            }

            if (!File.Exists(outputFile))
            {
                string dir = Path.GetDirectoryName(outputFile);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                File.Create(outputFile).Dispose();
            }

            Options.OutputPath = outputFile;
            return true;
        }

        protected override int Process()
        {
            Options.Rows = 2;
            try
            {
                var client = new MockarooClient(Options.Key);
                Type dataType = FindTheMostSpecificType();
                IEnumerable<object> data = client.FetchDataAsync(dataType, new Schema(dataType), Options.Rows).Result;
                // TODO: save to file.

                return ExitCode.Success;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unable to execute command.");
                Console.WriteLine(ex.Message);
                return ExitCode.UnknownError;
            }
        }

        private Type FindTheMostSpecificType()
        {
            foreach (var type in Assembly.LoadFile(Options.AssemblyPath).GetExportedTypes())
            {
                if (type.FullName == Options.TypeName)
                {
                    return type;
                }
            }
            return null;
        }
    }
}