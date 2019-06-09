using System;

namespace Acklann.Mockaroo.Fakes
{
    public class SpecialObject
    {
        public string Name { get; set; }

        public int this[int index]
        {
            get { return 0; }
            set { }
        }

        public event EventHandler Changed;
    }
}