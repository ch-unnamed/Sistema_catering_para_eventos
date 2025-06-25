using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

        public List<BE.Menu> ListarMenus()
        {
            Conexion conexion = new Conexion();

            List<BE.Menu> menus = new List<BE.Menu>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_menus_chef");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Menu unMenu = new BE.Menu();
                unMenu.Id = Convert.ToInt32(fila["id"]);
                unMenu.Nombre = fila["nombre"].ToString();
                unMenu.Descripcion = fila["descripcion"].ToString();
                unMenu.FechaDeCreacion = Convert.ToDateTime(fila["fechaDeCreacion"]);

                menus.Add(unMenu);
            }

            return menus;
        }

        public int CrearMenu(BE.Menu menu, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

            // para leer en el store procedure la cadena de ids e insertar en la tabla MENU_PLATO
            string idsPlatos = string.Join(",", menu.Platos.Select(p => p.Id));

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", menu.Nombre),
                new SqlParameter("@descripcion", menu.Descripcion),
                new SqlParameter("@fecha_creacion", DateTime.Now),
                new SqlParameter("@platos_ids", idsPlatos),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            string nombreLimpio = menu.Nombre.Trim();
            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_crear_menu", parametros);

            mensaje = parametros[4].Value.ToString();
            resultado = Convert.ToInt32(parametros[5].Value);

            return resultado;
        }

        public int EditarMenu(BE.Menu menu, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

            string idsPlatos = string.Join(",", menu.Platos.Select(p => p.Id));

            SqlParameter[] parametros = new SqlParameter[]
            {
            new SqlParameter("@id", menu.Id),
            new SqlParameter("@nombre", menu.Nombre),
            new SqlParameter("@descripcion", menu.Descripcion),
            new SqlParameter("@platos_ids", idsPlatos),
            new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
            new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_editar_menu", parametros);

            mensaje = parametros[4].Value.ToString();
            resultado = Convert.ToInt32(parametros[5].Value);

            return resultado;
        }

    }
}