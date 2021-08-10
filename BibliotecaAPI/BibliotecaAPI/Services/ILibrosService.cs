using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Services
{
    public interface ILibrosService
    {
        public Task<IEnumerable<LibroModel>> GetLibrosAsync(long autorId, string orderBy = "Id");
        public Task<IEnumerable<LibroModel>> GetLibrosTopAsync(string orderBy = "puntuacion");
        public Task<LibroModel> GetLibroAsync(long autorId, long libroId);
        public Task<LibroModel> CreateLibroAsync(long autorId, LibroModel newLibro);
        public Task<bool> DeleteLibroAsync(long autorId, long libroId);
        public Task<LibroModel> UpdateLibroAsync(long autorId, long libroId, LibroModel updatedLibro);
    }
}
