using System;
using System.IO;
using System.Reflection;

namespace Mockaroo.Command
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
            throw new System.NotImplementedException();
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