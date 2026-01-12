using skinet.Entities;

namespace skinet.Specifications
{
    public class BrandListSpecification:BaseSpecification<Product,string>
    {
        public BrandListSpecification()
        {
            AddSelect(x=>x.Brand);
            AddDistinct();
        }
    }
}
