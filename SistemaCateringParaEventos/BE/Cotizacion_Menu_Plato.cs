using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa la relación entre una cotización de menú y un plato específico.
    /// </summary>
    public class Cotizacion_Menu_Plato
    {
        private int _id;

        /// <summary>
        /// Obtiene o establece el identificador único de la relación Cotizacion_Menu_Plato.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private Cotizacion_Menu _cotizacion_menu;

        /// <summary>
        /// Obtiene o establece la cotización de menú asociada.
        /// </summary>
        public Cotizacion_Menu CotizacionMenu
        {
            get { return _cotizacion_menu; }
            set { _cotizacion_menu = value; }
        }

        private Plato _plato;

        /// <summary>
        /// Obtiene o establece el plato asociado a la cotización de menú.
        /// </summary>
        public Plato Plato
        {
            get { return _plato; }
            set { _plato = value; }
        }
    }
}