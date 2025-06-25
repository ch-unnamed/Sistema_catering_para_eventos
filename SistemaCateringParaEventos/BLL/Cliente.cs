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
    public class Cliente
    {
        public List<BE.Cliente> Listar()
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            return clienteDAL.Listar();
        }

        // validar email con MailAddres
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

            if (string.IsNullOrWhiteSpace(cliente.Region))
                errores["region"] = "El Cliente debe tener una Region";

            if (cliente.Telefono <= 0 || cliente.Telefono.ToString().Length < 6 || cliente.Telefono.ToString().Length > 15)
                errores["telefono"] = "El Cliente debe tener un Telefono valido";

            if (string.IsNullOrWhiteSpace(cliente.Tipo_Cliente.Nombre))
                errores["tipo"] = "El Cliente debe tener una Tipo";

            return errores;
        }

        public int CrearCliente(BE.Cliente cliente, out Dictionary<string, string> errores)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            errores = ValidarCliente(cliente);

            if(errores.Count == 0)
                return clienteDAL.CrearCliente(cliente, out _);
            else
                return 0;
        }

        public bool EditarCliente(BE.Cliente cliente, out Dictionary<string, string> errores)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            errores = ValidarCliente(cliente);

            if (errores.Count == 0)
                return clienteDAL.EditarCliente(cliente, out _);
            else
                return false;
        }

        public bool EliminarCliente(int idCliente, out string mensaje)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            return clienteDAL.EliminarCliente(idCliente, out mensaje);
        }

        public int cantidadCliente(int cliente_id)
        {
            DAL.Cliente cliente = new DAL.Cliente();

            return cliente.cantidadCliente(cliente_id);
        }

        public BE.Cliente dniCliente(int cliente_id)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();

            return clienteDAL.dniCliente(cliente_id);
        }
    }
}