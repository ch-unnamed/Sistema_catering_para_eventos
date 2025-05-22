using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Cotizacion
    {
        private int _idCotizacion;
        public int IdCotizacion
        {
            get { return _idCotizacion; }
            set { _idCotizacion = value; }
        }

        private Evento _evento;

        public Evento Evento
        {
            get { return _evento; }
            set { _evento = value; }
        }

        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        private Usuario _vendedor;
        public Usuario Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; }
        }

        private Estado _estado;

        public Estado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        private DateTime _fechaPedido;
        public DateTime FechaPedido
        {
            get { return _fechaPedido; }
            set { _fechaPedido = value; }
        }

        private DateTime _fechaRealizacion;
        public DateTime FechaRealizacion
        {
            get { return _fechaRealizacion; }
            set { _fechaRealizacion = value; }
        }

        private decimal _total;
        public decimal Total
        {
            get { return _total; }
            set
            {
                if (value >= 0)
                {
                    _total = value;
                }
                else
                {
                    throw new ArgumentException("El total no puede ser negativo.");
                }
            }
        }
    }
}
