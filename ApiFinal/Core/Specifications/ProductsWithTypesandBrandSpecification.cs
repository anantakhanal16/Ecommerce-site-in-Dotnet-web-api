using Core.Entities;


namespace Core.Specifications
{
    public class ProductsWithTypesandBrandSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesandBrandSpecification(ProductsSpecParams productParams)
            :base(x=> 
            (string.IsNullOrEmpty(productParams.Search)

            ||x.Name.ToLower().Contains(productParams.Search))

            &&

            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue  || x.ProductTypeId == productParams.TypeId)
            )
        {
            AddInclude(x => x.ProductType);

            AddInclude(x => x.ProductBrand);

            AddOrderBy(x => x.Name);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1),
                productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
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
        public ProductsWithTypesandBrandSpecification(int id):base (x=>x.Id ==id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
