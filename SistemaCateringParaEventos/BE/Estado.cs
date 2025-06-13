using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{   // Esta es una clase de una tabla intermedia
    public class Estado
    {
        private int _idEstado;
        public int IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (value == "Pendiente" || value == "Confirmado" ||
                    value == "Rechazado" || value == "Completado")
                {
                    _nombre = value;
                }
                else
                {
                    throw new ArgumentException("El estado debe ser 'Pendiente', 'Confirmado', 'Rechazado' o 'Completado'.");
                }
            }
        }
    }
}
