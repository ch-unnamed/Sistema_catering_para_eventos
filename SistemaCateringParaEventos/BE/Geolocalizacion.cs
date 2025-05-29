using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Geolocalizacion
    {
		private int _idGeolocalizacion;

		public int IdGeolocalizacion
		{
			get { return _idGeolocalizacion; }
			set { _idGeolocalizacion = value; }
		}

		private decimal _latitud;

		public decimal Latitud
		{
			get { return _latitud; }
			set { _latitud = value; }
		}

		private decimal _longitud;

		public decimal  Longitud
		{
			get { return _longitud; }
			set { _longitud = value; }
		}

	}
}
