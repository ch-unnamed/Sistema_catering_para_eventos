using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL;
using System.Net.Mail;

namespace BLL
{
    /// <summary>
    /// Lógica de negocio para la gestión de clientes.
    /// </summary>
    public class Cliente
    {
        /// <summary>
        /// Obtiene la lista de todos los clientes.
        /// </summary>
        /// <returns>Lista de objetos BE.Cliente.</returns>
        public List<BE.Cliente> Listar()
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            return clienteDAL.Listar();
        }

        /// <summary>
        /// Valida si el email tiene un formato correcto.
        /// </summary>
        /// <param name="email">Email a validar.</param>
        /// <returns>True si el email es válido, false en caso contrario.</returns>
        bool EmailValido(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Valida los datos de un cliente.
        /// </summary>
        /// <param name="cliente">Cliente a validar.</param>
        /// <returns>Diccionario con los errores encontrados, vacío si no hay errores.</returns>
        public Dictionary<string, string> ValidarCliente(BE.Cliente cliente)
        {
            var errores = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                errores["nombre"] = "El Cliente debe tener un Nombre";

            if (string.IsNullOrWhiteSpace(cliente.Apellido))
                errores["apellido"] = "El Cliente debe tener un Apellido";

            if (!int.TryParse(cliente.Dni.ToString(), out int dni) || dni <= 0)
                errores["dni"] = "El Cliente debe tener un DNI valido";

            if (string.IsNullOrWhiteSpace(cliente.Email) || !EmailValido(cliente.Email))
                errores["email"] = "El Cliente debe tener un Email valido";

            if (string.IsNullOrWhiteSpace(cliente.Localidad.Nombre))
                errores["localidad"] = "El Cliente debe tener una Localidad";
            
            if (string.IsNullOrWhiteSpace(cliente.Localidad.Provincia.Nombre))
                errores["provincia"] = "El Cliente debe tener una Provincia";

            if (cliente.Telefono <= 0 || cliente.Telefono.ToString().Length < 6 || cliente.Telefono.ToString().Length > 15)
                errores["telefono"] = "El Cliente debe tener un Telefono valido";

            if (string.IsNullOrWhiteSpace(cliente.Tipo_Cliente.Nombre))
                errores["tipo"] = "El Cliente debe tener una Tipo";

            return errores;
        }

        /// <summary>
        /// Crea un nuevo cliente si los datos son válidos.
        /// </summary>
        /// <param name="cliente">Cliente a crear.</param>
        /// <param name="errores">Diccionario de errores de validación.</param>
        /// <returns>Id del cliente creado, o 0 si hay errores.</returns>
        public int CrearCliente(BE.Cliente cliente, out Dictionary<string, string> errores)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            errores = ValidarCliente(cliente);

            if (errores.Count == 0)
                return clienteDAL.CrearCliente(cliente, out _);
            else
                return 0;
        }

        /// <summary>
        /// Edita un cliente existente si los datos son válidos.
        /// </summary>
        /// <param name="cliente">Cliente a editar.</param>
        /// <param name="errores">Diccionario de errores de validación.</param>
        /// <returns>True si la edición fue exitosa, false en caso contrario.</returns>
        public bool EditarCliente(BE.Cliente cliente, out Dictionary<string, string> errores)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            errores = ValidarCliente(cliente);

            if (errores.Count == 0)
                return clienteDAL.EditarCliente(cliente, out _);
            else
                return false;
        }

        /// <summary>
        /// Elimina un cliente por su identificador.
        /// </summary>
        /// <param name="idCliente">Id del cliente a eliminar.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario.</returns>
        public bool EliminarCliente(int idCliente, out string mensaje)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            return clienteDAL.EliminarCliente(idCliente, out mensaje);
        }

        /// <summary>
        /// Obtiene la cantidad de registros asociados a un cliente.
        /// </summary>
        /// <param name="cliente_id">Id del cliente.</param>
        /// <returns>Cantidad de registros encontrados.</returns>
        public int cantidadCliente(int cliente_id)
        {
            DAL.Cliente cliente = new DAL.Cliente();

            return cliente.cantidadCliente(cliente_id);
        }

        /// <summary>
        /// Obtiene el cliente por su identificador.
        /// </summary>
        /// <param name="cliente_id">Id del cliente.</param>
        /// <returns>Objeto BE.Cliente correspondiente al id.</returns>
        public BE.Cliente dniCliente(int cliente_id)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();

            return clienteDAL.dniCliente(cliente_id);
        }

        public bool repiteDNI(long dni, int id)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            return clienteDAL.repiteDNI(dni, id);
        }

        public bool repiteEmail(string email, int id)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            return clienteDAL.repiteEmail(email, id);
        }

        public bool repiteTelefono(long telefono, int id)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            return clienteDAL.repiteTelfono(telefono, id);
        }

    }
}