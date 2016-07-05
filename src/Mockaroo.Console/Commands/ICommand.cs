namespace Mockaroo.Commands
{
    public interface ICommand
    {
        int Execute(object args);
    }
}