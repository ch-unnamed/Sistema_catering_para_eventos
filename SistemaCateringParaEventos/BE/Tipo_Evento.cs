using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa un tipo de evento dentro del sistema.
    /// </summary>
    public class Tipo_Evento
    {
        /// <summary>
        /// Identificador único del tipo de evento.
        /// </summary>
        private int _idTipoEvento;

        /// <summary>
        /// Obtiene o establece el identificador único del tipo de evento.
        /// </summary>
        public int Id_TipoEvento
        {
            get { return _idTipoEvento; }
            set { _idTipoEvento = value; }
        }

        /// <summary>
        /// Nombre descriptivo del tipo de evento.
        /// </summary>
        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre del tipo de evento.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
