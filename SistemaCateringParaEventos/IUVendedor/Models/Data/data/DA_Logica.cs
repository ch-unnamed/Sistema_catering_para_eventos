using DAL;
using IUVendedor.Models; // referencia para tener todo
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IUVendedor.Models.Data
{
    public class DA_Logica
    {
        //    //Simular la conexion a la DB 
        //    public List<Usuario> ListaUsuario()
        //    {
        //        return new List<Usuario>()
        //{
        //    new Usuario {Nombre = "Sasha", Correo = "gerente01@gmail.com", Clave = "123", IdRol = Rol.Gerente},
        //    new Usuario {Nombre = "Matias", Correo = "vendedor01@gmail.com", Clave = "456", IdRol = Rol.Vendedor},
        //    new Usuario {Nombre = "Anabela", Correo = "chef01@gmail.com", Clave = "111", IdRol = Rol.Chef}
        //};
        //    }
        //    public Usuario ValidarUsuario(string _correo, string _clave)
        //    {
        //        return ListaUsuario().Where(item => item.Correo == _correo && item.Clave == _clave).FirstOrDefault();
        //    }

        public Usuario ValidarUsuario(string _correo, string _clave)
        {
            Usuario usuario = null;
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
        conexion.crearParametro("@Correo", _correo),
        conexion.crearParametro("@Clave", _clave)
            };

            DataTable tabla = conexion.LeerPorStoreProcedure("sp_ValidarUsuario", parametros);

            if (tabla != null && tabla.Rows.Count > 0)
            {
                DataRow row = tabla.Rows[0];
                usuario = new Usuario()
                {
                    Nombre = row["nombre"].ToString(),
                    Apellido = row["apellido"].ToString(),
                    Email = row["email"].ToString(),
                    PasswordHash = row["passwordHash"].ToString(),
                    Rol_id = (Rol)Convert.ToInt32(row["rol_id"])
                };
            }

            return usuario;
        }


    }
}