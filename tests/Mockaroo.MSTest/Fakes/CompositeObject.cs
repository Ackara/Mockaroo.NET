using System.Collections.Generic;

namespace Acklann.Mockaroo.Fakes
{
    public class CompositeObject
    {
        public BasicObject Basic;
        public NestedObject Nested;
        
        public int Id { get; set; }
        public List<BasicObject> Collection1 { get; set; }
    }
}