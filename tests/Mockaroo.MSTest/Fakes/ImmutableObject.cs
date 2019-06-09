namespace Acklann.Mockaroo.Fakes
{
    public struct ImmutableObject
    {
        public ImmutableObject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public readonly int Id;
        public string Name { get; private set; }
    }
}