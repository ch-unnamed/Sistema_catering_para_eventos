using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    // Puede acceder el Vendedor y el Gerente
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

        private DateTime _fecha;
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
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

        private Ubicacion _ubicacionId;

        public Ubicacion UbicacionId
        {
            get { return _ubicacionId; }
            set { _ubicacionId = value; }
        }

        private Usuario _vendedorId;

        public Usuario MyProperty
        {
            get { return _vendedorId; }
            set { _vendedorId = value; }
        }

        private Cliente _clienteId;

        public Cliente ClienteId
        {
            get { return _clienteId; }
            set { _clienteId = value; }
        }

        private EstadoEvento _estadoId;

        public EstadoEvento EstadoId
        {
            get { return _estadoId; }
            set { _estadoId = value; }
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