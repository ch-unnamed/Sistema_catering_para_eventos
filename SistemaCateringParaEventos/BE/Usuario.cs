using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa un usuario dentro del sistema.
    /// </summary>
    public class Usuario
    {
        private int _IdUsuario;

        /// <summary>
        /// Obtiene o establece el identificador único del usuario.
        /// </summary>
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        private string _email;

        /// <summary>
        /// Obtiene o establece el correo electrónico del usuario.
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _passwordHash;

        /// <summary>
        /// Obtiene o establece el hash de la contraseña del usuario.
        /// </summary>
        public string PasswordHash
        {
            get { return _passwordHash; }
            set { _passwordHash = value; }
        }

        private DateTime _fechaCreacion;

        /// <summary>
        /// Obtiene o establece la fecha de creación del usuario.
        /// </summary>
        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }

        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre del usuario.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;

        /// <summary>
        /// Obtiene o establece el apellido del usuario.
        /// </summary>
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private Rol _rol;

        /// <summary>
        /// Obtiene o establece el rol asignado al usuario.
        /// </summary>
        public Rol RolUsuario
        {
            get { return _rol; }
            set { _rol = value; }
        }
    }
}
