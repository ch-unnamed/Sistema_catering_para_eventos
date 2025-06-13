using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Menu
    {
		private int _id;

		public int Id
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

		private DateTime _fechaDeCreacion;

		public DateTime FechaDeCreacion
		{
			get { return _fechaDeCreacion; }
			set { _fechaDeCreacion = value; }
		}

		private List<Plato> _platos;

		public List<Plato> Platos
		{
			get { return _platos; }
			set { _platos = value; }
		}
	}
}
