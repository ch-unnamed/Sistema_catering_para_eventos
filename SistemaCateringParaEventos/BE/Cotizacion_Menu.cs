using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Cotizacion_Menu
    {
		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private Cotizacion _cotizacion;

		public Cotizacion Cotizacion
		{
			get { return _cotizacion; }
			set { _cotizacion = value; }
		}

		private Menu _menu;

		public Menu Menu
		{
			get { return _menu; }
			set { _menu = value; }
		}

		private int _estado;

		public int Estado
		{
			get { return _estado; }
			set { _estado = value; }
		}

	}
}