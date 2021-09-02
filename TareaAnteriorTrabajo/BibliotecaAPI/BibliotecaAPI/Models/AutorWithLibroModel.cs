using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Models
{
    public class AutorWithLibroModel : AutorModel
    {
        public IEnumerable<LibroModel> Libros { get; set; }

    }
}
