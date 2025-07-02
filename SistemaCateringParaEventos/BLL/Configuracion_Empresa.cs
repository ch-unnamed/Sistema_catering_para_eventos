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

        /// <summary>
        /// Lista todas las configuraciones de empresa existentes.
        /// </summary>
        /// <returns>Lista de instancias de <see cref="BE.Configuracion_Empresa"/>.</returns>
        public List<BE.Configuracion_Empresa> Listar()
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();
            return configuracionDAL.Listar();
        }

        /// <summary>
        /// Valida los datos de una configuración de empresa.
        /// </summary>
        /// <param name="configuracion">Instancia de <see cref="BE.Configuracion_Empresa"/> a validar.</param>
        /// <returns>Diccionario con los errores encontrados, donde la clave es el campo y el valor es el mensaje de error.</returns>
        public Dictionary<string, string> ValidarConfiguracion(BE.Configuracion_Empresa configuracion)
        {
            var errores = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(configuracion.Nombre) ||
                !System.Text.RegularExpressions.Regex.IsMatch(configuracion.Nombre, @"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]+$"))
                errores["nombre"] = "La configuración debe tener un nombre válido";

            if (!decimal.TryParse(configuracion.Porcentaje.ToString(), out decimal porcentaje) || porcentaje <= 0)
                errores["porcentaje"] = "La Configuracion debe tener un porcentaje valido";

            return errores;
        }

        /// <summary>
        /// Crea una nueva configuración de empresa si los datos son válidos.
        /// </summary>
        /// <param name="configuracion">Instancia de <see cref="BE.Configuracion_Empresa"/> a crear.</param>
        /// <param name="errores">Diccionario de errores de validación encontrados.</param>
        /// <returns>Identificador de la configuración creada, o 0 si hay errores.</returns>
        public int CrearConfiguracion(BE.Configuracion_Empresa configuracion, out Dictionary<string, string> errores)
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();
            errores = ValidarConfiguracion(configuracion);

            if (errores.Count == 0)
                return configuracionDAL.CrearConfiguracion(configuracion, out _);
            else
                return 0;
        }

        /// <summary>
        /// Edita una configuración de empresa existente si los datos son válidos.
        /// </summary>
        /// <param name="configuracion">Instancia de <see cref="BE.Configuracion_Empresa"/> a editar.</param>
        /// <param name="errores">Diccionario de errores de validación encontrados.</param>
        /// <returns>True si la edición fue exitosa, false en caso contrario.</returns>
        public bool EditarConfiguracion(BE.Configuracion_Empresa configuracion, out Dictionary<string, string> errores)
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();
            errores = ValidarConfiguracion(configuracion);

            if (errores.Count == 0)
                return configuracionDAL.EditarConfiguracion(configuracion, out _);
            else
                return false;
        }

        /// <summary>
        /// Elimina una configuración de empresa por su identificador.
        /// </summary>
        /// <param name="idConfiguracion">Identificador de la configuración a eliminar.</param>
        /// <param name="mensaje">Mensaje resultante de la operación.</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario.</returns>
        public bool EliminarConfiguracion(int idConfiguracion, out string mensaje)
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();
            return configuracionDAL.EliminarConfiguracion(idConfiguracion, out mensaje);
        }
    }
}