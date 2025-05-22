using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    public class Evento
    {
        private int _idEvento;
        public int IdEvento
        {
            get { return _idEvento; }
            set { _idEvento = value; }
        }

        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private int _capacidad;
        public int Capacidad
        {
            get { return _capacidad; }
            set
            {
                if (value > 0)
                {
                    _capacidad = value;
                }
                else
                {
                    throw new ArgumentException("La capacidad debe ser mayor a 0.");
                }
            }
        }

        private string _tipo;
        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        private Ubicacion _ubicacion;
        public Ubicacion Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }
    }
}