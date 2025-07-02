using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa un tipo de cliente en el sistema.
    /// </summary>
    public class Tipo_Cliente
    {
        /// <summary>
        /// Identificador único del tipo de cliente.
        /// </summary>
        private int _id_tipo_cliente;

        /// <summary>
        /// Obtiene o establece el identificador único del tipo de cliente.
        /// </summary>
        public int Id_Tipo_Cliente
        {
            get { return _id_tipo_cliente; }
            set { _id_tipo_cliente = value; }
        }

        /// <summary>
        /// Nombre descriptivo del tipo de cliente.
        /// </summary>
        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre del tipo de cliente.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}