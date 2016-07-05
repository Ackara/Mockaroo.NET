using System;

namespace Mockaroo.Commands
{
    public abstract class CommandTemplate<T> : ICommand
    {
        protected T Options;

        public virtual int Execute(object args)
        {
            Options = (T)args;
            bool canExecute = Validate();
            if (canExecute) return Process();
            else return ExitCode.ParsingError;
        }

        protected abstract bool Validate();

        protected abstract int Process();

        protected void PrintGenericErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Unable to execute command.");
            Console.WriteLine("Please check that you have entered valid values then try again.");
        }
        
    }
}