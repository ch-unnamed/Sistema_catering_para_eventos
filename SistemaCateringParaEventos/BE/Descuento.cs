using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa un descuento aplicable, incluyendo su identificador, nombre, descripción y porcentaje.
    /// </summary>
    public class Descuento
    {
        /// <summary>
        /// Identificador único del descuento.
        /// </summary>
        private int _idDescuento;

        /// <summary>
        /// Obtiene o establece el identificador único del descuento.
        /// </summary>
        public int IdDescuento
        {
            get { return _idDescuento; }
            set { _idDescuento = value; }
        }

        /// <summary>
        /// Nombre del descuento.
        /// </summary>
        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre del descuento.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        /// <summary>
        /// Descripción del descuento.
        /// </summary>
        private string descripcion;

        /// <summary>
        /// Obtiene o establece la descripción del descuento.
        /// </summary>
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        /// <summary>
        /// Porcentaje de descuento aplicado.
        /// </summary>
        private decimal _porcentaje;

        /// <summary>
        /// Obtiene o establece el porcentaje de descuento aplicado.
        /// </summary>
        public decimal Porcentaje
        {
            get { return _porcentaje; }
            set { _porcentaje = value; }
        }
    }
}