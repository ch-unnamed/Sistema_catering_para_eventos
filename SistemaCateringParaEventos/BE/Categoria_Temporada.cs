using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Categoria_Temporada
    {
        private int _idCategoriaTemporada;
        public int IdCategoriaTemporada
        {
            get { return _idCategoriaTemporada; }
            set { _idCategoriaTemporada = value; }
        }

        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
