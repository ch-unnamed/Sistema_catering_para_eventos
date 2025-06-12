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

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _insumoId;

        public int InsumoId
        {
            get { return _insumoId; }
            set { _insumoId = value; }
        }

        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        private double _costoUnidatario;

        public double CostoUnitario
        {
            get { return _costoUnidatario; }
            set { _costoUnidatario = value; }
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