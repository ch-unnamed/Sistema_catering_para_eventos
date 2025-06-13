using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Usuario
    {
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
        /*
        public bool EditarUsuario(string nombreUsuario, string apellidoUsuario, string emailUsuario, Rol rolUsuario)
        {

        }
        public bool EliminarUsuario(int IDUsuario)
        {

        }
        public bool CrearUsuario(string nombreUsuario, string apellidoUsuario, string emailUsuario, string passwordHash, Rol rolUsuario)
        {
        }*/
    }
}
