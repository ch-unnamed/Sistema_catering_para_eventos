using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Ubicacion
    {
        private int _idUbicacion;
        public int IdUbicacion
        {
            get { return _idUbicacion; }
            set { _idUbicacion = value; }
        }

        private string _direccion;
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        private string _ciudad;
        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }

        private string _pais;
        public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }
    }
}
