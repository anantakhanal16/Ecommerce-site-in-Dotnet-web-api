using Core.Entities;


namespace Core.Specifications
{
    public class ProductsWithTypesSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesSpecification(string? sort,int? brandId, int? typeId)
            :base(x=> 
            (!brandId.HasValue || x.ProductBrandId == brandId) &&
            (!typeId.HasValue || x.ProductTypeId == typeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDecending(x => x.Price);
                        break;
                    default:
                        AddOrderBy( n=> n.Name);
                        break;
                }
            }
        }
        public ProductsWithTypesSpecification(int id):base (x=>x.Id ==id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
