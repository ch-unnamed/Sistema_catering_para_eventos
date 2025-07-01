using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Rol
    {
        private readonly Conexion conexion = new Conexion();
        public List<BE.Rol> ListarRol()
        {
            Conexion conexion = new Conexion();

            List<BE.Rol> roles = new List<BE.Rol>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_roles");

            foreach (DataRow row in dt.Rows)
            {
                BE.Rol unRol = new BE.Rol();

                unRol.IdRol = Convert.ToInt32(row["id"]);
                unRol.NombreRol = row["nombre"].ToString();

                roles.Add(unRol);
            }

            return roles;
        }
    }
}
