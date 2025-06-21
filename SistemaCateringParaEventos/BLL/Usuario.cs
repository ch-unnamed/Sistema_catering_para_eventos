using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Usuario
    {
        /*
        public List<BE.Usuarios> ListarUsuarios()
        {
  
        }

        public bool CrearUsuario(string nombreUsuario, string apellidoUsuario, string emailUsuario, string passwordHash, Rol rolUsuario)
        {
        }
        
        public bool EditarUsuario(string nombreUsuario, string apellidoUsuario, string emailUsuario,string passwordHash, Rol rolUsuario)
        {

        }

        public bool EliminarUsuario(int IDUsuario)
        {

        }
        */

        public BE.Usuario idVendedor(int rol_id)
        {
            DAL.Usuario usuario = new DAL.Usuario();

            return usuario.idVendedor(rol_id);
        }

    }
}