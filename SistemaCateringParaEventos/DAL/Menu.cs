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
        /// <summary>
        /// Lista los menús disponibles para el vendedor.
        /// </summary>
        /// <returns>Lista de menús para el vendedor.</returns>
        public List<BE.Menu> ListarMenusVendedor()
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

        /// <summary>
        /// Lista todos los menús disponibles para el chef.
        /// </summary>
        /// <returns>Lista de todos los menús con detalles.</returns>
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

        /// <summary>
        /// Crea un nuevo menú después de validar los datos proporcionados.
        /// </summary>
        /// <param name="menu">El menú a crear.</param>
        /// <param name="mensaje">Mensaje de validación o resultado.</param>
        /// <returns>El identificador del menú creado, o 0 si hay error de validación.</returns>
        public int CrearMenu(BE.Menu menu, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

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

        /// <summary>
        /// Edita un menú existente después de validar los datos proporcionados.
        /// </summary>
        /// <param name="menu">El menú a editar.</param>
        /// <param name="mensaje">Mensaje de validación o resultado.</param>
        /// <returns>El identificador del menú editado, o 0 si hay error de validación.</returns>
        public int EditarMenu(BE.Menu menu, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

            string idsPlatos = string.Join(",", menu.Platos.Select(p => p.Id));

            SqlParameter[] parametros = new SqlParameter[]
            {
            new SqlParameter("@id_menu", menu.Id),
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

        /// <summary>
        /// Elimina un menú por su identificador.
        /// </summary>
        /// <param name="idMenu">Identificador del menú a eliminar.</param>
        /// <param name="mensaje">Mensaje de resultado.</param>
        /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
        public bool EliminarMenu(int idMenu, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                SqlParameter[] parametrosSql = new SqlParameter[]
                {
                    new SqlParameter("@Id", idMenu),
                    new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                    new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
                };

                int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_eliminar_menu", parametrosSql);

                mensaje = parametrosSql[1].Value.ToString();
                resultado = Convert.ToBoolean(parametrosSql[2].Value);

                return resultado;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    mensaje = "¡El Menu tiene vinculos activos con Cotizacion!";
                }
                else
                {
                    mensaje = "Error en la base de datos: " + ex.Message;
                }
                return false;
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado" + ex.Message;
                return false;
            }
        }
    }
}