﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Insumo
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

        private string _tipo;

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        private int _unidad; 

        public int Unidad
        {
            get { return _unidad; }
            set { _unidad = value; }
        }

        private int _stockMinimo;

        public int StockMinimo
        {
            get { return _stockMinimo; }
            set { _stockMinimo = value; }
        }

        private double _costo;

        public double Costo
        {
            get { return _costo; }
            set { _costo = value; }
        }

        private int _LoteId;

        public int LoteId
        {
            get { return _LoteId; }
            set { _LoteId = value; }
        }

    }
}