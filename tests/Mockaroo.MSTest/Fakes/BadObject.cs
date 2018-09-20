namespace Acklann.Mockaroo.Fakes
{
    public class BadObject
    {
        public BadObject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}