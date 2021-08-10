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
    public class AutoresService : IAutoresService
    {

        private IBibliotecaRepository _bibliotecaRepository;
        private IMapper _mapper;

        private HashSet<string> _allowedOrderByValues = new HashSet<string>()
        {
            "id",
            "nombre",
            "nacionalidad"
        };

        public AutoresService(IBibliotecaRepository bibliotecaRepository, IMapper mapper)
        {
            _bibliotecaRepository = bibliotecaRepository;
            _mapper = mapper;
        }

        public async Task<AutorModel> CreateAutorAsync(AutorModel newAutor)
        {
            var autorEntity = _mapper.Map<AutorEntity>(newAutor);
            _bibliotecaRepository.CreateAutor(autorEntity);
            var result = await _bibliotecaRepository.SaveChangesAsync();

            if (result)
            {
                return _mapper.Map<AutorModel>(autorEntity);
            }

            throw new Exception("Database Error");
        }

        public async Task<bool> DeleteAutorAsync(long autorId)
        {
            await ValidateAutorAsync(autorId);
            await _bibliotecaRepository.DeleteAutorAsync(autorId);
            var result = await _bibliotecaRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }
            return true;
        }

        public async Task<AutorWithLibroModel> GetAutorAsync(long autorId)
        {
            var autor = await _bibliotecaRepository.GetAutorAsync(autorId);

            if (autor == null)
            {
                throw new NotFoundItemException($"The autor with id: {autorId} does not exists.");
            }


            return _mapper.Map<AutorWithLibroModel>(autor);
        }

        public async Task<IEnumerable<AutorModel>> GetAutoresAsync(string orderBy = "Id")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            var entityList = await _bibliotecaRepository.GetAutoresAsync(orderBy.ToLower());
            var modelList = _mapper.Map<IEnumerable<AutorModel>>(entityList);
            return modelList;
        }

        public async Task<IEnumerable<LibroModel>> GetLibrosTopAsync(string orderBy = "puntuacion")
        {
            throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            var entityList = await _bibliotecaRepository.GetLibrosTopAsync(orderBy.ToUpper());
            var modelList = _mapper.Map<IEnumerable<LibroModel>>(entityList);
            return modelList;
        }

        public async Task<AutorModel> UpdateAutorAsync(long autorId, AutorModel updatedAutor)
        {
            await GetAutorAsync(autorId);
            updatedAutor.Id = autorId;
            await _bibliotecaRepository.UpdateAutorAsync(autorId, _mapper.Map<AutorEntity>(updatedAutor));
            var result = await _bibliotecaRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return _mapper.Map<AutorModel>(updatedAutor);
        }

        private async Task ValidateAutorAsync(long autorId)
        {
            await GetAutorAsync(autorId);
        }
    }
}
