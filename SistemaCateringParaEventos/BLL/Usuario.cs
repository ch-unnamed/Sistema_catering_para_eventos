using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class Usuario
    {
        private readonly DAL.Usuario oUsuarioDAL;

        public Usuario()
        {
            oUsuarioDAL = new DAL.Usuario();
        }

        public List<BE.Usuario> ListarUsuario()
        {
            DAL.Usuario usuarioDAL = new DAL.Usuario();
            return usuarioDAL.ListarUsuario();
        }



        public int CrearUsuario(BE.Usuario usuario)
        {

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("El usuario necesita un nombre");

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
                throw new Exception("El usuario necesita un apellido");

            return oUsuarioDAL.InsertarUsuario(usuario);

        }

        public bool EliminarUsuario(int Id)
        {
            DAL.Usuario usuarioDAL = new DAL.Usuario();
            try
            {
                usuarioDAL.EliminarUsuario(Id);
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error SQL: " + ex.Message);
                return false;
            }
        }


        public BE.Usuario idVendedor(int rol_id)
        {
            DAL.Usuario usuario = new DAL.Usuario();

            return usuario.idVendedor(rol_id);
        }

    }
}