using BibliotecaAPI.Exceptions;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AutoresController : Controller
    {

        private IAutoresService _autoresService;
        private IFileService _fileService;
        private ILibrosService _librosService;

        public AutoresController(IAutoresService autoresService, IFileService fileService, ILibrosService libroService)
        {
            _autoresService = autoresService;
            _fileService = fileService;
            _librosService = libroService;
        }

        // api/autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorModel>>> GetAutoresAsync(string orderBy = "Id")
        {
            try
            {
                var autores = await _autoresService.GetAutoresAsync(orderBy);
                return Ok(autores);
            }
            catch (InvalidOperationItemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        // api/autores/top
        [HttpGet("libros/top")]
        public async Task<ActionResult<IEnumerable<AutorModel>>> GetTopLibrosAsync(string orderBy = "puntuacion")
        {
            try
            {
                var libros = await _librosService.GetLibrosTopAsync(orderBy);
                return Ok(libros);
            }
            catch (InvalidOperationItemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        // api/autores/2
        [HttpGet("{autorId:long}")]
        public async Task<ActionResult<AutorWithLibroModel>> GetAutorAsync(long autorId)
        {
            try
            {
                var autor = await _autoresService.GetAutorAsync(autorId);
                return Ok(autor);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        // api/autores
        [HttpPost]
        public async Task<ActionResult<AutorModel>> CreateAutorFormAsync([FromBody] AutorFormModel newAutor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var file = newAutor.Image;
                string imagePath = _fileService.UploadFile(file);

                newAutor.FotografiaPath = imagePath;

                var autor = await _autoresService.CreateAutorAsync(newAutor);
                return Created($"/api/autores/{autor.Id}", autor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }


        [HttpDelete("{autorId:long}")]
        public async Task<ActionResult<bool>> DeleteAutorAsync(long autorId)
        {
            try
            {
                var result = await _autoresService.DeleteAutorAsync(autorId);
                return Ok(result);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpPut("{autorId:long}")]
        public async Task<ActionResult<AutorModel>> UpdateAutorAsync(long autorId, [FromBody] AutorModel updatedAutor)
        {
            try
            {
                /*if (!ModelState.IsValid)
                {
                    foreach (var pair in ModelState)
                    {
                        if (pair.Key == nameof(updatedAutor.City) && pair.Value.Errors.Count > 0)
                        {
                            return BadRequest(pair.Value.Errors);
                        }
                    }
                }*/

                var autor = await _autoresService.UpdateAutorAsync(autorId, updatedAutor);
                return Ok(autor);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }
    }
}
