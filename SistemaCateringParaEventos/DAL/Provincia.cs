using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DAL
{
    public class Provincia
    {
        public BE.Provincia obtenerProvincia(string nombreLocalidad)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", nombreLocalidad)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_provincia_nombre", parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow fila = dt.Rows[0];

                return new BE.Provincia
                {
                    Nombre = fila["provincia_nombre"].ToString()
                };
            }
            return null;
        }

    }
}