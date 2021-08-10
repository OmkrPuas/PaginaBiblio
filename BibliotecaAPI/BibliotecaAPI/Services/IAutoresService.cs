using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Services
{
    public interface IAutoresService
    {
        public Task<IEnumerable<AutorModel>> GetAutoresAsync(string orderBy = "Id");
        public Task<IEnumerable<LibroModel>> GetLibrosTopAsync(string orderBy = "puntuacion");
        public Task<AutorWithLibroModel> GetAutorAsync(long autorId);
        public Task<AutorModel> CreateAutorAsync(AutorModel newAutor);
        public Task<bool> DeleteAutorAsync(long autorId);
        public Task<AutorModel> UpdateAutorAsync(long autorId, AutorModel updatedAutor);
    }
}