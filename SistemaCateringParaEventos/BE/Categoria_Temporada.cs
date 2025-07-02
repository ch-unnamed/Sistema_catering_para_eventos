using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa la relación entre una categoría y una temporada.
    /// </summary>
    public class Categoria_Temporada
    {
        private int _idCategoriaTemporada;

        /// <summary>
        /// Obtiene o establece el identificador único de la categoría-temporada.
        /// </summary>
        public int IdCategoriaTemporada
        {
            get { return _idCategoriaTemporada; }
            set { _idCategoriaTemporada = value; }
        }

        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre de la categoría-temporada.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
