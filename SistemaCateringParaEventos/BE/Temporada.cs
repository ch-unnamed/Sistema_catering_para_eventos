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


        private DateTime _fechaFin;
        public DateTime FechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }
    }
}
