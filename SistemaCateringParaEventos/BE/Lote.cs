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

        private decimal _cantidad;

        public decimal Cantidad
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

        private decimal _costoUnitario;

        public decimal CostoUnitario
        {
            get { return _costoUnitario; }
            set { _costoUnitario = value; }
        }

        private bool _estado;

        public bool  Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
    }
}