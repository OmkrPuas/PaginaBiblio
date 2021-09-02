using BibliotecaAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Data.Repositories
{
    public interface IBibliotecaRepository
    {
        //autores
        public Task<IEnumerable<AutorEntity>> GetAutoresAsync(string orderBy = "Id");

        public Task<IEnumerable<LibroEntity>> GetLibrosTopAsync(string orderBy = "puntuacion");
        public Task<AutorEntity> GetAutorAsync(long autorId);
        public void CreateAutor(AutorEntity newAutor);
        public Task DeleteAutorAsync(long autorId);
        public Task UpdateAutorAsync(long autorId, AutorEntity updatedAutor);

        //libros
        public Task<IEnumerable<LibroEntity>> GetLibrosAsync(long autorId, string orderBy = "Id");
        public Task<LibroEntity> GetLibroAsync(long autorId, long libroId);
        public void CreateLibro(long autorId, LibroEntity newLibro);
        public Task DeleteLibroAsync(long autorId, long libroId);
        public Task UpdateLibroAsync(long autorId, long libroId, LibroEntity updatedLibro);

        Task<bool> SaveChangesAsync();
    }
}

