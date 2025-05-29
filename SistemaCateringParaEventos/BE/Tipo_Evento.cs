using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TipoEvento
    {
		private int _idTipoEvento;

		public int Id_TipoEvento
		{
			get { return _idTipoEvento; }
			set { _idTipoEvento = value; }
		}

		private string _nombre;

		public string Nombre
		{
			get { return _nombre; }
			set { _nombre = value; }
		}

	}
}
