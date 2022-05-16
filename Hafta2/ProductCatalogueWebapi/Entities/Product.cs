using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductCatalogueWebapi.Entities.GenericRepository;

namespace ProductCatalogueWebapi.Entities{
   public class Product : BaseEntity
   {
       [Range(1, 3)]
       public int GenreId { get; set; }
       public Genre Genre { get; set; }
       public double Price { get; set; }
   } 
}