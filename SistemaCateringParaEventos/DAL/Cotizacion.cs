using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Cotizacion
    {
        public List<BE.Cotizacion> Listar()
        {
            Conexion conexion = new Conexion();

            List<BE.Cotizacion> cotizaciones = new List<BE.Cotizacion>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_cotizacion");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Cotizacion unaCotizacion = new BE.Cotizacion();

                // tomar cada fila como se usa en el procedimiento almacenado

                unaCotizacion.IdCotizacion = Convert.ToInt32(fila["cotizacion_id"]);
                unaCotizacion.FechaPedido = Convert.ToDateTime(fila["fecha_pedido"]);
                unaCotizacion.Total = Convert.ToInt32(fila["total"]);
                unaCotizacion.FechaRealizacion = Convert.ToDateTime(fila["fecha_realizacion"]);

                // Crear instancia de cada clase individual para obtener el campo requerido

                BE.Evento evento_nombre = new BE.Evento();
                evento_nombre.Nombre = fila["evento_nombre"].ToString();
                unaCotizacion.Evento = evento_nombre;

                BE.Cliente cliente_dni = new BE.Cliente();
                cliente_dni.Dni = Convert.ToInt32(fila["cliente_dni"]);
                unaCotizacion.Cliente = cliente_dni;

                BE.Usuario usuario_id = new BE.Usuario();
                usuario_id.IdUsuario = Convert.ToInt32(fila["usuario_id"]);
                unaCotizacion.Vendedor = usuario_id;

                BE.Estado estado_nombre = new BE.Estado();
                estado_nombre.Nombre = fila["estado_nombre"].ToString();
                unaCotizacion.Estado = estado_nombre;

                cotizaciones.Add(unaCotizacion);
            }

            return cotizaciones;
        }

        public bool EditarCotizacion(BE.Cotizacion cotizacion, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@cotizacion_id", cotizacion.IdCotizacion),
                new SqlParameter("@estado_id", cotizacion.Estado.IdEstado),
                new SqlParameter("@fecha_realizacion", cotizacion.FechaRealizacion),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
            };

            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_editar_cotizacion", parametros);

            mensaje = parametros[3].Value.ToString();
            resultado = Convert.ToBoolean(parametros[4].Value);

            return resultado;
        }

        public bool EliminarCotizacion(int cotizacion_id, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdCotizacion", cotizacion_id),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
            };

            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_eliminar_cotizacion", parametros);

            mensaje = parametros[1].Value.ToString();
            resultado = Convert.ToBoolean(parametros[2].Value);

            return resultado;
        }

        public string ObtenerMailCliente(int cliente_id)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@clienteID", cliente_id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_mail_cliente", parametros);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0]["email"]);
            }
            return "";
        }
    }
}
