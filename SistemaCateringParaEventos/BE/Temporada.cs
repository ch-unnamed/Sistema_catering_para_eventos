using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa una temporada, incluyendo su categoría, fechas y cantidad de eventos asociados.
    /// </summary>
    public class Temporada
    {
        private int _idTemporada;

        /// <summary>
        /// Obtiene o establece el identificador único de la temporada.
        /// </summary>
        public int IdTemporada
        {
            get { return _idTemporada; }
            set { _idTemporada = value; }
        }

        private Categoria_Temporada _categoria_Temporada;

        /// <summary>
        /// Obtiene o establece la categoría asociada a la temporada.
        /// </summary>
        public Categoria_Temporada Id_CategoriaTemporada
        {
            get { return _categoria_Temporada; }
            set { _categoria_Temporada = value; }
        }

        private DateTime _fechaComienzo_temp;

        /// <summary>
        /// Obtiene o establece la fecha de comienzo de la temporada.
        /// </summary>
        public DateTime FechaComienzoTemp
        {
            get { return _fechaComienzo_temp; }
            set { _fechaComienzo_temp = value; }
        }

        private int _cantidad_evento;

        /// <summary>
        /// Obtiene o establece la cantidad de eventos asociados a la temporada.
        /// </summary>
        public int CantidadEvento
        {
            get { return _cantidad_evento; }
            set { _cantidad_evento = value; }
        }

        private DateTime _fechaFin;

        /// <summary>
        /// Obtiene o establece la fecha de finalización de la temporada.
        /// </summary>
        public DateTime FechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }
    }
}
