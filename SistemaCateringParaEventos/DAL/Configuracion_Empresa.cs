using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL
{

    /// <summary>
    /// Clase de acceso a datos para la configuración de la empresa.
    /// Proporciona métodos para obtener el porcentaje de ganancia y consultar información de clientes.
    /// </summary>
    public class Configuracion_Empresa
    {

        public List<BE.Configuracion_Empresa> Listar()
        {
            Conexion conexion = new Conexion();

            List<BE.Configuracion_Empresa> configuraciones = new List<BE.Configuracion_Empresa>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_config");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Configuracion_Empresa unaConfig = new BE.Configuracion_Empresa();

                unaConfig.ID = Convert.ToInt32(fila["Id"]);
                unaConfig.Nombre = fila["Nombre"].ToString();
                unaConfig.Porcentaje = Convert.ToDecimal(fila["Porcentaje"]);

                configuraciones.Add(unaConfig);
            }

            return configuraciones;

        }

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
                    Porcentaje = Convert.ToDecimal(fila["Porcentaje"])
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

            return Convert.ToInt32(dt.Rows[0]["cantidad"]);

        }

        public int CrearConfiguracion(BE.Configuracion_Empresa configuracion, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

            SqlParameter[] parametrosSql = new SqlParameter[]
            {
                new SqlParameter("@admin_id", configuracion.Admin.IdUsuario),
                new SqlParameter("@Nombre", configuracion.Nombre),
                new SqlParameter("@Porcentaje", configuracion.Porcentaje),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_crear_config", parametrosSql);

            mensaje = parametrosSql[3].Value.ToString();
            resultado = Convert.ToInt32(parametrosSql[4].Value);

            return resultado;
        }

        public bool EditarConfiguracion(BE.Configuracion_Empresa configuracion, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            SqlParameter[] parametrosSql = new SqlParameter[]
            {
                new SqlParameter("@idConfig", configuracion.ID),
                new SqlParameter("@Nombre", configuracion.Nombre),
                new SqlParameter("@Porcentaje", configuracion.Porcentaje),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_editar_config", parametrosSql);

            mensaje = parametrosSql[3].Value.ToString();
            resultado = Convert.ToBoolean(parametrosSql[4].Value);

            return resultado;
        }

        public bool EliminarConfiguracion(int idConfiguracion, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            SqlParameter[] parametrosSql = new SqlParameter[]
            {
                new SqlParameter("@idConfig", idConfiguracion),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
            };

            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_eliminar_config", parametrosSql);

            mensaje = parametrosSql[1].Value.ToString();
            resultado = Convert.ToBoolean(parametrosSql[2].Value);

            return resultado;

        }
    }
}