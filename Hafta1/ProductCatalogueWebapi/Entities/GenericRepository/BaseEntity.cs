using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalogueWebapi.Entities.GenericRepository
{
    public class BaseEntity : IEntity
    {

        // Id auto increment
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    
    }
}