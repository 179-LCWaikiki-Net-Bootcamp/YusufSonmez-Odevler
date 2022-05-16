using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogueWebapi.Attributes;
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

        [Branch("LC Waikiki")]
        // GET /Products/Search
        [HttpGet("Search")]
        public ActionResult<IEnumerable<Product>> Search([FromQuery] SomeQuery search)
        {
            ProductDetailViewModelByTitle result;
            try
            {
                GetProductDetailQueryByTitle query = new GetProductDetailQueryByTitle(context, _mapper);
                query.Title = search.searchQuery;
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
            
            if(ModelState.IsValid)
            {
                // try
                // {
                    command.Model = newProduct;
                    command.Handle();
                    return Ok();
                // }
                // catch(Exception ex)
                // {
                //     return BadRequest(ex.Message);
                // }
            }
            else
            {
                return BadRequest("Uygun olmayan bir model girdiniz!");
            }
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
        }
    }
}