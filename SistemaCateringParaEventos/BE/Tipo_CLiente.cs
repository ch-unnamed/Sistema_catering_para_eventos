using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tipo_CLiente
    {
		private int _id_tipo_cliente;

		public int Id_Tipo_Cliente
		{
			get { return _id_tipo_cliente; }
			set { _id_tipo_cliente = value; }
		}

		private string _nombre;

		public string Nombre
		{
			get { return _nombre; }
			set { _nombre = value; }
		}

	}
}