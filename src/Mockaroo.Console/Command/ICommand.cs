namespace Mockaroo.Command
{
    public interface ICommand
    {
        int Execute(object args);
    }
}