﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Cliente
    {
        public List<BE.Cliente> Listar()
        {
            Conexion conexion = new Conexion();

            List<BE.Cliente> clientes = new List<BE.Cliente>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_clientes");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Cliente unCliente = new BE.Cliente();

                unCliente.IdCliente = Convert.ToInt32(fila["cliente_id"]);
                unCliente.Dni = Convert.ToInt64(fila["dni"]);
                unCliente.Email = fila["email"].ToString();
                unCliente.Region = fila["region"].ToString();
                unCliente.Telefono = Convert.ToInt64(fila["telefono"]);
                unCliente.Nombre = fila["nombre"].ToString();
                unCliente.Apellido = fila["apellido"].ToString();

                // Crear instancia de tipocliente para obtener el ID
                BE.Tipo_Cliente tipo_cliente = new BE.Tipo_Cliente();
                tipo_cliente.Id_Tipo_Cliente = Convert.ToInt32(fila["tipo_cliente_id"]);
                tipo_cliente.Nombre = $"{fila["tipo_cliente_nombre"]}";
                unCliente.Tipo_Cliente = tipo_cliente;
                clientes.Add(unCliente);
            }

            return clientes;
        }

        public int CrearCliente(BE.Cliente cliente, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

            SqlParameter[] parametrosSql = new SqlParameter[]
            {
                new SqlParameter("@dni", cliente.Dni),
                new SqlParameter("@email", cliente.Email),
                new SqlParameter("@region", cliente.Region),
                new SqlParameter("@telefono", cliente.Telefono),
                new SqlParameter("@nombre", cliente.Nombre),
                new SqlParameter("@apellido", cliente.Apellido),
                new SqlParameter("@tipo_cliente", cliente.Tipo_Cliente.Nombre),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            // Ejecutamos el procedimiento almacenado
            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_crear_cliente", parametrosSql);

            // Capturamos los valores de salida
            mensaje = parametrosSql[7].Value.ToString();
            resultado = Convert.ToInt32(parametrosSql[8].Value);

            return resultado;
        }

        public bool EditarCliente(BE.Cliente cliente, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            SqlParameter[] parametrosSql = new SqlParameter[]
            {
                new SqlParameter("@id", cliente.IdCliente),
                new SqlParameter("@dni", cliente.Dni),
                new SqlParameter("@email", cliente.Email),
                new SqlParameter("@region", cliente.Region),
                new SqlParameter("@telefono", cliente.Telefono),
                new SqlParameter("@nombre", cliente.Nombre),
                new SqlParameter("@apellido", cliente.Apellido),
                new SqlParameter("@tipo_cliente", cliente.Tipo_Cliente.Nombre),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
            };

            // Ejecutamos el procedimiento almacenado
            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_editar_cliente", parametrosSql);

            // Capturamos los valores de salida
            mensaje = parametrosSql[8].Value.ToString();
            resultado = Convert.ToBoolean(parametrosSql[9].Value);

            return resultado;
        }

        public bool EliminarCliente(int idCliente, out string mensaje)
        {
            Conexion conexion = new Conexion();
            bool resultado = false;
            mensaje = string.Empty;
            SqlParameter[] parametrosSql = new SqlParameter[]
            {
                new SqlParameter("@Id", idCliente),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
            };
            // Ejecutamos el procedimiento almacenado
            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_eliminar_cliente", parametrosSql);
            // Capturamos los valores de salida
            mensaje = parametrosSql[1].Value.ToString();
            resultado = Convert.ToBoolean(parametrosSql[2].Value);
            return resultado;
        }
    }
}
