using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Temporada
    {
<<<<<<< HEAD
        private int _idTemporada;
        public int IdTemporada
        {
            get { return _idTemporada; }
            set { _idTemporada = value; }
        }


        private string _nombreTemporada;
        public string Nombre
        {
            get { return _nombreTemporada; }
            set { _nombreTemporada = value; }
        }


        private DateTime _fechaInicio;
        public DateTime FechaInicio
        {
            get { return _fechaInicio; }
            set { _fechaInicio = value; }
        }


=======
		private int _idTemporada;

		public int IdTemporada
		{
			get { return _idTemporada; }
			set { _idTemporada = value; }
		}

		private Categoria_Temporada id_Categoria_Temporada;

        public Categoria_Temporada Id_CategoriaTemporada
        {
            get { return id_Categoria_Temporada; }
            set { id_Categoria_Temporada = value; }
        }

		private string _nombre;

		public string Nombre
		{
			get { return _nombre; }
			set { _nombre = value; }
		}

        private DateTime _fechaComienzo;
        public DateTime FechaComienzo
        {
            get { return _fechaComienzo; }
            set { _fechaComienzo = value; }
        }

>>>>>>> mati
        private DateTime _fechaFin;
        public DateTime FechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }
<<<<<<< HEAD
=======

        private int _cantidad_evento;

        public int CantidadEvento
        {
            get { return _cantidad_evento; }
            set { _cantidad_evento = value; }
        }

>>>>>>> mati
    }
}
