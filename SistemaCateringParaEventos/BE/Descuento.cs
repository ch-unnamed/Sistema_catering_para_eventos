using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Descuento
    {
		private int _idDescuento;

		public int IdDescuento
		{
			get { return _idDescuento; }
			set { _idDescuento = value; }
		}

		private string _nombre;

		public string Nombre
		{
			get { return _nombre; }
			set { _nombre = value; }
		}

		private string descripcion;

		public string Descripcion
		{
			get { return descripcion; }
			set { descripcion = value; }
		}

		private decimal _porcentaje;

		public decimal Porcentaje
		{
			get { return _porcentaje; }
			set { _porcentaje = value; }
		}

	}
}