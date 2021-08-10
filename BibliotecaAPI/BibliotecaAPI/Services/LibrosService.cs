using AutoMapper;
using BibliotecaAPI.Data.Entities;
using BibliotecaAPI.Data.Repositories;
using BibliotecaAPI.Exceptions;
using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Services
{
    public class LibrosService : ILibrosService
    {
        private IBibliotecaRepository _bibliotecaRepository;
        private IMapper _mapper;

        private HashSet<string> _allowedOrderByValues = new HashSet<string>()
        {
            "id",
            "titulo",
            "puntuacion"
        };

        public LibrosService(IBibliotecaRepository bibliotecaRepository, IMapper mapper)
        {
            _bibliotecaRepository = bibliotecaRepository;
            _mapper = mapper;
        }


        public async Task<LibroModel> CreateLibroAsync(long autorId, LibroModel newLibro)
        {
            await ValidateAutorAsync(autorId);
            newLibro.AutorId = autorId;
            var libroEntity = _mapper.Map<LibroEntity>(newLibro);

            _bibliotecaRepository.CreateLibro(autorId, libroEntity);

            var result = await _bibliotecaRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return _mapper.Map<LibroModel>(libroEntity);
        }

        public async Task<bool> DeleteLibroAsync(long autorId, long libroId)
        {
            await ValidateAutorAndPlaterAsync(autorId, libroId);
            await _bibliotecaRepository.DeleteLibroAsync(autorId, libroId);

            var result = await _bibliotecaRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return true;
        }

        public async Task<LibroModel> GetLibroAsync(long autorId, long libroId)
        {
            await ValidateAutorAsync(autorId);
            var libroEntity = await _bibliotecaRepository.GetLibroAsync(autorId, libroId);
            if (libroEntity == null)
            {
                throw new NotFoundItemException($"The libro with id: {libroId} does not exist in autor with id:{autorId}.");
            }

            var libroModel = _mapper.Map<LibroModel>(libroEntity);

            libroModel.AutorId = autorId;
            return libroModel;
        }

        public async Task<IEnumerable<LibroModel>> GetLibrosAsync(long autorId, string orderBy = "Id")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            await ValidateAutorAsync(autorId);
            var libros = await _bibliotecaRepository.GetLibrosAsync(autorId, orderBy.ToLower());
            return _mapper.Map<IEnumerable<LibroModel>>(libros);
        }

        public async Task<IEnumerable<LibroModel>> GetLibrosTopAsync(string orderBy = "puntuacion")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            var libros = await _bibliotecaRepository.GetLibrosTopAsync(orderBy.ToUpper());
            return _mapper.Map<IEnumerable<LibroModel>>(libros);
        }

        public async Task<LibroModel> UpdateLibroAsync(long autorId, long libroId, LibroModel updatedLibro)
        {
            await ValidateAutorAndPlaterAsync(autorId, libroId);
            await _bibliotecaRepository.UpdateLibroAsync(autorId, libroId, _mapper.Map<LibroEntity>(updatedLibro));
            var result = await _bibliotecaRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return updatedLibro;
        }

        private async Task ValidateAutorAsync(long autorId)
        {
            var autor = await _bibliotecaRepository.GetAutorAsync(autorId);
            if (autor == null)
            {
                throw new NotFoundItemException($"The autor with id: {autorId} does not exists.");
            }
        }

        private async Task ValidateAutorAndPlaterAsync(long autorId, long libroId)
        {
            var libro = await GetLibroAsync(autorId, libroId);
        }
    }
}
