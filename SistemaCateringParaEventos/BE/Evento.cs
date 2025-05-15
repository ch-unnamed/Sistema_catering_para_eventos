using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Evento
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

		private string _estadoDelEvento;

		public string EstadoDelEvento
		{
			get { return _estadoDelEvento; }
			set { _estadoDelEvento = value; }
		}

		private Cliente _cliente;

		public Cliente Cliente
		{
			get { return _cliente; }
			set { _cliente = value; }
		}

		private Menu _menu;

		public Menu Menu
		{
			get { return _menu; }
			set { _menu = value; }
		}

		private float _cotizacion;

		public float Cotizacion
		{
			get { return _cotizacion; }
			set { _cotizacion = value; }
		}

		private string _region;

		public string Region
		{
			get { return _region; }
			set { _region = value; }
		}

		private string _notaAdicional;

		public string NotaAdicional
		{
			get { return _notaAdicional; }
			set { _notaAdicional = value; }
		}



	}
}
