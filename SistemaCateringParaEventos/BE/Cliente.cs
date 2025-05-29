using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Cliente
    {
        private int _idCliente;
        public int IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        private long _dni;
        public long Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private DateTime _fechaCreacion;
        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }

        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private string _region;
        public string Region
        {
            get { return _region; }
            set { _region = value; }
        }

        private long _telefono;
        public long Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        private Tipo_Cliente _tipo_cliente;

        public Tipo_Cliente Tipo_Cliente
        {
            get { return _tipo_cliente; }
            set { _tipo_cliente = value; }
        }

    }
}
