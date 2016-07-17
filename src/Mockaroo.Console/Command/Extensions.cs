using CommandLine;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Mockaroo.Command
{
    public static class Extensions
    {
        public static HelpOptionAttribute GetHelp<T>(this T options, Expression<Func<T, object>> property)
        {
            Match match = new Regex(@"(?i)[_a-z0-9]+\.(?<property>[_a-z0-9]+)").Match(property.ToString());
            if (match.Success)
            {
                string propertyName = match.Groups["property"].Value;
                Type type = typeof(T);
                PropertyInfo propertyInfo = typeof(T).GetProperties().First(x => x.Name == propertyName);
                var help = propertyInfo.GetCustomAttribute<HelpOptionAttribute>();
                if (help == null) return new HelpOptionAttribute(propertyName) { HelpText = "N/A" };
                else return help;
            }

            throw new ArgumentException();
        }

        public static string GetHelpText<T>(this T options, Expression<Func<T, object>> property)
        {
            Match match = new Regex(@"(?i)[_a-z0-9]+\.(?<property>[_a-z0-9]+)").Match(property.ToString());
            if (match.Success)
            {
                string propertyName = match.Groups["property"].Value;
                PropertyInfo propertyInfo = typeof(T).GetProperties().First(x => x.Name == propertyName);
                var help = propertyInfo.GetCustomAttribute<OptionAttribute>();
                return $"--{help.LongName}\t{help.HelpText}.";
            }

            throw new ArgumentException();
        }
    }
}