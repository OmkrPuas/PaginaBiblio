using BibliotecaAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Data.Repositories
{
    public class BibliotecaRepository : IBibliotecaRepository
    {
        private BibliotecaDbContext _dbContext;

        public BibliotecaRepository(BibliotecaDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void CreateLibro(long autorId, LibroEntity newLibro)
        {
            _dbContext.Entry(newLibro.Autor).State = EntityState.Unchanged;
            _dbContext.Libros.Add(newLibro);

        }

        public void CreateAutor(AutorEntity newAutor)
        {
            _dbContext.Autores.Add(newAutor);
        }

        public async Task DeleteLibroAsync(long autorId, long libroId)
        {
            var libroToDelete = await _dbContext.Libros.FirstOrDefaultAsync(p => p.Id == libroId);
            _dbContext.Remove(libroToDelete);
        }

        public async Task DeleteAutorAsync(long autorId)
        {
            var autorToDelete = await _dbContext.Autores.FirstAsync(t => t.Id == autorId);
            _dbContext.Autores.Remove(autorToDelete);

        }

        public async Task<LibroEntity> GetLibroAsync(long autorId, long libroId)
        {
            IQueryable<LibroEntity> query = _dbContext.Libros;
            query = query.AsNoTracking();
            //query = query.Include(p => p.Autor);
            return await query.FirstOrDefaultAsync(p => p.Autor.Id == autorId && p.Id == libroId);
        }

        public async Task<IEnumerable<LibroEntity>> GetLibrosAsync(long autorId, string orderBy = "Id")
        {
            IQueryable<LibroEntity> query = _dbContext.Libros;
            query = query.Where(p => p.Autor.Id == autorId);
            query = query.Include(p => p.Autor);
            query = query.AsNoTracking();
            switch (orderBy.ToLower())
            {
                case "titulo":
                    query = query.OrderBy(t => t.Titulo);
                    break;
                case "puntuacion":
                    query = query.OrderBy(t => t.puntuacion);
                    break;
                default:
                    query = query.OrderBy(t => t.Id);
                    break;
            }
            return await query.ToListAsync();
        }

        public async Task<AutorEntity> GetAutorAsync(long autorId)
        {
            IQueryable<AutorEntity> query = _dbContext.Autores;
            query = query.AsNoTracking();
            query = query.Include(t => t.Libros);
            return await query.FirstOrDefaultAsync(t => t.Id == autorId);

            //hit to database
            //tolist()
            //toArray()
            //foreach
            //firstOfDefaul
            //Single
            //Count
        }

        public async Task<IEnumerable<AutorEntity>> GetAutoresAsync(string orderBy = "Id")
        {
            IQueryable<AutorEntity> query = _dbContext.Autores;
            query = query.AsNoTracking();

            switch (orderBy.ToLower())
            {
                case "name":
                    query = query.OrderBy(t => t.Nombre);
                    break;
                case "nacionalidad":
                    query = query.OrderBy(t => t.Nacionalidad);
                    break;
                default:
                    query = query.OrderBy(t => t.Id);
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<LibroEntity>> GetLibrosTopAsync(string orderBy = "puntuacion")
        {
            IQueryable<LibroEntity> query = _dbContext.Libros;
            query = query.AsNoTracking();
            query = query.Include(p => p.Autor);
            query = query.OrderByDescending(t => t.puntuacion);
            return await query.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var res = await _dbContext.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task UpdateLibroAsync(long autorId, long libroId, LibroEntity updatedLibro)
        {
            var libroToUpdate = await _dbContext.Libros.FirstOrDefaultAsync(p => p.Id == libroId);
            libroToUpdate.Titulo = updatedLibro.Titulo ?? libroToUpdate.Titulo;
            libroToUpdate.Genero = updatedLibro.Genero ?? libroToUpdate.Genero;
            libroToUpdate.AnioPublicacion = updatedLibro.AnioPublicacion ?? libroToUpdate.AnioPublicacion;
            if(updatedLibro.puntuacion > libroToUpdate.puntuacion)
            {
                libroToUpdate.puntuacion = updatedLibro.puntuacion;
            }
        }

        public async Task UpdateAutorAsync(long autorId, AutorEntity updatedAutor)
        {
            var autor = await _dbContext.Autores.FirstOrDefaultAsync(t => t.Id == autorId);

            autor.Nombre = updatedAutor.Nombre ?? autor.Nombre;
            autor.Nacionalidad = updatedAutor.Nacionalidad ?? autor.Nacionalidad;
            autor.FechaNacimiento = updatedAutor.FechaNacimiento ?? autor.FechaNacimiento;
            autor.Biografia = updatedAutor.Biografia ?? autor.Biografia;
        }
    }
}
