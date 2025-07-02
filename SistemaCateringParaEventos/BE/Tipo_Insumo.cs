using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa un tipo de insumo con identificador y nombre.
    /// </summary>
    public class Tipo_Insumo
    {
        private int _id;

        /// <summary>
        /// Obtiene o establece el identificador único del tipo de insumo.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre del tipo de insumo.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
