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
        private readonly Conexion conexion = new Conexion();


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

        public void EliminarUsuario(int id)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@id", id)
            };

            conexion.EscribirPorStoreProcedure("sp_eliminar_usuario", parametros);
        }


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
