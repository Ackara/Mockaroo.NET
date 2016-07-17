using CommandLine;

namespace Mockaroo.Command
{
    public abstract class ExportVerbOptionsBase
    {
        [Option('k', "key", Required = true, HelpText = "say something useful.")]
        public string Key { get; set; }

        [Option('o', "outputPath", HelpText = "Say something useful.")]
        public string OutputPath { get; set; }

        [Option('r', "rows", DefaultValue = 10, HelpText = "Say something useful.")]
        public int Rows { get; set; }
    }
}