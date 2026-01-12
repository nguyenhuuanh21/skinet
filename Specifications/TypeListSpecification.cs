using skinet.Entities;

namespace skinet.Specifications
{
    public class TypeListSpecification:BaseSpecification<Product,string>
    {
        public TypeListSpecification()
        {
            AddSelect(x => x.Type);
            AddDistinct();
        }
    }
}
