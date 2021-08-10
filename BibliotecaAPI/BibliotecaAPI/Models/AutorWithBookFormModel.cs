using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Models
{
    public class AutorWithBookFormModel : AutorFormModel
    {
        public IEnumerable<LibroFormModel> Libros { get; set; }

    }
}