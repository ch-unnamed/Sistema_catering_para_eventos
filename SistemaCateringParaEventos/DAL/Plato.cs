using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BE;
using System.Linq;

namespace DAL
{
    public class Plato
    {
        private readonly Conexion objConexion;

        public Plato()
        {
            objConexion = new Conexion();
        }

        /// <summary>
        /// Crea un nuevo plato en la base de datos utilizando un procedimiento almacenado.
        /// </summary>
        /// <param name="plato">Objeto BE.Plato con los datos del plato a crear.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>El identificador del plato creado, o 0 si hay error de validación.</returns>
        public int CrearPlato(BE.Plato plato, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

            string idsInsumos = string.Join(",", plato.Insumos.Select(i => i.Id));

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", plato.Nombre),
                new SqlParameter("@precio", plato.Precio),
                new SqlParameter("@descripcion", plato.Descripcion),
                new SqlParameter("@insumos_ids", idsInsumos),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            string nombreLimpio = plato.Nombre.Trim();
            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_crear_plato", parametros);

            mensaje = parametros[4].Value.ToString();
            resultado = Convert.ToInt32(parametros[5].Value);

            return resultado;
        }

        /// <summary>
        /// Edita un plato existente en la base de datos utilizando un procedimiento almacenado.
        /// </summary>
        /// <param name="plato">Objeto BE.Plato con los datos actualizados del plato.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>El identificador del plato editado, o 0 si hay error de validación.</returns>
        public int EditarPlato(BE.Plato plato, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

            string idsInsumos = string.Join(",", plato.Insumos.Select(i => i.Id));

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id_plato", plato.Id),
                new SqlParameter("@nombre", plato.Nombre),
                new SqlParameter("@precio", plato.Precio),
                new SqlParameter("@descripcion", plato.Descripcion),
                new SqlParameter("@insumos_ids", idsInsumos),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_editar_plato", parametros);

            mensaje = parametros[5].Value.ToString();
            resultado = Convert.ToInt32(parametros[6].Value);

            return resultado;

        }

        /// <summary>
        /// Elimina un plato de la base de datos utilizando un procedimiento almacenado.
        /// </summary>
        /// <param name="idPlato">Identificador del plato a eliminar.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
        public bool EliminarPlato(int idPlato, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                SqlParameter[] parametrosSql = new SqlParameter[]
                {
                    new SqlParameter("@Id", idPlato),
                    new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                    new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
                };

                int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_eliminar_plato", parametrosSql);

                mensaje = parametrosSql[1].Value.ToString();
                resultado = Convert.ToBoolean(parametrosSql[2].Value);

                return resultado;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    mensaje = "¡El Plato tiene vinculos activos con un Menu!";
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
        /// Lista todos los platos disponibles en la base de datos.
        /// </summary>
        /// <returns>Lista de objetos BE.Plato.</returns>
        public List<BE.Plato> Listar()
        {
            Conexion conexion = new Conexion();

            List<BE.Plato> platos = new List<BE.Plato>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_platos");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Plato unPlato = new BE.Plato();
                unPlato.Id = Convert.ToInt32(fila["id"]);
                unPlato.Nombre = fila["nombre"].ToString();
                unPlato.Precio = Convert.ToInt32(fila["precio"]);
                unPlato.Descripcion = fila["descripcion"].ToString();
                unPlato.FechaDeCreacion = Convert.ToDateTime(fila["fechaDeCreacion"]);

                platos.Add(unPlato);
            }

            return platos;
        }

        /// <summary>
        /// Inserta los platos seleccionados en una cotización personalizada.
        /// </summary>
        /// <param name="eventoId">ID del evento asociado.</param>
        /// <param name="clienteId">ID del cliente asociado.</param>
        /// <param name="menu_platos">Lista de objetos BE.Menu_Plato a insertar.</param>
        /// <param name="fechaRealizacion">Fecha de realización de la cotización.</param>
        /// <param name="total">Total de la cotización.</param>
        /// <param name="estado_id">ID del estado de la cotización.</param>
        /// <param name="vendedor_id">ID del vendedor responsable.</param>
        /// <returns>El identificador de la cotización creada.</returns>
        public int InsertarPlatosCotizacion(int eventoId, int clienteId, List<BE.Menu_Plato> menu_platos, DateTime fechaRealizacion, decimal total, int estado_id, int vendedor_id)
        {
            Conexion conexion = new Conexion();

            DataTable dtMenuPlatos = new DataTable();
            dtMenuPlatos.Columns.Add("menu_id", typeof(int));
            dtMenuPlatos.Columns.Add("plato_id", typeof(int));

            foreach (var item in menu_platos)
            {
                dtMenuPlatos.Rows.Add(item.Menu.Id, item.Plato.Id);
            }

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@evento_id", eventoId),
                new SqlParameter("@cliente_id", clienteId),
                new SqlParameter("@menu_platos", SqlDbType.Structured)
                {
                    TypeName = "MenuConPlatos",
                    Value = dtMenuPlatos
                },
                new SqlParameter("@fechaRealizacion", fechaRealizacion),
                new SqlParameter("@total", total),
                new SqlParameter("@estado_id", estado_id),
                new SqlParameter("@vendedor_id", vendedor_id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("CrearCotizacionMenuPersonalizado", parametros);

            int idCotizacion = 0;

            if (dt.Rows.Count > 0)
            {
                idCotizacion = Convert.ToInt32(dt.Rows[0]["CotizacionGenerada"]);
            }

            return idCotizacion;
        }

        public BE.Plato ConsultarPlato(int idPlato)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idPlato", idPlato)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_consultar_nombre_precio_plato", parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow fila = dt.Rows[0];
                return new BE.Plato
                {
                    Nombre = Convert.ToString(fila["nombre"]),
                    Precio = Convert.ToDecimal(fila["precio"])
                };
            }

            return null; 
        }

    }
}
