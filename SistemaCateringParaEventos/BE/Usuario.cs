using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario
    {
		private int _IdUsuario;

		public int IdUsuario
		{
			get { return _IdUsuario; }
			set { _IdUsuario = value; }
		}

		private string _email;

		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}

		private string _passwordHash;

		public string PasswordHash
        {
			get { return _passwordHash; }
			set { _passwordHash = value; }
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

		private Rol _rol;

		public Rol RolUsuario
		{
			get { return _rol; }
			set { _rol = value; }
		}

	}
}
