using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Lote
    {
        private int _codigo;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        private DateTime _fechaDeVencimiento;

        public DateTime FechaDeVencimiento
        {
            get { return _fechaDeVencimiento; }
            set { _fechaDeVencimiento = value; }
        }

        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

    }
}