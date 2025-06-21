using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Configuracion_Empresa
    {
        public BE.Configuracion_Empresa ObtenerPorcentajeGanancia(string nombre)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", nombre)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_ganancia_general", parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow fila = dt.Rows[0];
                return new BE.Configuracion_Empresa
                {
                    PorcentajeGanancia = Convert.ToDecimal(fila["PorcentajeGanancia"])
                };
            }

            return null; // Si no hay datos, devuelve null
        }

        public int consultarCliente(int cliente_id)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@cliente_id", cliente_id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_descuento_cliente", parametros);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["cantidad"]);
            }
            return 0;
        }
    }
}
