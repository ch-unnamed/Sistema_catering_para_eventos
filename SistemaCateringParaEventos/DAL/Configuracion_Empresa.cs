using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Clase de acceso a datos para la configuración de la empresa.
    /// Proporciona métodos para obtener el porcentaje de ganancia y consultar información de clientes.
    /// </summary>
    public class Configuracion_Empresa
    {
        /// <summary>
        /// Obtiene la configuración de empresa correspondiente al nombre especificado,
        /// incluyendo el porcentaje de ganancia.
        /// </summary>
        /// <param name="nombre">Nombre de la empresa.</param>
        /// <returns>Instancia de <see cref="BE.Configuracion_Empresa"/> con la configuración encontrada, o null si no existe.</returns>
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

            return null;
        }

        /// <summary>
        /// Consulta la cantidad asociada a un cliente específico por su identificador.
        /// </summary>
        /// <param name="cliente_id">Identificador del cliente.</param>
        /// <returns>Entero que representa la cantidad encontrada, o 0 si no existe.</returns>
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
