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

        private Geolocalizacion _idGeolocalizacion;

        public Geolocalizacion IdGeolocalizacion
        {
            get { return _idGeolocalizacion; }
            set { _idGeolocalizacion = value; }
        }

        private string _calle;
        public string Calle
        {
            get { return _calle; }
            set { _calle = value; }
        }

        private int _altura;
        public int Altura
        {
            get { return _altura; }
            set { _altura = value; }
        }

        private string _ciudad;
        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }

        private string _provincia;
        public string Provincia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }
    }
}
