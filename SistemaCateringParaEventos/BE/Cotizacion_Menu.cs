using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa la relación entre una cotización y un menú, incluyendo su estado.
    /// </summary>
    public class Cotizacion_Menu
    {
        private int id;

        /// <summary>
        /// Obtiene o establece el identificador único de la relación Cotizacion_Menu.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private Cotizacion _cotizacion;

        /// <summary>
        /// Obtiene o establece la cotización asociada.
        /// </summary>
        public Cotizacion Cotizacion
        {
            get { return _cotizacion; }
            set { _cotizacion = value; }
        }

        private Menu _menu;

        /// <summary>
        /// Obtiene o establece el menú asociado.
        /// </summary>
        public Menu Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }

        private int _estado;

        /// <summary>
        /// Obtiene o establece el estado de la relación Cotizacion_Menu.
        /// </summary>
        public int Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
    }
}