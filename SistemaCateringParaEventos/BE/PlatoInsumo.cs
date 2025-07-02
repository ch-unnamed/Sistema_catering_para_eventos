using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa la relación entre un plato y un insumo, indicando la cantidad de insumo utilizada en el plato.
    /// </summary>
    public class PlatoInsumo
    {
        private Plato _plato;

        /// <summary>
        /// Obtiene o establece el plato asociado.
        /// </summary>
        public Plato Plato
        {
            get { return _plato; }
            set { _plato = value; }
        }

        private Insumo _insumo;

        /// <summary>
        /// Obtiene o establece el insumo asociado.
        /// </summary>
        public Insumo Insumo
        {
            get { return _insumo; }
            set { _insumo = value; }
        }

        private int _cantidad;

        /// <summary>
        /// Obtiene o establece la cantidad del insumo utilizada en el plato.
        /// </summary>
        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
    }
}
