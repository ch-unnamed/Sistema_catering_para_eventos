using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa la relación entre un menú y un plato, permitiendo asociar platos a un menú específico.
    /// </summary>
    public class Menu_Plato
    {
        private Menu _menu;

        /// <summary>
        /// Obtiene o establece el menú asociado.
        /// </summary>
        public Menu Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }

        private Plato _plato;

        /// <summary>
        /// Obtiene o establece el plato asociado.
        /// </summary>
        public Plato Plato
        {
            get { return _plato; }
            set { _plato = value; }
        }

        private List<int> _platos;

        /// <summary>
        /// Obtiene o establece la lista de identificadores de platos asociados al menú.
        /// </summary>
        public List<int> Platos
        {
            get { return _platos; }
            set { _platos = value; }
        }
    }
}
