using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Insumo
    {
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _stockMinimo;

        public int StockMinimo
        {
            get { return _stockMinimo; }
            set { _stockMinimo = value; }
        }

        private List<Lote> _lotes;

        public List<Lote> Lotes
        {
            get { return Lotes; }
            set { Lotes = value; }
        }

        private string _tipo;

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

    }
}