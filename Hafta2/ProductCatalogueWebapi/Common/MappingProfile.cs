using AutoMapper;
using ProductCatalogueWebapi.Entities;
using ProductCatalogueWebapi.Operations.GenreOperations.GetGenreDetail;
using ProductCatalogueWebapi.Operations.GenreOperations.GetGenres;
using ProductCatalogueWebapi.Operations.ProductOperations.GetProductDetail;
using ProductCatalogueWebapi.Operations.ProductOperations.GetProducts;
using static ProductCatalogueWebapi.Operations.GenreOperations.CreateGenre.CreateGenreCommand;
using static ProductCatalogueWebapi.Operations.ProductOperations.CreateProduct.CreateProductCommand;
// using static ProductCatalogueWebapi.Operations.ProductOperations.CreateProduct.CreateProductCommand;

namespace ProductCatalogueWebapi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductModel, Product>();
            CreateMap<Product, ProductDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src=>(src.Genre.Title)));
            CreateMap<Product, ProductDetailViewModelByTitle>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src=>(src.Genre.Title)));
            CreateMap<Product, ProductViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src=>(src.Genre.Title)));

            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre,GenreDetailViewModel>();
            CreateMap<Genre,GenreDetailViewModelByTitle>();


        }
    }
}