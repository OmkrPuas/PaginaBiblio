using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Models
{
    public class LibroModel
    {
        public long Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string AnioPublicacion { get; set; }
        public string ImagePath { get; set; }
        public long puntuacion { get; set; }
        public long AutorId { get; set; }
    }
}
