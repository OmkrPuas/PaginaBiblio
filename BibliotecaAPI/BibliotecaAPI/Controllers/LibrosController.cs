using BibliotecaAPI.Exceptions;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Controllers
{
    [Route("api/autores/{autorId:long}/[controller]")]
    public class LibrosController : ControllerBase
    {

        private ILibrosService _libroService;
        private IFileService _fileService;

        public LibrosController(ILibrosService libroService, IFileService fileService)
        {
            _libroService = libroService;
            _fileService = fileService;
        }

        //localhost:3030/api/autores/{autorId:long}/libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroModel>>> GetLibrosAsync(long autorId, string orderBy = "Id")
        {
            try
            {
                var libros = await _libroService.GetLibrosAsync(autorId, orderBy);
                return Ok(libros);
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

        [HttpGet("{libroId:long}")]
        public async Task<IActionResult> GetLibroAsync(long autorId, long libroId)
        {
            try
            {
                var libro = await _libroService.GetLibroAsync(autorId, libroId);
                return Ok(libro);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }

        }

        [HttpPost("Antiguo")]
        public async Task<ActionResult<LibroModel>> CreateLibroAsync(long autorId, [FromBody] LibroModel newLibro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdLibro = await _libroService.CreateLibroAsync(autorId, newLibro);
                return Created($"/api/autores/{autorId}/libros/{createdLibro.Id}", createdLibro);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<LibroModel>> CreateLibroFormAsync(long autorId, [FromForm] LibroFormModel newLibro)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var file = newLibro.Image;
                string imagePath = _fileService.UploadFile(file);

                newLibro.ImagePath = imagePath;

                var libro = await _libroService.CreateLibroAsync(autorId, newLibro);
                return Created($"/api/autores/{autorId}/libros/{libro.Id}", libro);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpDelete("{libroId:int}")]
        public async Task<ActionResult<bool>> DeleteLibroAsync(long autorId, long libroId)
        {
            try
            {

                var result = await _libroService.DeleteLibroAsync(autorId, libroId);
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

        [HttpPut("{libroId:long}")]
        public async Task<ActionResult<LibroModel>> UpdateLibroAsync(long autorId, long libroId, [FromBody] LibroFormModel libroToUpdate)
        {
            try
            {

                var file = libroToUpdate.Image;
                string imagePath = _fileService.UploadFile(file);

                libroToUpdate.ImagePath = imagePath;

                var updatedPayer = await _libroService.UpdateLibroAsync(autorId, libroId, libroToUpdate);
                return Ok(updatedPayer);
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

        [HttpPut("{libroId:long}/like")]
        public async Task<ActionResult<LibroModel>> UpdateLikeLibroAsync(long autorId, long libroId, [FromBody] LibroModel libroToUpdate)
        {
            try
            {
                var updatedPayer = await _libroService.UpdateLibroAsync(autorId, libroId, libroToUpdate);
                return Ok(updatedPayer);
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
