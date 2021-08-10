using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaAPI.Models
{
    public class LibroFormModel : LibroModel
    {
        public IFormFile Image { get; set; }
    }
}

