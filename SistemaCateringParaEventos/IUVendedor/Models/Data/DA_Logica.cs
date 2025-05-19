using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IUVendedor.Models; // referencia para tener todo

namespace IUVendedor.Data
{
    public class DA_Logica
    {
        //Simular la conexion a la DB 
        public List<Usuario> ListaUsuario()
        {
            return new List<Usuario>()
            {
                new Usuario{Nombre = "Sasha", Correo = "gerente01@gmail.com", Clave = "123", IdRol = Rol.Gerente},
                new Usuario{Nombre = "Matias", Correo = "vendedor01@gmail.com", Clave = "456", IdRol = Rol.Vendedor},
                new Usuario {Nombre = "Anabela", Correo = "chef01@gmail.com", Clave = "111", IdRol = Rol.Chef}
            };
        }
        public Usuario ValidarUsuario(string _correo, string _clave)
        {
            return ListaUsuario().Where(item => item.Correo == _correo && item.Clave == _clave).FirstOrDefault();
        }


    }
}