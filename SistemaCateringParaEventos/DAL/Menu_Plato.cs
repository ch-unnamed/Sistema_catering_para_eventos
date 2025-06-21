using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Menu_Plato
    {
        public List<BE.Menu_Plato> ObtenerNombresPlatosPorMenu(BE.Menu_Plato menu_plato)
        {
            Conexion conexion = new Conexion();

            // guardo en una lista los platos que hay en un menu
            List<BE.Menu_Plato> platosPorMenuId = new List<BE.Menu_Plato>();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@menu_id", menu_plato.Menu.Id) // por parametro va el id de el menu
            };

            DataTable dt = conexion.LeerPorStoreProcedure("ObtenerNombresPlatosPorMenu", parametros);

            foreach (DataRow fila in dt.Rows)
            {
                BE.Menu_Plato unMenuPlato = new BE.Menu_Plato();
                unMenuPlato.Menu = new BE.Menu();
                unMenuPlato.Plato = new BE.Plato();

                unMenuPlato.Menu.Id = Convert.ToInt32(fila["menu_id"]);
                unMenuPlato.Plato.Id = Convert.ToInt32(fila["id"]);
                unMenuPlato.Plato.Nombre = fila["Nombre"].ToString(); 
                unMenuPlato.Plato.Descripcon = fila["descripcion"].ToString(); 
                unMenuPlato.Plato.Precio = Convert.ToDecimal(fila["precio"]);

                platosPorMenuId.Add(unMenuPlato);

            }

            return platosPorMenuId;
        }
    }
}