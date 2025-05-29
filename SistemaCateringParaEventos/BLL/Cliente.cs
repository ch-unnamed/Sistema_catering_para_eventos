using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Cliente
    {
        public List<BE.Cliente> Listar()
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            return clienteDAL.Listar();
        }

        public int CrearCliente(BE.Cliente cliente, out string mensaje)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            
            mensaje = string.Empty;

            if (cliente.Dni <= 0 || cliente.Dni.ToString().Length != 8)
            {
                mensaje = "El DNI debe contener exactamente 8 dígitos numéricos.";
            }
            else if (string.IsNullOrEmpty(cliente.Email) || string.IsNullOrWhiteSpace(cliente.Email))
            {
                mensaje = "El email no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(cliente.Region) || string.IsNullOrWhiteSpace(cliente.Region))
            {
                mensaje = "La región no puede estar vacía.";
            }
            else if (cliente.Telefono <= 0 || cliente.Telefono.ToString().Length < 6 || cliente.Telefono.ToString().Length > 15)
                {
                mensaje = "El teléfono debe contener entre 6 y 15 dígitos numéricos.";
            }
            else if (string.IsNullOrEmpty(cliente.Nombre) || string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                mensaje = "El nombre no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(cliente.Apellido) || string.IsNullOrWhiteSpace(cliente.Apellido))
            {
                mensaje = "El apellido no puede estar vacío.";
            }

            if(string.IsNullOrEmpty(mensaje))
            {
               // logica de mandar email
                return clienteDAL.CrearCliente(cliente, out mensaje);

            } else
            {
                return 0;
            }
                        
        }

        public bool EditarCliente(BE.Cliente cliente, out string mensaje)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();

            mensaje = string.Empty;

            if (cliente.Dni <= 0 || cliente.Dni.ToString().Length != 8)
            {
                mensaje = "El DNI debe contener exactamente 8 dígitos numéricos.";
            }
            else if (string.IsNullOrEmpty(cliente.Email) || string.IsNullOrWhiteSpace(cliente.Email))
            {
                mensaje = "El email no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(cliente.Region) || string.IsNullOrWhiteSpace(cliente.Region))
            {
                mensaje = "La región no puede estar vacía.";
            }
            else if (cliente.Telefono <= 0 || cliente.Telefono.ToString().Length < 6 || cliente.Telefono.ToString().Length > 15)
            {
                mensaje = "El teléfono debe contener entre 6 y 15 dígitos numéricos.";
            }
            else if (string.IsNullOrEmpty(cliente.Nombre) || string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                mensaje = "El nombre no puede estar vacío.";
            }
            else if (string.IsNullOrEmpty(cliente.Apellido) || string.IsNullOrWhiteSpace(cliente.Apellido))
            {
                mensaje = "El apellido no puede estar vacío.";
            }
            
            if (string.IsNullOrEmpty(mensaje))
            {
                return clienteDAL.EditarCliente(cliente, out mensaje);
            }
            else
            {
                return false;
            }
            
        }

        public bool EliminarCliente(int idCliente, out string mensaje)
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            return clienteDAL.EliminarCliente(idCliente, out mensaje);
        }
    }
}