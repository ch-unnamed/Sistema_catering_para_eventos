using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUVendedor.Models
{
	public class Usuario
	{
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Rol Rol_id { get; set; }
    }
}