using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Temporada
    {

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

		private Categoria_Temporada id_Categoria_Temporada;

        public Categoria_Temporada Id_CategoriaTemporada
        {
            get { return id_Categoria_Temporada; }
            set { id_Categoria_Temporada = value; }
        }

        private DateTime _fechaComienzo;
        public DateTime FechaComienzo
        {
            get { return _fechaComienzo; }
            set { _fechaComienzo = value; }
        }

        private DateTime _fechaFin;
        public DateTime FechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }

        private int _cantidad_evento;

        public int CantidadEvento
        {
            get { return _cantidad_evento; }
            set { _cantidad_evento = value; }
        }

    }
}
