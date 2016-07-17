using CommandLine;
using CommandLine.Text;
using System;
using System.Reflection;

namespace Mockaroo.Command
{
    public class Verbs
    {
        [VerbOption("export-clr",
            HelpText = ("pulls sample data from 'https://mockaroo.com' then stores in a file. "
                        + "USAGE: export "
                        + "--key <your_api_key> "
                        + "--assembly <path_to_dll> "
                        + "--type <the_type_name>"))]
        public ExportClrVerbOptions Export { get; set; }

        [HelpVerbOption]
        public string GetHelp(string verbName)
        {
            var helpText = HelpText.AutoBuild(this);
            helpText.AddDashesToOption = false;
            helpText.AdditionalNewLineAfterOption = false;
            helpText.AddPreOptionsLine("--- HELP ---");

            bool verbNameIsNotValid = true;
            var verbArgs = GetType().GetProperties();
            foreach (var arg in verbArgs)
            {
                var verbInfo = arg.GetCustomAttribute<VerbOptionAttribute>();
                if ((verbName ?? string.Empty).Equals(verbInfo?.LongName, StringComparison.CurrentCultureIgnoreCase))
                {
                    helpText.AddPreOptionsLine($"The '{verbInfo.LongName}' command {verbInfo.HelpText}");
                    helpText.AddPreOptionsLine("options:");
                    helpText.AddOptions(arg.GetValue(this), "[Required]");
                    verbNameIsNotValid = false;
                    break;
                }
            }

            if (verbNameIsNotValid) { helpText.AddPreOptionsLine($"The following commands are available."); }

            return helpText;
        }
    }
}