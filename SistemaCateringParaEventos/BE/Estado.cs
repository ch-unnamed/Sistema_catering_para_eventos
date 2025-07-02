using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{   
/ <summary>
    /// Representa un estado con un identificador y un nombre restringido a ciertos valores.
    /// </summary>
    public class Estado
    {
        private int _idEstado;

        /// <summary>
        /// Obtiene o establece el identificador único del estado.
        /// </summary>
        public int IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre del estado.
        /// Solo se permiten los valores: "Pendiente", "Confirmado", "Rechazado" o "Completado".
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Se lanza si el valor asignado no es uno de los valores permitidos.
        /// </exception>
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (value == "Pendiente" || value == "Confirmado" ||
                    value == "Rechazado" || value == "Completado")
                {
                    _nombre = value;
                }
                else
                {
                    throw new ArgumentException("El estado debe ser 'Pendiente', 'Confirmado', 'Rechazado' o 'Completado'.");
                }
            }
        }
    }
}
