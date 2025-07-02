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
    /// <summary>
    /// Proporciona métodos de negocio para la gestión de usuarios.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Instancia de la capa de acceso a datos para usuarios.
        /// </summary>
        private readonly DAL.Usuario oUsuarioDAL;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Usuario"/>.
        /// </summary>
        public Usuario()
        {
            oUsuarioDAL = new DAL.Usuario();
        }

        /// <summary>
        /// Obtiene una lista de todos los usuarios.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Usuario"/>.</returns>
        public List<BE.Usuario> ListarUsuario()
        {
            DAL.Usuario usuarioDAL = new DAL.Usuario();
            return usuarioDAL.ListarUsuario();
        }

        /// <summary>
        /// Valida si el email proporcionado tiene un formato correcto.
        /// </summary>
        /// <param name="email">Email a validar.</param>
        /// <returns>True si el email es válido, false en caso contrario.</returns>
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

        /// <summary>
        /// Valida los datos de un usuario.
        /// </summary>
        /// <param name="usuario">Usuario a validar.</param>
        /// <returns>Diccionario con los errores encontrados, si los hay.</returns>
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

        /// <summary>
        /// Crea un nuevo usuario si los datos son válidos.
        /// </summary>
        /// <param name="usuario">Usuario a crear.</param>
        /// <param name="errores">Diccionario de errores de validación.</param>
        /// <returns>Id del usuario creado, o 0 si hay errores.</returns>
        public int CrearUsuario(BE.Usuario usuario, out Dictionary<string, string> errores)
        {
            DAL.Usuario userDAL = new DAL.Usuario();
            errores = ValidarUsuario(usuario);

            if (errores.Count == 0)
                return userDAL.InsertarUsuario(usuario);
            else
                return 0;
        }

        /// <summary>
        /// Elimina un usuario por su identificador.
        /// </summary>
        /// <param name="Id">Id del usuario a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false si hubo error.</returns>
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

        /// <summary>
        /// Obtiene un usuario con rol de vendedor según el id de rol.
        /// </summary>
        /// <param name="rol_id">Id del rol.</param>
        /// <returns>Usuario con el rol especificado.</returns>
        public BE.Usuario idVendedor(int rol_id)
        {
            DAL.Usuario usuario = new DAL.Usuario();
            return usuario.idVendedor(rol_id);
        }
    }
}