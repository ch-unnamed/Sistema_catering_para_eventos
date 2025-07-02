using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Proporciona métodos para la gestión de roles en la capa de acceso a datos.
    /// </summary>
    public class Rol
    {
        /// <summary>
        /// Instancia de la clase <see cref="Conexion"/> para interactuar con la base de datos.
        /// </summary>
        private readonly Conexion conexion = new Conexion();

        /// <summary>
        /// Obtiene una lista de todos los roles disponibles desde la base de datos.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Rol"/> que representan los roles.</returns>
        public List<BE.Rol> ListarRol()
        {
            Conexion conexion = new Conexion();
            List<BE.Rol> roles = new List<BE.Rol>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_roles");

            foreach (DataRow row in dt.Rows)
            {
                BE.Rol unRol = new BE.Rol
                {
                    IdRol = Convert.ToInt32(row["id"]),
                    NombreRol = row["nombre"].ToString()
                };

                roles.Add(unRol);
            }

            return roles;
        }
    }
}
