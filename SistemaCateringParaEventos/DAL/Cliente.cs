using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class Cliente
    {
        /// <summary>
        /// Obtiene la lista de todos los clientes desde la base de datos.
        /// </summary>
        /// <returns>Lista de objetos BE.Cliente.</returns>
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
                unCliente.Telefono = Convert.ToInt64(fila["telefono"]);
                unCliente.Nombre = fila["nombre"].ToString();
                unCliente.Apellido = fila["apellido"].ToString();

                BE.Provincia provincia = new BE.Provincia();
                provincia.Nombre = fila["nombre_provincia"].ToString();

                BE.Localidad localidad = new BE.Localidad();
                localidad.Id = Convert.ToInt32(fila["localidad_id"]);
                localidad.Nombre = fila["localidad"].ToString();
                localidad.Provincia = provincia;
                unCliente.Localidad = localidad;

                BE.Tipo_Cliente tipo_cliente = new BE.Tipo_Cliente();
                tipo_cliente.Id_Tipo_Cliente = Convert.ToInt32(fila["tipo_cliente_id"]);
                tipo_cliente.Nombre = $"{fila["tipo_cliente_nombre"]}";
                unCliente.Tipo_Cliente = tipo_cliente;
                clientes.Add(unCliente);
            }

            return clientes;
        }

        /// <summary>
        /// Crea un nuevo cliente en la base de datos.
        /// </summary>
        /// <param name="cliente">Objeto BE.Cliente con los datos a insertar.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>Entero indicando el resultado de la operación.</returns>
        public int CrearCliente(BE.Cliente cliente, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

            SqlParameter[] parametrosSql = new SqlParameter[]
            {
                new SqlParameter("@dni", cliente.Dni),
                new SqlParameter("@email", cliente.Email),
                new SqlParameter("@localidad", cliente.Localidad.Nombre),
                new SqlParameter("@provincia", cliente.Localidad.Provincia.Nombre),
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
            mensaje = parametrosSql[8].Value.ToString();
            resultado = Convert.ToInt32(parametrosSql[9].Value);

            return resultado;
        }

        /// <summary>
        /// Edita los datos de un cliente existente en la base de datos.
        /// </summary>
        /// <param name="cliente">Objeto BE.Cliente con los datos actualizados.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>True si la edición fue exitosa, false en caso contrario.</returns>
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
                new SqlParameter("@localidad", cliente.Localidad.Nombre),
                new SqlParameter("@provincia", cliente.Localidad.Provincia.Nombre),
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
            mensaje = parametrosSql[9].Value.ToString();
            resultado = Convert.ToBoolean(parametrosSql[10].Value);

            return resultado;
        }

        /// <summary>
        /// Elimina un cliente de la base de datos.
        /// </summary>
        /// <param name="idCliente">ID del cliente a eliminar.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario.</returns>
        public bool EliminarCliente(int idCliente, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                SqlParameter[] parametrosSql = new SqlParameter[]
                {
                    new SqlParameter("@Id", idCliente),
                    new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                    new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
                };

                int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_eliminar_cliente", parametrosSql);

                mensaje = parametrosSql[1].Value.ToString();
                resultado = Convert.ToBoolean(parametrosSql[2].Value);

                return resultado;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Restriccion de FK
                {
                    mensaje = "¡El Cliente tiene vinculos activos con Cotizacion!";
                }
                else
                {
                    mensaje = "Error en la base de datos: " + ex.Message;
                }
                return false;
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Obtiene la cantidad de registros asociados a un cliente específico.
        /// </summary>
        /// <param name="cliente_id">ID del cliente.</param>
        /// <returns>Cantidad de registros encontrados.</returns>
        public int cantidadCliente(int cliente_id)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@cliente_id", cliente_id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_cantidad_cliente", parametros);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["cantidad"]);
            }
            return 0;
        }
        /// <summary>
        /// Obtiene el DNI de un cliente a partir de su ID.
        /// </summary>
        /// <param name="cliente_id">ID del cliente.</param>
        /// <returns>Objeto BE.Cliente con el DNI, o null si no se encuentra.</returns>
        public BE.Cliente dniCliente(int cliente_id)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@cliente_id", cliente_id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_dni_cliente", parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow fila = dt.Rows[0];

                return new BE.Cliente
                {
                    Dni = Convert.ToInt64(fila["dni"])
                };
            }
            return null;
        }

        public bool repiteDNI(long dni, int id)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@dni", dni)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_repite_dni", parametros);
            
            return dt.Rows.Count > 0 ? true : false;
            
        }

        public bool repiteEmail(string email, long id)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@email", email)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_repite_email", parametros);

            return dt.Rows.Count > 0 ? true : false;

        }

        public bool repiteTelfono(long telefono, long id)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@telefono", telefono)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_repite_telefono", parametros);

            return dt.Rows.Count > 0 ? true : false;

        }

        public List<BE.Cliente> nombreClientes(BE.Cliente cliente)
        {
            Conexion conexion = new Conexion();

            List<BE.Cliente> clientes = new List<BE.Cliente>();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombreCompleto", cliente.Nombre)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_nombre_cliente", parametros);

            foreach (DataRow fila in dt.Rows)
            {
                BE.Cliente unCliente = new BE.Cliente();

                unCliente.IdCliente = Convert.ToInt32(fila["id"]);
                unCliente.Nombre = fila["nombre"].ToString();
                unCliente.Apellido = fila["apellido"].ToString();

                clientes.Add(unCliente);
            }

            return clientes;
        }

        public List<BE.Cliente> nombreClientePorId(BE.Cliente cliente)
        {
            Conexion conexion = new Conexion();

            List<BE.Cliente> clientes = new List<BE.Cliente>();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idCliente", cliente.IdCliente)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_nombre_cliente_por_id", parametros);

            foreach (DataRow fila in dt.Rows)
            {
                BE.Cliente unCliente = new BE.Cliente();

                unCliente.IdCliente = Convert.ToInt32(fila["id"]);
                unCliente.Nombre = fila["nombre"].ToString();
                unCliente.Apellido = fila["apellido"].ToString();

                clientes.Add(unCliente);
            }

            return clientes;
        }

        public BE.Cliente idClienteIdEvento(int idEvento)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idEvento", idEvento)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_cliente_id_evento", parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow fila = dt.Rows[0];

                return new BE.Cliente
                {
                    IdCliente = Convert.ToInt32(fila["id"])
                };
            }
            return null;
        }
    }
}

