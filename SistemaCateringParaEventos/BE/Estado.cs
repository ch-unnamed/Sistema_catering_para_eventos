using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{   // Esta es una clase de una tabla intermedia
    public class Estado
    {
        private int _idEstadoEvento;
        public int IdEstadoEvento
        {
            get { return _idEstadoEvento; }
            set { _idEstadoEvento = value; }
        }

        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (value == "Pendiente" || value == "Confirmado" ||
                    value == "Cancelado" || value == "Completado")
                {
                    _nombre = value;
                }
                else
                {
                    throw new ArgumentException("El estado debe ser 'Pendiente', 'Confirmado', 'Cancelado' o 'Completado'.");
                }
            }
        }
    }
}
