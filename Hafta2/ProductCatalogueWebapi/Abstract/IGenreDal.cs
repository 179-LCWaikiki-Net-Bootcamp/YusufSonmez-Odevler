using ProductCatalogueWebapi.Entities;

namespace ProductCatalogueWebapi.Abstract
{
    public interface IGenreDal : IGenericDal<Genre>
    {
        public Genre GetSingleGenreByProductAsync(int productId);
    }
}
