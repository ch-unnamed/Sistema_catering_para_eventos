using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuracion_Empresa
    {
		private int _id;

		public int ID
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _nombre;

		public string Nombre
		{
			get { return _nombre; }
			set { _nombre = value; }
		}

		private decimal _porcentajeGanancia;

		public decimal PorcentajeGanancia
		{
			get { return _porcentajeGanancia; }
			set { _porcentajeGanancia = value; }
		}

		private Usuario _admin;

		public Usuario Admin
		{
			get { return _admin; }
			set { _admin = value; }
		}

	}
}
