using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa la relación entre un descuento y una cotización.
    /// </summary>
    public class Descuento_Cotizacion
    {
        /// <summary>
        /// Identificador único de la relación Descuento-Cotización.
        /// </summary>
        private int _id_descuento_cotizacion;

        /// <summary>
        /// Obtiene o establece el identificador único de la relación Descuento-Cotización.
        /// </summary>
        public int ID_Descuento_Cotizacion
        {
            get { return _id_descuento_cotizacion; }
            set { _id_descuento_cotizacion = value; }
        }

        /// <summary>
        /// Descuento asociado a la cotización.
        /// </summary>
        private Descuento _idDescuento;

        /// <summary>
        /// Obtiene o establece el descuento asociado.
        /// </summary>
        public Descuento IdDescuento
        {
            get { return _idDescuento; }
            set { _idDescuento = value; }
        }

        /// <summary>
        /// Cotización asociada al descuento.
        /// </summary>
        private Cotizacion _idCotizacion;

        /// <summary>
        /// Obtiene o establece la cotización asociada.
        /// </summary>
        public Cotizacion IdCotizacion
        {
            get { return _idCotizacion; }
            set { _idCotizacion = value; }
        }
    }
}