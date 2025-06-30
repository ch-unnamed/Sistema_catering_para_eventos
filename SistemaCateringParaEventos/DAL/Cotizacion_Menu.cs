using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Cotizacion_Menu
    {
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
                unMenu.Id = Convert.ToInt32(fila["id"]);
                unaContizacionMenus.Menu = unMenu;


                unaContizacionMenus.Estado = Convert.ToInt32(fila["estado"]);

                contizacionMenus.Add(unaContizacionMenus);
            }

            return contizacionMenus;
        }

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