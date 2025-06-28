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
    public class Usuario
    {
        /// <summary>
        /// Obtiene una lista de todos los usuarios desde la base de datos.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Usuario"/>.</returns>
        public List<BE.Usuario> ListarUsuario()
        {
            Conexion conexion = new Conexion();

            List<BE.Usuario> usuarios = new List<BE.Usuario>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_usuarios");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Usuario unUsuario = new BE.Usuario();

                unUsuario.IdUsuario = Convert.ToInt32(fila["id"]);
                unUsuario.Email = fila["email"].ToString();
                unUsuario.FechaCreacion = Convert.ToDateTime(fila["fechaDeCreacion"]);
                unUsuario.Nombre = fila["nombre"].ToString();
                unUsuario.Apellido = fila["apellido"].ToString();

                BE.Rol unRol = new BE.Rol();
                unRol.IdRol = Convert.ToInt32(fila["id"]);
                unRol.Nombre = fila["nombre"].ToString();
                unUsuario.RolUsuario = unRol;

                usuarios.Add(unUsuario);
            }

            return usuarios;
        }
    
        /// <summary>
        /// Obtiene el usuario correspondiente al rol de vendedor según el ID de rol proporcionado.
        /// </summary>
        /// <param name="rol_id">ID del rol a buscar.</param>
        /// <returns>Objeto <see cref="BE.Usuario"/> correspondiente al vendedor, o null si no se encuentra.</returns>
        public BE.Usuario idVendedor(int rol_id)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@rol_id", rol_id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_id_usuario", parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow fila = dt.Rows[0];

                return new BE.Usuario
                {
                    IdUsuario = Convert.ToInt32(fila["id"])
                };
            }

            return null;
        }
    }
}
