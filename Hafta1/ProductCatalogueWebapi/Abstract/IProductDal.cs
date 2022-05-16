using ProductCatalogueWebapi.Entities;
using System.Collections.Generic;


namespace ProductCatalogueWebapi.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        List<Product> GetProductsWithGenre(int genreId);
    }
}
