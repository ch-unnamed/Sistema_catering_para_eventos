using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Descuento_Cotizacion
    {
		private int _id_descuento_cotizacion;

		public int ID_Descuento_Cotizacion
		{
			get { return _id_descuento_cotizacion; }
			set { _id_descuento_cotizacion = value; }
		}

		private Descuento _idDescuento;

		public Descuento IdDescuento
		{
			get { return _idDescuento; }
			set { _idDescuento = value; }
		}

		private Cotizacion _idCotizacion;

		public Cotizacion IdCotizacion
		{
			get { return _idCotizacion; }
			set { _idCotizacion = value; }
		}

	}
}
