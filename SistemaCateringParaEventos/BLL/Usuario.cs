using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class Usuario
    {
        private readonly DAL.Usuario oUsuarioDAL;

        public Usuario()
        {
            oUsuarioDAL = new DAL.Usuario();
        }

        public List<BE.Usuario> ListarUsuario()
        {
            DAL.Usuario usuarioDAL = new DAL.Usuario();
            return usuarioDAL.ListarUsuario();
        }

        bool EmailValido(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public Dictionary<string, string> ValidarUsuario(BE.Usuario usuario)
        {
            var errores = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                errores["nombre"] = "El usuario debe tener un nombre";

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
                errores["apellido"] = "El usuario debe tener un apellido";

            if (string.IsNullOrWhiteSpace(usuario.Email) || !EmailValido(usuario.Email))
                errores["email"] = "El usuario debe tener un email valido";

            if (string.IsNullOrWhiteSpace(usuario.PasswordHash) || usuario.PasswordHash.Length < 6 || usuario.PasswordHash.Length > 15)
                errores["password"] = "El usuario debe tener una contraseña de al menos 6 caracteres";

            if (usuario.RolUsuario.IdRol == 0)
                errores["rol"] = "El usuaruo debe tener un rol";

            return errores;
        }

        public int CrearUsuario(BE.Usuario usuario, out Dictionary<string, string> errores)
        {
            DAL.Usuario userDAL = new DAL.Usuario();
            errores = ValidarUsuario(usuario);

            if (errores.Count == 0)
                return userDAL.InsertarUsuario(usuario);
            else
                return 0;
        }


        public bool EliminarUsuario(int Id)
        {
            DAL.Usuario usuarioDAL = new DAL.Usuario();
            try
            {
                usuarioDAL.EliminarUsuario(Id);
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error SQL: " + ex.Message);
                return false;
            }
        }


        public BE.Usuario idVendedor(int rol_id)
        {
            DAL.Usuario usuario = new DAL.Usuario();

            return usuario.idVendedor(rol_id);
        }

    }
}