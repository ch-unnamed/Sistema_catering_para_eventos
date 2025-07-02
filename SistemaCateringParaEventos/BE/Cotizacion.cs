using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa una cotización de un evento, incluyendo información del cliente, vendedor, menú, estado y totales.
    /// </summary>
    public class Cotizacion
    {
        private int _idCotizacion;
        /// <summary>
        /// Obtiene o establece el identificador único de la cotización.
        /// </summary>
        public int IdCotizacion
        {
            get { return _idCotizacion; }
            set { _idCotizacion = value; }
        }

        private Evento _evento;
        /// <summary>
        /// Obtiene o establece el evento asociado a la cotización.
        /// </summary>
        public Evento Evento
        {
            get { return _evento; }
            set { _evento = value; }
        }

        private Cliente _cliente;
        /// <summary>
        /// Obtiene o establece el cliente asociado a la cotización.
        /// </summary>
        public Cliente Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        private Usuario _vendedor;
        /// <summary>
        /// Obtiene o establece el vendedor responsable de la cotización.
        /// </summary>
        public Usuario Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; }
        }

        private Estado _estado;
        /// <summary>
        /// Obtiene o establece el estado actual de la cotización.
        /// </summary>
        public Estado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        private Menu _menu;
        /// <summary>
        /// Obtiene o establece el menú seleccionado en la cotización.
        /// </summary>
        public Menu Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }

        private DateTime _fechaPedido;
        /// <summary>
        /// Obtiene o establece la fecha en que se realizó el pedido de la cotización.
        /// </summary>
        public DateTime FechaPedido
        {
            get { return _fechaPedido; }
            set { _fechaPedido = value; }
        }

        private decimal _total;
        /// <summary>
        /// Obtiene o establece el total de la cotización. No puede ser negativo.
        /// </summary>
        /// <exception cref="ArgumentException">Se lanza si el valor es negativo.</exception>
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

        private DateTime _fechaRealizacion;
        /// <summary>
        /// Obtiene o establece la fecha de realización de la cotización.
        /// </summary>
        public DateTime FechaRealizacion
        {
            get { return _fechaRealizacion; }
            set { _fechaRealizacion = value; }
        }
    }
}
