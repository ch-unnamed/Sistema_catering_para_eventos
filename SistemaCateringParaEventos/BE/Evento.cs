using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    public class Evento
    {
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

        private Usuario _vendedor;
        public Usuario Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; }
        }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        private EstadoEvento _estado;
        public EstadoEvento Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        private decimal _cotizacion;
        public decimal Cotizacion
        {
            get { return _cotizacion; }
            set
            {
                if (value >= 0)
                {
                    _cotizacion = value;
                }
                else
                {
                    throw new ArgumentException("La cotización no puede ser negativa.");
                }
            }
        }


    }
}