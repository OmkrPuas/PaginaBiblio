using AutoMapper;
using BibliotecaAPI.Data.Entities;
using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Data
{
    public class AtomapperProfile : Profile
    {
        public AtomapperProfile()
        {
            this.CreateMap<AutorModel, AutorEntity>()
                //.ForMember(tm => tm.Name, te => te.MapFrom(m => m.Name))
                .ReverseMap();

            this.CreateMap<LibroModel, LibroEntity>()
                .ForMember(ent => ent.Autor, mod => mod.MapFrom(modSrc => new AutorEntity() { Id = modSrc.AutorId }))
                .ReverseMap()
                .ForMember(mod => mod.AutorId, ent => ent.MapFrom(entSrc => entSrc.Autor.Id));

            this.CreateMap<AutorWithLibroModel, AutorEntity>()
                .ReverseMap();
        }
    }
}
