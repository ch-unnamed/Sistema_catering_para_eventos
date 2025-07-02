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
    /// Proporciona métodos para la gestión de usuarios en la capa de acceso a datos.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Instancia de la clase <see cref="Conexion"/> para interactuar con la base de datos.
        /// </summary>
        private readonly Conexion conexion = new Conexion();

        /// <summary>
        /// Obtiene una lista de todos los usuarios desde la base de datos.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Usuario"/> que representan los usuarios.</returns>
        public List<BE.Usuario> ListarUsuario()
        {
            Conexion conexion = new Conexion();
            List<BE.Usuario> usuarios = new List<BE.Usuario>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_usuarios");

            foreach (DataRow row in dt.Rows)
            {
                BE.Usuario unUsuario = new BE.Usuario();

                unUsuario.IdUsuario = Convert.ToInt32(row["id"]);
                unUsuario.Email = row["email"].ToString();
                unUsuario.FechaCreacion = Convert.ToDateTime(row["fechaDeCreacion"]);
                unUsuario.Nombre = row["nombre_usuario"].ToString();
                unUsuario.Apellido = row["apellido"].ToString();

                BE.Rol unRol = new BE.Rol();
                unRol.IdRol = Convert.ToInt32(row["rol_id"]);
                unRol.NombreRol = row["nombre_rol"].ToString();
                unUsuario.RolUsuario = unRol;

                usuarios.Add(unUsuario);
            }

            return usuarios;
        }

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Objeto <see cref="BE.Usuario"/> a insertar.</param>
        /// <returns>Id del usuario insertado.</returns>
        /// <exception cref="Exception">Se lanza si no se puede obtener el ID del usuario insertado.</exception>
        public int InsertarUsuario(BE.Usuario usuario)
        {
            var parametros = new SqlParameter[]
            {
                    conexion.crearParametro("@email", usuario.Email),
                    conexion.crearParametro("@fechaDeCreacion", usuario.FechaCreacion),
                    conexion.crearParametro("@password", usuario.PasswordHash),
                    conexion.crearParametro("@rol_id", usuario.RolUsuario.IdRol),
                    conexion.crearParametro("@nombre", usuario.Nombre),
                    conexion.crearParametro("@apellido", usuario.Apellido),
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_crear_usuario", parametros);

            if (dt.Rows.Count == 0)
                throw new Exception("Error al obtener ID");

            return Convert.ToInt32(dt.Rows[0][0]);
        }

        /// <summary>
        /// Elimina un usuario de la base de datos por su identificador.
        /// </summary>
        /// <param name="id">Id del usuario a eliminar.</param>
        public void EliminarUsuario(int id)
        {
            var parametros = new SqlParameter[]
            {
                    conexion.crearParametro("@id", id)
            };

            conexion.EscribirPorStoreProcedure("sp_eliminar_usuario", parametros);
        }

        /// <summary>
        /// Obtiene un usuario con rol de vendedor según el id de rol.
        /// </summary>
        /// <param name="rol_id">Id del rol.</param>
        /// <returns>Objeto <see cref="BE.Usuario"/> con el id correspondiente, o null si no se encuentra.</returns>
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
