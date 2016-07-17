namespace Mockaroo.Command
{
    public class NullCommand : ICommand
    {
        public int Execute(object args)
        {
            return ExitCode.ParsingError;
        }
    }
}