using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogueWebapi.Entities;

using ProductCatalogueWebapi.Operations.GenreOperations.CreateGenre;
using ProductCatalogueWebapi.Operations.GenreOperations.DeleteGenre;
using ProductCatalogueWebapi.Operations.GenreOperations.GetGenreDetail;
using ProductCatalogueWebapi.Operations.GenreOperations.GetGenres;
using ProductCatalogueWebapi.Operations.GenreOperations.UpdateGenre;
using static ProductCatalogueWebapi.Operations.GenreOperations.CreateGenre.CreateGenreCommand;
using static ProductCatalogueWebapi.Operations.GenreOperations.UpdateGenre.UpdateGenreCommand;

namespace ProductCatalogueWebapi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController:ControllerBase
    {
        private readonly ProjectDbContext context;
        private readonly IMapper _mapper;

        public GenreController(ProjectDbContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }

        // GET /Genres
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(context, _mapper);
            var result = query.Handle();

            return Ok(result);
        }

        // // GET /Genres/{id}
        [HttpGet("/Genres/{id}")]
        public IActionResult GetGenreById(int id)
        {
            GenreDetailViewModel result;
            try
            {
                GetGenreDetailQuery query = new GetGenreDetailQuery(context, _mapper);
                query.GenreId = id;
                result = query.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        // // GET /Genres/Search
        [HttpGet("Search")]
        public ActionResult<IEnumerable<Genre>> Search(string search)
        {
            GenreDetailViewModelByTitle result;
            try
            {
                GetGenreDetailQueryByTitle query = new GetGenreDetailQueryByTitle(context, _mapper);
                query.Title = search;
                result = query.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        // POST /Genres/{id}
        [HttpPost]
        public IActionResult AddProduct([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(context, _mapper);
            
                try
                {
                    if(ModelState.IsValid) // çalışmıyor buraya dön
                    {
                    command.Model = newGenre;
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
            
            return Ok("Tür başarıyla eklendi!");
        }
        // PUT /Genres/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreModel updateGenre)
        {
            try
            {
                UpdateGenreCommand command = new UpdateGenreCommand(context);
                command.GenreId = id;
                command.Model = updateGenre;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        // DELETE /Genres/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteGenreById(int id)
        {
            
            try
            {
                DeleteGenreCommand command = new DeleteGenreCommand(context);
                command.GenreId = id;
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