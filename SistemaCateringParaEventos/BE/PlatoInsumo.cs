using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PlatoInsumo
    {
		private Plato _plato;

		public Plato Plato
		{
			get { return _plato; }
			set { _plato = value; }
		}


		private Insumo _insumo;

		public Insumo Insumo
		{
			get { return _insumo; }
			set { _insumo = value; }
		}

		private int _cantidad;

		public int Cantidad
		{
			get { return _cantidad; }
			set { _cantidad = value; }
		}
	}
}
