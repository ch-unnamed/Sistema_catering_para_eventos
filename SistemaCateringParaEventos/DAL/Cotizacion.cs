using System;
using System.Collections.Generic;
using System.Data;
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

                BE.Menu menu_nombre= new BE.Menu();
                menu_nombre.Nombre = fila["menu_nombre"].ToString();
                unaCotizacion.Menu = menu_nombre;

                cotizaciones.Add(unaCotizacion);
            }

            return cotizaciones;
        }

        public void GenerarDescuento()
        {
            // Logica de generar descuento
        }
    }
}
