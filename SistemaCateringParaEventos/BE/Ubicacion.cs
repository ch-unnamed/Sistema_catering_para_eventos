using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa una ubicación física con información de dirección y geolocalización.
    /// </summary>
    public class Ubicacion
    {
        private int _idUbicacion;

        /// <summary>
        /// Obtiene o establece el identificador único de la ubicación.
        /// </summary>
        public int IdUbicacion
        {
            get { return _idUbicacion; }
            set { _idUbicacion = value; }
        }

        private Geolocalizacion _idGeolocalizacion;

        /// <summary>
        /// Obtiene o establece la geolocalización asociada a la ubicación.
        /// </summary>
        public Geolocalizacion IdGeolocalizacion
        {
            get { return _idGeolocalizacion; }
            set { _idGeolocalizacion = value; }
        }

        private string _calle;

        /// <summary>
        /// Obtiene o establece el nombre de la calle de la ubicación.
        /// </summary>
        public string Calle
        {
            get { return _calle; }
            set { _calle = value; }
        }

        private int _altura;

        /// <summary>
        /// Obtiene o establece la altura (número) de la calle.
        /// </summary>
        public int Altura
        {
            get { return _altura; }
            set { _altura = value; }
        }

        private string _ciudad;

        /// <summary>
        /// Obtiene o establece la ciudad de la ubicación.
        /// </summary>
        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }

        private string _provincia;

        /// <summary>
        /// Obtiene o establece la provincia de la ubicación.
        /// </summary>
        public string Provincia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }
    }
}
