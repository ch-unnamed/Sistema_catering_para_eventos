using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Plato
    {
		private int _id;

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private DateTime _fechaDeCreacion;

		public DateTime FechaDeCreacion
		{
			get { return _fechaDeCreacion; }
			set { _fechaDeCreacion = value; }
		}

		private List <Insumo> _insumos;

		public List <Insumo> Insumos
		{
			get { return _insumos; }
			set { _insumos = value; }
		}

		private string _categoria;

		public string Categoria
		{
			get { return _categoria; }
			set { _categoria = value; }
		}

	}
}
