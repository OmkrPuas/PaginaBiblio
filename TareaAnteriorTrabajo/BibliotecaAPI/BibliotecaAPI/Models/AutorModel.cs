using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Models
{
    public class AutorModel
    {
        public long? Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Error {0} City name is invalid it should be at most {1} and at least {2}.", MinimumLength = 2)]
        public string Nacionalidad { get; set; }
        public string FotografiaPath { get; set; }
        public string Biografia { get; set; }
    }
}
