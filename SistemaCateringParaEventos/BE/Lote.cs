using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Lote
    {

        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        private DateTime _fechaDeIngreso;

        public DateTime FechaDeIngreso
        {
            get { return _fechaDeIngreso; }
            set { _fechaDeIngreso = value; }
        }

        private DateTime _fechaDeVencimiento;

        public DateTime FechaDeVencimiento
        {
            get { return _fechaDeVencimiento; }
            set { _fechaDeVencimiento = value; }
        }

        private bool _estado;

        public bool  Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
    }
}