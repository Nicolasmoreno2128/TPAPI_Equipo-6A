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
    public class ArticuloController : ApiController
    {
        // GET: api/Articulo
        public HttpResponseMessage Get()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                var lista = negocio.listar();

                if (lista == null || lista.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No hay artículos cargados.");

                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al obtener los artículos: " + ex.Message);
            }
        }

        // GET: api/Articulo/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El ID debe ser mayor a cero.");

                ArticuloNegocio negocio = new ArticuloNegocio();
                var articulo = negocio.listar().Find(x => x.Id == id);

                if (articulo == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"No se encontró un artículo con ID {id}.");

                return Request.CreateResponse(HttpStatusCode.OK, articulo);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al obtener el artículo: " + ex.Message);
            }
        }

        // POST: api/Articulo
        public HttpResponseMessage Post([FromBody]ArticuloDto articulo)
        {

            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                var marcaNegocio = new MarcaNegocio();
                var categoriaNegocio = new CategoriaNegocio();

                Marca marca = marcaNegocio.Listar().Find(x => x.Id == articulo.IdMarca);
                Categoria categoria = categoriaNegocio.Listar().Find(x => x.Id == articulo.IdCategoria);

                if (marca == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La marca no existe.");

                if (categoria == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoría no existe.");

                Articulo nuevo = new Articulo();

                nuevo.Codigo = articulo.Codigo;
                nuevo.Nombre = articulo.Nombre;
                nuevo.Descripcion = articulo.Descripcion;
                nuevo.Precio = articulo.Precio;
                nuevo.Categoria = new Categoria { Id = articulo.IdCategoria };
                nuevo.Marca = new Marca { Id = articulo.IdMarca };

                negocio.agregar(nuevo);

                return Request.CreateResponse(HttpStatusCode.OK, "Artículo agregado correctamente.");

            }
            catch (Exception )
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");


            }

        }

        // PUT: api/Articulo/5
        public HttpResponseMessage Put(int id, [FromBody]ArticuloDto articulo)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El ID debe ser mayor a cero.");

                ArticuloNegocio negocio = new ArticuloNegocio();
                var existente = negocio.listar().Find(x => x.Id == id);
                if (existente == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No se encontró el artículo a modificar.");

                var marcaNegocio = new MarcaNegocio();
                var categoriaNegocio = new CategoriaNegocio();

                Marca marca = marcaNegocio.Listar().Find(x => x.Id == articulo.IdMarca);
                Categoria categoria = categoriaNegocio.Listar().Find(x => x.Id == articulo.IdCategoria);

                if (marca == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La marca no existe.");

                if (categoria == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoría no existe.");

                Articulo modificar = new Articulo();


                modificar.Codigo = articulo.Codigo;
                modificar.Nombre = articulo.Nombre;
                modificar.Descripcion = articulo.Descripcion;
                modificar.Precio = articulo.Precio;
                modificar.Categoria = new Categoria { Id = articulo.IdCategoria };
                modificar.Marca = new Marca { Id = articulo.IdMarca };
                modificar.Id = id;

                negocio.Modificar(modificar);
                return Request.CreateResponse(HttpStatusCode.OK, "Articulo modificado correctamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }
        }

        // DELETE: api/Articulo/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El ID debe ser mayor a cero.");

                ArticuloNegocio negocio = new ArticuloNegocio();
                var existente = negocio.listar().Find(x => x.Id == id);

                if (existente == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No se encontró el artículo a eliminar.");

                negocio.eliminarRegistro(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Artículo eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al eliminar el artículo: " + ex.Message);
            }
        }
    }
}
