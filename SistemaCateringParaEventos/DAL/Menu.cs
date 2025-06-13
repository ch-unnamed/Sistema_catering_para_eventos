using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Menu
    {
        // Esto es para el vendedor, si lo borran se rompe cotizacion, es para mostrarlo en un select y no hacerlo hardcodeado como bot
        public List<BE.Menu> Listar()
        {
            Conexion conexion = new Conexion();

            List<BE.Menu> menus = new List<BE.Menu>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_menus");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Menu unMenu = new BE.Menu();
                unMenu.Id = Convert.ToInt32(fila["id"]);
                unMenu.Nombre = fila["nombre"].ToString();

                menus.Add(unMenu);
            }

            return menus;
        }

    }
}