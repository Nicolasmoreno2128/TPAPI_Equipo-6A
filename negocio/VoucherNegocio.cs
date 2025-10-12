using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using negocio;

namespace negocio
{
    public class VoucherNegocio
    {
       public List<Voucher> Listar()
        {
            List<Voucher> lista = new List<Voucher>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT CodigoVoucher, IdCliente, FechaCanje, IdArticulo FROM Vouchers");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Voucher aux = new Voucher();
                    aux.CodigoVoucher = datos.Lector["CodigoVoucher"].ToString();
                    aux.IdCliente = datos.Lector["IdCliente"] != DBNull.Value ? (int?)Convert.ToInt32(datos.Lector["IdCliente"]) : null;
                    aux.FechaCanje = datos.Lector["FechaCanje"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(datos.Lector["FechaCanje"]) : null;
                    aux.IdArticulo = datos.Lector["IdArticulo"] != DBNull.Value ? (int?)Convert.ToInt32(datos.Lector["IdArticulo"]) : null;

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void MarcarCanjePorCodigo(string codigoVoucher, int idCliente, int idArticulo)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
            UPDATE Vouchers
               SET IdCliente  = @idCliente,
                   FechaCanje = @fecha,
                   IdArticulo = @idArticulo
             WHERE CodigoVoucher = @codigo
               AND (FechaCanje IS NULL)");   // evita re-canjear

                datos.setearParametro("@idCliente", idCliente);
                datos.setearParametro("@fecha", DateTime.Now);
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearParametro("@codigo", codigoVoucher);

                datos.ejecutarAccion();
            }
            catch (Exception) { throw; }
            finally { datos.cerrarConexion(); }
        }
    }
}


