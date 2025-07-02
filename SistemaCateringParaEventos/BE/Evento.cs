using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    /// <summary>
    /// Representa un evento con información relevante como nombre, capacidad, ubicación, tipo, temporada y fecha.
    /// </summary>
    public class Evento
    {
        private int _idEvento;

        /// <summary>
        /// Obtiene o establece el identificador único del evento.
        /// </summary>
        public int IdEvento
        {
            get { return _idEvento; }
            set { _idEvento = value; }
        }

        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre del evento.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private int _capacidad;

        /// <summary>
        /// Obtiene o establece la capacidad máxima de asistentes al evento.
        /// Debe ser mayor a 0.
        /// </summary>
        /// <exception cref="ArgumentException">Se lanza si el valor es menor o igual a 0.</exception>
        public int Capacidad
        {
            get { return _capacidad; }
            set
            {
                if (value > 0)
                {
                    _capacidad = value;
                }
                else
                {
                    throw new ArgumentException("La capacidad debe ser mayor a 0.");
                }
            }
        }

        private Ubicacion _ubicacion;

        /// <summary>
        /// Obtiene o establece la ubicación donde se realiza el evento.
        /// </summary>
        public Ubicacion Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        private Tipo_Evento _tipo_evento;

        /// <summary>
        /// Obtiene o establece el tipo de evento.
        /// </summary>
        public Tipo_Evento Tipo_Evento
        {
            get { return _tipo_evento; }
            set { _tipo_evento = value; }
        }

        private Temporada _temporada;

        /// <summary>
        /// Obtiene o establece la temporada a la que pertenece el evento.
        /// </summary>
        public Temporada Temporada
        {
            get { return _temporada; }
            set { _temporada = value; }
        }

        private DateTime _fecha;

        /// <summary>
        /// Obtiene o establece la fecha en la que se realiza el evento.
        /// </summary>
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
    }
}