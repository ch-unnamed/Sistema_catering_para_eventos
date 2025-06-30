using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Cotizacion_Menu_Plato
    {
		private int _id;

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private Cotizacion_Menu _cotizacion_menu;

		public Cotizacion_Menu CotizacionMenu
		{
			get { return _cotizacion_menu; }
			set { _cotizacion_menu = value; }
		}

		private Plato _plato;

		public Plato Plato
		{
			get { return _plato; }
			set { _plato = value; }
		}

	}
}