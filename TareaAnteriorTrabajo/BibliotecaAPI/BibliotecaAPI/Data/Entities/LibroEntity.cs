using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Data.Entities
{
    public class LibroEntity
    {
        [Key]
        [Required]
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string AnioPublicacion { get; set; }
        public string ImagePath { get; set; }
        public long puntuacion { get; set; }

        [ForeignKey("AutorId")]
        public virtual AutorEntity Autor { get; set; }
    }
}

