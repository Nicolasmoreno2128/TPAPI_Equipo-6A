using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, Codigo, Nombre, A.Descripcion, Precio, M.Descripcion Marca, C.Descripcion Categoria,a.IdMarca,a.IdCategoria FROM ARTICULOS A, MARCAS M, CATEGORIAS C WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    aux.Precio = (decimal)datos.Lector["precio"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"]; //Se agrega linea para traer el id.Marca (Modificar art)
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];//Se agrega linea para traer el id.Categoria(modificar art)
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }
            return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }

        }
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
 
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca) " + "VALUES ('" + nuevo.Codigo + "', '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "', " + nuevo.Precio + ", " + nuevo.Categoria.Id + ", " + nuevo.Marca.Id + ")");
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarRegistro(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Articulo> Filtrar(string campo, string criterio, int filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            string consulta = "SELECT A.Id, Codigo, Nombre, A.Descripcion, Precio, M.Descripcion Marca, C.Descripcion Categoria FROM ARTICULOS A, MARCAS M, CATEGORIAS C WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id AND ";

            if (campo == "Id")
            {
                switch (criterio)
                {
                    case "Mayor a":
                        consulta += "A.Id >" + filtro;
                        break;
                    case "Menor a":
                        consulta += "A.Id <" + filtro;
                        break;
                    case "Igual a":
                        consulta += "A.Id =" + filtro;
                        break;
                }
            }
            else
            {
                switch (criterio)
                {
                    case "Mayor a":
                        consulta += "Precio > " + filtro;
                        break;
                    case "Menor a":
                        consulta += "Precio < " + filtro;
                        break;
                    case "Igual a":
                        consulta += "Precio = " + filtro;
                        break;
                }


            }
            datos.setearConsulta(consulta);
            datos.ejecutarLectura();

            while (datos.Lector.Read())
            {
                Articulo aux = new Articulo();
                aux.Id = (int)datos.Lector["Id"];
                aux.Codigo = (string)datos.Lector["Codigo"];
                aux.Nombre = (string)datos.Lector["Nombre"];
                aux.Descripcion = (string)datos.Lector["descripcion"];
                aux.Precio = (decimal)datos.Lector["precio"];
                aux.Marca = new Marca();
                aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                aux.Categoria = new Categoria();

                aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                lista.Add(aux);
            }
            return lista;

        }

        public void Modificar(Articulo modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("update ARTICULOS set Codigo = @codigo ,Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idmarca, IdCategoria=@idcategoria, Precio= @precio Where id = @id");
                datos.setearParametro("@codigo", modificado.Codigo);
                datos.setearParametro("@nombre", modificado.Nombre);
                datos.setearParametro("@descripcion", modificado.Descripcion);
                datos.setearParametro("@idmarca", modificado.Marca.Id);
                datos.setearParametro("@idcategoria", modificado.Categoria.Id);
                datos.setearParametro("@precio", modificado.Precio);
                datos.setearParametro("@id", modificado.Id);
                
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



    }
    }

