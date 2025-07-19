using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa un cliente con información personal y de contacto.
    /// </summary>
    public class Cliente
    {
        private int _idCliente;

        /// <summary>
        /// Obtiene o establece el identificador único del cliente.
        /// </summary>
        public int IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        private long _dni;

        /// <summary>
        /// Obtiene o establece el DNI del cliente.
        /// </summary>
        public long Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private string _email;

        /// <summary>
        /// Obtiene o establece el correo electrónico del cliente.
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private DateTime _fechaCreacion;

        /// <summary>
        /// Obtiene o establece la fecha de creación del cliente.
        /// </summary>
        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }

        private string _nombre;

        /// <summary>
        /// Obtiene o establece el nombre del cliente.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;

        /// <summary>
        /// Obtiene o establece el apellido del cliente.
        /// </summary>
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private Localidad _localidad;

        /// <summary>
        /// Obtiene o establece la localidad del cliente.
        /// </summary>
        public Localidad Localidad
        {
            get { return _localidad; }
            set { _localidad = value; }
        }

        private long _telefono;

        /// <summary>
        /// Obtiene o establece el número de teléfono del cliente.
        /// </summary>
        public long Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        private Tipo_Cliente _tipo_cliente;

        /// <summary>
        /// Obtiene o establece el tipo de cliente.
        /// </summary>
        public Tipo_Cliente Tipo_Cliente
        {
            get { return _tipo_cliente; }
            set { _tipo_cliente = value; }
        }
    }
}
