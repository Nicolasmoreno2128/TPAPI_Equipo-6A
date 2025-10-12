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
        public IEnumerable<Articulo> Get()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            return negocio.listar();
        }

        // GET: api/Articulo/5
        public Articulo Get(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> lista = negocio.listar();
            return lista.Find(x=> x.Id == id);
        }

        // POST: api/Articulo
        public void Post([FromBody]ArticuloDto articulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevo = new Articulo();

            nuevo.Codigo = articulo.Codigo;
            nuevo.Nombre = articulo.Nombre;
            nuevo.Descripcion = articulo.Descripcion;
            nuevo.Precio = articulo.Precio;
            nuevo.Categoria = new Categoria { Id = articulo.IdCategoria };
            nuevo.Marca = new Marca { Id = articulo.IdMarca };

            negocio.agregar(nuevo);
        }

        // PUT: api/Articulo/5
        public void Put(int id, [FromBody]ArticuloDto articulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo modificar = new Articulo();

            modificar.Codigo = articulo.Codigo;
            modificar.Nombre = articulo.Nombre;
            modificar.Descripcion = articulo.Descripcion;
            modificar.Precio = articulo.Precio;
            modificar.Categoria = new Categoria { Id = articulo.IdCategoria };
            modificar.Marca = new Marca { Id = articulo.IdMarca };
            modificar.Id = id;

            negocio.Modificar(modificar);
        }

        // DELETE: api/Articulo/5
        public void Delete(int id)
        {
        }
    }
}
