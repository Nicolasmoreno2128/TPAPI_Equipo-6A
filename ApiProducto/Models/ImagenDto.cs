using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProducto.Models
{
    public class ImagenDto
    {

        public int Id { get; set; }
        public List<string> Imagenes { get; set; }
    }
}