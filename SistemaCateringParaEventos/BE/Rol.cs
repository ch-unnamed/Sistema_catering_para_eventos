using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa un rol dentro del sistema.
    /// </summary>
    public class Rol
    {
        private int _idRol;

        /// <summary>
        /// Obtiene o establece el identificador único del rol.
        /// </summary>
        public int IdRol
        {
            get { return _idRol; }
            set { _idRol = value; }
        }

        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre del rol.
        /// </summary>
        public string NombreRol
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}