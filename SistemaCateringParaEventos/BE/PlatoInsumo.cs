using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PlatoInsumo
    {
		private int _id_plato;

		public int Id_Plato
		{
			get { return _id_plato; }
			set { _id_plato = value; }
		}

		private int _id_Insumo;

		public int Id_Insumo
		{
			get { return _id_Insumo; }
			set { _id_Insumo = value; }
		}

		private int _cantidad;

		public int Cantidad
		{
			get { return _cantidad; }
			set { _cantidad = value; }
		}

		private Insumo _insumo;

		public Insumo Insumo
		{
			get { return _insumo; }
			set { _insumo = value; }
		}


	}
}
