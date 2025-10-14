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
        public HttpResponseMessage Post([FromBody]ImagenDto imagen)
        {
           
            try
            {

                ImagenNegocio negocio = new ImagenNegocio();

                if (imagen == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No se recibieron datos.");

                if (imagen.Id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El ID del artículo debe ser mayor a cero.");

                if (imagen.Imagenes == null || imagen.Imagenes.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Debe enviar al menos una imagen.");

                var existente = negocio.Listar().Find(x => x.Id == imagen.Id);
                if (existente == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "El artículo especificado no existe.");
                

                foreach (var url in imagen.Imagenes)
                {
                    Imagen nueva = new Imagen
                    {
                        IdArticulo = imagen.Id,
                        ImagenUrl = url
                    };
                    negocio.agregar(nueva);
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Imagen agregada correctamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }


        }
    }
}
