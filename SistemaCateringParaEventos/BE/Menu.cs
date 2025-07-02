using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa un menú que contiene una lista de platos, junto con información relevante como nombre, descripción y fecha de creación.
    /// </summary>
    public class Menu
    {
        private int _id;

        /// <summary>
        /// Obtiene o establece el identificador único del menú.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre del menú.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _descripcion;

        /// <summary>
        /// Obtiene o establece la descripción del menú.
        /// </summary>
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private DateTime _fechaDeCreacion;

        /// <summary>
        /// Obtiene o establece la fecha de creación del menú.
        /// </summary>
        public DateTime FechaDeCreacion
        {
            get { return _fechaDeCreacion; }
            set { _fechaDeCreacion = value; }
        }

        private List<Plato> _platos;

        /// <summary>
        /// Obtiene o establece la lista de platos que conforman el menú.
        /// </summary>
        public List<Plato> Platos
        {
            get { return _platos; }
            set { _platos = value; }
        }
    }
}
