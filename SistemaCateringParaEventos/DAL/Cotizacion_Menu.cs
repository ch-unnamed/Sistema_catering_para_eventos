using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Clase de acceso a datos para la gestión de la relación Cotización-Menú.
    /// </summary>
    public class Cotizacion_Menu
    {
        /// <summary>
        /// Lista todas las relaciones Cotización-Menú obtenidas desde la base de datos.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Cotizacion_Menu"/>.</returns>
        public List<BE.Cotizacion_Menu> Listar()
        {
            Conexion conexion = new Conexion();

            List<BE.Cotizacion_Menu> contizacionMenus = new List<BE.Cotizacion_Menu>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_cotizacion_menu");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Cotizacion_Menu unaContizacionMenus = new BE.Cotizacion_Menu();
                unaContizacionMenus.Id = Convert.ToInt32(fila["id"]);

                BE.Cotizacion unaCotizacion = new BE.Cotizacion();
                unaCotizacion.IdCotizacion = Convert.ToInt32(fila["cotizacion_id"]);
                unaContizacionMenus.Cotizacion = unaCotizacion;

                BE.Menu unMenu = new BE.Menu();
                unMenu.Id = Convert.ToInt32(fila["menu_id"]);
                unaContizacionMenus.Menu = unMenu;

                unaContizacionMenus.Estado = Convert.ToInt32(fila["estado"]);

                contizacionMenus.Add(unaContizacionMenus);
            }

            return contizacionMenus;
        }

        /// <summary>
        /// Edita una relación Cotización-Menú en la base de datos utilizando un procedimiento almacenado.
        /// </summary>
        /// <param name="cotizacionMenu">Objeto <see cref="BE.Cotizacion_Menu"/> con los datos a editar.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>True si la edición fue exitosa, false en caso contrario.</returns>
        public bool EditarCotizacionMenu(BE.Cotizacion_Menu cotizacionMenu, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            SqlParameter[] parametrosSql = new SqlParameter[]
            {
                    new SqlParameter("@Id", cotizacionMenu.Id),
                    new SqlParameter("@Estado", cotizacionMenu.Estado),
                    new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                    new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
            };

            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_editar_cotizacion_menu", parametrosSql);

            mensaje = parametrosSql[2].Value.ToString();
            resultado = Convert.ToBoolean(parametrosSql[3].Value);

            return resultado;
        }
    }
}