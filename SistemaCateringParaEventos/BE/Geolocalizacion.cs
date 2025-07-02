using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa una ubicación geográfica con latitud y longitud.
    /// </summary>
    public class Geolocalizacion
    {
        /// <summary>
        /// Identificador único de la geolocalización.
        /// </summary>
        private int _idGeolocalizacion;

        /// <summary>
        /// Obtiene o establece el identificador único de la geolocalización.
        /// </summary>
        public int IdGeolocalizacion
        {
            get { return _idGeolocalizacion; }
            set { _idGeolocalizacion = value; }
        }

        /// <summary>
        /// Latitud de la ubicación geográfica.
        /// </summary>
        private decimal _latitud;

        /// <summary>
        /// Obtiene o establece la latitud de la ubicación geográfica.
        /// </summary>
        public decimal Latitud
        {
            get { return _latitud; }
            set { _latitud = value; }
        }

        /// <summary>
        /// Longitud de la ubicación geográfica.
        /// </summary>
        private decimal _longitud;

        /// <summary>
        /// Obtiene o establece la longitud de la ubicación geográfica.
        /// </summary>
        public decimal Longitud
        {
            get { return _longitud; }
            set { _longitud = value; }
        }
    }
}
