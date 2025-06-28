using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Plato
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

        private decimal _precio;

        public decimal Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private DateTime _fechaDeCreacion;

        public DateTime FechaDeCreacion
        {
            get { return _fechaDeCreacion; }
            set { _fechaDeCreacion = value; }
        }

        private List<Insumo> _insumos;

        public List<Insumo> Insumos
        {
            get { return _insumos; }
            set { _insumos = value; }
        }


    }
}