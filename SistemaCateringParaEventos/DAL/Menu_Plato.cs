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
        /// <summary>
        /// Obtiene los nombres, descripciones y precios de los platos asociados a un menú específico.
        /// </summary>
        /// <param name="menu_plato">Objeto <see cref="BE.Menu_Plato"/> que contiene el identificador del menú.</param>
        /// <returns>Lista de objetos <see cref="BE.Menu_Plato"/> con los datos de los platos asociados al menú.</returns>
        public List<BE.Menu_Plato> ObtenerNombresPlatosPorMenu(BE.Menu_Plato menu_plato)
        {
            Conexion conexion = new Conexion();

            List<BE.Menu_Plato> platosPorMenuId = new List<BE.Menu_Plato>();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@menu_id", menu_plato.Menu.Id)
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
                unMenuPlato.Plato.Descripcion = fila["descripcion"].ToString();
                unMenuPlato.Plato.Precio = Convert.ToDecimal(fila["precio"]);

                platosPorMenuId.Add(unMenuPlato);
            }

            return platosPorMenuId;
        }

        /// <summary>
        /// Obtiene la lista de platos (con nombre, descripción y precio) asociados a un menú específico.
        /// </summary>
        /// <param name="menu_plato">Objeto <see cref="BE.Menu_Plato"/> que contiene el identificador del menú.</param>
        /// <returns>Lista de objetos <see cref="BE.Menu_Plato"/> con los datos de los platos asociados al menú.</returns>
        public List<BE.Menu_Plato> ObtenerPlatosDelMenu(BE.Menu_Plato menu_plato)
        {
            Conexion conexion = new Conexion();

            List<BE.Menu_Plato> platosPorMenuId = new List<BE.Menu_Plato>();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@menu_id", menu_plato.Menu.Id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_ObtenerPlatosDelMenu", parametros);

            foreach (DataRow fila in dt.Rows)
            {
                BE.Menu_Plato unMenuPlato = new BE.Menu_Plato();
                unMenuPlato.Menu = new BE.Menu();
                unMenuPlato.Plato = new BE.Plato();

                unMenuPlato.Menu.Id = Convert.ToInt32(fila["menu_id"]);
                unMenuPlato.Plato.Id = Convert.ToInt32(fila["id"]);
                unMenuPlato.Plato.Nombre = fila["Nombre"].ToString();
                unMenuPlato.Plato.Precio = Convert.ToDecimal(fila["precio"]);
                unMenuPlato.Plato.Descripcion = fila["descripcion"].ToString();

                platosPorMenuId.Add(unMenuPlato);
            }

            return platosPorMenuId;
        }
    }
}