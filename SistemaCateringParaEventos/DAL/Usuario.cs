using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Usuario
    {   
        /*
        public bool EditarUsuario(string nombreUsuario, string apellidoUsuario, string emailUsuario, Rol rolUsuario)
        {

        }
        public bool EliminarUsuario(int IDUsuario)
        {

        }
        public bool CrearUsuario(string nombreUsuario, string apellidoUsuario, string emailUsuario, string passwordHash, Rol rolUsuario)
        {
        }*/
    
        public BE.Usuario idVendedor(int rol_id)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@rol_id", rol_id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_id_usuario", parametros);

            if(dt.Rows.Count > 0)
            {
                DataRow fila = dt.Rows[0];

                return new BE.Usuario
                {
                    IdUsuario = Convert.ToInt32(fila["id"])
                };
            }

            return null;
        }
    }
}
