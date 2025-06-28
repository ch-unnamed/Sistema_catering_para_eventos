using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Clase de lógica de negocio para la configuración de la empresa.
    /// Proporciona métodos para obtener el porcentaje de ganancia y consultar clientes.
    /// </summary>
    public class Configuracion_Empresa
    {
        /// <summary>
        /// Obtiene la configuración de empresa correspondiente al nombre especificado,
        /// incluyendo el porcentaje de ganancia.
        /// </summary>
        /// <param name="nombre">Nombre de la empresa.</param>
        /// <returns>Instancia de <see cref="BE.Configuracion_Empresa"/> con la configuración encontrada.</returns>
        public BE.Configuracion_Empresa ObtenerPorcentajeGanancia(string nombre)
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();
            return configuracionDAL.ObtenerPorcentajeGanancia(nombre);
        }

        /// <summary>
        /// Consulta la existencia de un cliente por su identificador.
        /// </summary>
        /// <param name="cliente_id">Identificador del cliente.</param>
        /// <returns>Entero que representa el resultado de la consulta.</returns>
        public int consultarCliente(int cliente_id)
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();
            return configuracionDAL.consultarCliente(cliente_id);
        }
    }
}