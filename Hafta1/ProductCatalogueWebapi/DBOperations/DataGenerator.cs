using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalogueWebapi.Entities;

namespace ProductCatalogueWebapi.DBOperations
{
    public class DataGenerator
    {
        // Inmemory
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new ProjectDbContext(serviceProvider.GetRequiredService<DbContextOptions<ProjectDbContext>>()))
            {
                if(context.Products.Any())
                {
                    // Bir liste zaten varsa hiçbir şey dönme.
                    return;
                }

                context.Products.AddRange(
                        new Product{
                            Title = "Ceket",
                            GenreId = 1,
                            Price = 50.0
                        },
                        new Product{
                            Title = "Pantolon",
                            GenreId = 3,
                            Price = 45.0
                        },
                        new Product{
                            Title = "Kazak",
                            GenreId = 2,
                            Price = 55.0
                        },
                        new Product{
                            Title = "Gömlek",
                            GenreId = 1,
                            Price = 70.0
                        },
                        new Product{
                            Title = "Şort",
                            GenreId = 2,
                            Price = 100.0
                        }
                );
                context.SaveChanges();

                

                if(context.Genres.Any())
                {
                    // Bir liste zaten varsa hiçbir şey dönme.
                    return;
                }
                context.Genres.AddRange(
                    new Genre{
                        Title="Yazlık"
                    },
                    new Genre{
                        Title="Kışlık"
                    },
                    new Genre{
                        Title="Baharlık"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}






