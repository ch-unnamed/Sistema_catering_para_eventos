using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Rol
    {
		private int _idRol;

		public int IdRol
		{
			get { return _idRol; }
			set { _idRol = value; }
		}

		private string _nombre;

		public string Nombre
		{
			get { return _nombre; }
			set { _nombre = value; }
		}

	}
}