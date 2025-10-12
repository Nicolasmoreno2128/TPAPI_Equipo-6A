using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dominio;
using negocio;
using ApiProducto.Models;

namespace ApiProducto.Controllers
{
    public class ImagenController : ApiController
    {


        // POST: api/Imagen
        public void Post([FromBody]ImagenDto imagen)
        {
           
                ImagenNegocio negocio = new ImagenNegocio();

                foreach (var url in imagen.Imagenes)
                {
                    Imagen nueva = new Imagen
                    {
                        IdArticulo = imagen.Id,
                        ImagenUrl = url
                    };
                    negocio.agregar(nueva);
                }
        }
    }
}
