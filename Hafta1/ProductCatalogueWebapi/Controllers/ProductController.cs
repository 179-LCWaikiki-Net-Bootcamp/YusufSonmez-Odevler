using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogueWebapi.Entities;
using ProductCatalogueWebapi.Operations.ProductOperations.CreateProduct;
using ProductCatalogueWebapi.Operations.ProductOperations.DeleteProduct;
using ProductCatalogueWebapi.Operations.ProductOperations.GetProductDetail;
using ProductCatalogueWebapi.Operations.ProductOperations.GetProducts;
using ProductCatalogueWebapi.Operations.ProductOperations.UpdateProduct;
using static ProductCatalogueWebapi.Operations.ProductOperations.CreateProduct.CreateProductCommand;
using static ProductCatalogueWebapi.Operations.ProductOperations.UpdateProduct.UpdateProductCommand;


namespace ProductCatalogueWebapi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ProductController:ControllerBase
    {
        private readonly ProjectDbContext context;
        private readonly IMapper _mapper;

        public ProductController(ProjectDbContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }

        // GET /Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            GetProductsQuery query = new GetProductsQuery(context, _mapper);
            var result = query.Handle();

            return Ok(result);
        }

        // GET /Products/{id}
        [HttpGet("/Products/{id}")]
        public IActionResult GetProductById(int id)
        {
            ProductDetailViewModel result;
            try
            {
                GetProductDetailQuery query = new GetProductDetailQuery(context, _mapper);
                query.ProductId = id;
                result = query.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        // GET /Products/Search
        [HttpGet("Search")]
        public ActionResult<IEnumerable<Product>> Search(string search)
        {
            ProductDetailViewModelByTitle result;
            try
            {
                GetProductDetailQueryByTitle query = new GetProductDetailQueryByTitle(context, _mapper);
                query.Title = search;
                result = query.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        // POST /Products/{id}
        [HttpPost]
        public IActionResult AddProduct([FromBody] CreateProductModel newProduct)
        {
            CreateProductCommand command = new CreateProductCommand(context, _mapper);
            
                try
                {
                    if(ModelState.IsValid) // çalışmıyor buraya dön
                    {
                    command.Model = newProduct;
                    command.Handle();
                    }
                    else
                    {
                        return BadRequest("Model is not valid!");
                    }
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                
            
            

            // var product = context.Products.SingleOrDefault(x=>x.Title == newProduct.Title);

            // // Ürün stokta mevcut degilse girilmişse(required) kaydet ve Ok("Ürün başarıyla eklendi!") mesajı dön.
            // if(product is not null)
            // {
            //    return BadRequest("Ürün stokta mevcut!"); 
            // }

            // if(ModelState.IsValid)
            // {
            //     context.Products.Add(newProduct);
            //     context.SaveChanges();
            // }
                   
            
            return Ok("Ürün başarıyla eklendi!");
        }
        // PUT /Products/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id,[FromBody] UpdateProductModel updateProduct)
        {
            try
            {
                UpdateProductCommand command = new UpdateProductCommand(context);
                command.ProductId = id;
                command.Model = updateProduct;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
            // var product = context.Products.SingleOrDefault(x=>x.Id == id);
            // // Eger degistirilmek istenen title zaten var mı diye kontrol etmek icin eklendi.
            // var checkIfTitleExist = context.Products.SingleOrDefault(x=>x.Title == updateProduct.Title);

            // // Öyle bir id'ye sahip bir ürün var ise günceller ve Ok() fonksiyonu ile bunu bildirir, yok ise BadRequest() ile hata mesajı döner.
            // if(product is null)
            // {
            //     return NotFound("Ürün id'si bulunamadı!");
            // }
            
            // if(ModelState.IsValid)
            //     {
            //     if(checkIfTitleExist is null)
            //     // Bir ürün başka bir ürün adına sahip olacak şekilde degiştirilirse "Ürün stokta mevcut!" hatasını döner. Aksi takdirde güncellemeyi yapacaktır.
            //         {
            //             product.GenreId = updateProduct.GenreId != default ? updateProduct.GenreId : product.GenreId;
            //             product.Title = updateProduct.Title != default ? updateProduct.Title : product.Title;
            //             product.Price = updateProduct.Price != default ? updateProduct.Price : product.Price;
            //             context.SaveChanges();
            //             return Ok("Ürün başarıyla güncellendi!");
            //         }
            //     else
            //         {
            //             return BadRequest("Ürün stokta mevcut!");
            //         }
            //     }
            // else
            //     {
            //     return BadRequest("Model dogrulaması hatalı");
            //     }
        }

        // DELETE /Products/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProductById(int id)
        {
            
            try
            {
                DeleteProductCommand command = new DeleteProductCommand(context);
                command.ProductId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
            // var product = context.Products.SingleOrDefault(x=>x.Id == id);
            
            // // Öyle bir id'ye sahip bir ürün yoksa BadRequest() fonksiyonu ile hata döner, varsa ürünü siler ve Ok() fonksiyonu ile bunu bildirir.
            // if(product is null)
            // {
            //     return NotFound("Ürün id'si bulunamadı!");
            // }
            // else
            // {
            //     context.Products.Remove(product);
            //     context.SaveChanges();
            // }
            // return Ok("Ürün başarıyla silindi!");
        }
    }
}