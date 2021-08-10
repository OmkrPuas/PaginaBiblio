using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Data.Entities
{
    public class AutorEntity
    {
        [Key]
        [Required]
        public long? Id { get; set; }
        public string Nombre { get; set; }
        public string Nacionalidad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Biografia { get; set; }

        public string FotografiaPath { get; set; }

        public ICollection<LibroEntity> Libros { get; set; }
    }
}
