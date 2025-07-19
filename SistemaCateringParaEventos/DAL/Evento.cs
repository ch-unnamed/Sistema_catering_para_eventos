using BE;
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
    /// Clase de acceso a datos para la gestión de eventos.
    /// Proporciona métodos para listar, crear, editar, eliminar y consultar eventos en la base de datos.
    /// </summary>
    public class Evento
    {
        /// <summary>
        /// Lista todos los eventos disponibles en la base de datos.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Evento"/>.</returns>
        public List<BE.Evento> Listar()
        {
            Conexion conexion = new Conexion();
            List<BE.Evento> eventos = new List<BE.Evento>();
            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_eventos");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Evento unEvento = new BE.Evento();

                unEvento.IdEvento = Convert.ToInt32(fila["evento_id"]);
                unEvento.Nombre = fila["evento_nombre"].ToString();
                unEvento.Capacidad = Convert.ToInt32(fila["capacidad"]);
                unEvento.Fecha = Convert.ToDateTime(fila["fecha"].ToString());

                BE.Ubicacion ubicacion_id = new BE.Ubicacion();
                ubicacion_id.IdUbicacion = Convert.ToInt32(fila["ubicacion_id"]);
                ubicacion_id.Calle = $"{fila["ubicacion_calle"]}";
                ubicacion_id.Altura = Convert.ToInt32(fila["ubicacion_altura"]);
                ubicacion_id.Ciudad = $"{fila["ubicacion_ciudad"]}";
                ubicacion_id.Provincia = $"{fila["ubicacion_provincia"]}";
                unEvento.Ubicacion = ubicacion_id;

                BE.Tipo_Evento tipo_evento_nombre = new BE.Tipo_Evento();
                tipo_evento_nombre.Nombre = $"{fila["tipo_evento_nombre"]}";
                unEvento.Tipo_Evento = tipo_evento_nombre;

                eventos.Add(unEvento);
            }

            return eventos;
        }

        /// <summary>
        /// Crea un nuevo evento en la base de datos.
        /// </summary>
        /// <param name="evento">Objeto <see cref="BE.Evento"/> con los datos del evento a crear.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>El identificador del evento creado, o 0 si hay error.</returns>
        public int CrearEvento(BE.Evento evento, out string mensaje)
        {
            Conexion conexion = new Conexion();

            int resultado = 0;
            mensaje = string.Empty;

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", evento.Nombre),
                new SqlParameter("@capacidad", evento.Capacidad),
                new SqlParameter("@tipo", evento.Tipo_Evento.Nombre),
                new SqlParameter("@calle", evento.Ubicacion.Calle),
                new SqlParameter("@altura", evento.Ubicacion.Altura),
                new SqlParameter("@ciudad", evento.Ubicacion.Ciudad),
                new SqlParameter("@provincia", evento.Ubicacion.Provincia),
                new SqlParameter("@fecha", evento.Fecha),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            // Ejecupar el proc.alm
            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_crear_evento", parametros);

            mensaje = parametros[8].Value.ToString();
            resultado = Convert.ToInt32(parametros[9].Value);


            return resultado;
        }


        /// <summary>
        /// Edita los datos de un evento existente en la base de datos.
        /// </summary>
        /// <param name="evento">Objeto <see cref="BE.Evento"/> con los datos actualizados.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>True si la edición fue exitosa, false en caso contrario.</returns>
        public bool EditarEvento(BE.Evento evento, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Id", evento.IdEvento),
                new SqlParameter("@Nombre", evento.Nombre),
                new SqlParameter("@Capacidad", evento.Capacidad),
                new SqlParameter("@Tipo", evento.Tipo_Evento.Nombre),
                new SqlParameter("@Calle", evento.Ubicacion.Calle),
                new SqlParameter("@Altura", evento.Ubicacion.Altura),
                new SqlParameter("@Ciudad", evento.Ubicacion.Ciudad),
                new SqlParameter("@Provincia", evento.Ubicacion.Provincia),
                new SqlParameter("@Fecha", evento.Fecha),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_editar_evento", parametros);

            mensaje = parametros[9].Value.ToString();
            resultado = Convert.ToBoolean(parametros[10].Value);

            return resultado;
        }

        /// <summary>
        /// Elimina un evento de la base de datos.
        /// </summary>
        /// <param name="idEvento">ID del evento a eliminar.</param>
        /// <param name="mensaje">Mensaje de resultado de la operación.</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario.</returns>
        public bool EliminarEvento(int idEvento, out string mensaje)
        {
            Conexion conexion = new Conexion();

            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@IdEvento", idEvento),
                    new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                    new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
                };

                int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_eliminar_evento", parametros);

                mensaje = parametros[1].Value.ToString();
                resultado = Convert.ToBoolean(parametros[2].Value);

                return resultado;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    mensaje = "¡El evento tiene vínculos activos con Cotización!";
                }
                else
                {
                    mensaje = "Error en la base de datos: " + ex.Message;
                }
                return false;
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Obtiene la capacidad de un evento por su ID.
        /// </summary>
        /// <param name="eventoId">ID del evento.</param>
        /// <returns>Objeto <see cref="BE.Evento"/> con la capacidad, o null si no se encuentra.</returns>
        public BE.Evento ObtenerCapacidadPorIdEvento(int eventoId)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@evento_id", eventoId)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("ObtenerCapacidadEvento", parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow fila = dt.Rows[0];
                return new BE.Evento
                {
                    IdEvento = Convert.ToInt32(fila["id"]),
                    Capacidad = Convert.ToInt32(fila["capacidad"]),
                    Fecha = Convert.ToDateTime(fila["fecha"])
                };
            }

            return null;
        }

        /// <summary>
        /// Inserta los platos seleccionados en una cotización personalizada.
        /// </summary>
        /// <param name="cotizacionId">ID de la cotización.</param>
        /// <param name="menuId">ID del menú base.</param>
        /// <param name="platos">Lista de platos a insertar.</param>
        public void InsertarPlatosCotizacion(int cotizacionId, int menuId, List<BE.Plato> platos)
        {
            Conexion conexion = new Conexion();

            DataTable dtPlatos = new DataTable();
            dtPlatos.Columns.Add("plato_id", typeof(int));

            foreach (var plato in platos)
            {
                dtPlatos.Rows.Add(plato.Id);
            }

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@cotizacion_id", cotizacionId),
                new SqlParameter("@menu_base_id", menuId),
                new SqlParameter("@platos", SqlDbType.Structured)
                {
                    TypeName = "PlatoIdList",
                    Value = dtPlatos
                }
            };

            conexion.EscribirPorStoreProcedure("CrearCotizacionMenuPersonalizado", parametros);
        }

        /// <summary>
        /// Obtiene la cantidad de un evento por su ID.
        /// </summary>
        /// <param name="evento_id">ID del evento.</param>
        /// <returns>Cantidad del evento.</returns>
        public int cantidadEvento(int evento_id)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@evento_id", evento_id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_cantidad_evento", parametros);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["cantidad"]);
            }
            return 0;
        }

        /// <summary>
        /// Obtiene el nombre de un evento por su ID.
        /// </summary>
        /// <param name="evento_id">ID del evento.</param>
        /// <returns>Nombre del evento, o cadena vacía si no se encuentra.</returns>
        public string nombreEvento(int evento_id)
        {
            Conexion conexion = new Conexion();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@evento_id", evento_id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_nombre_evento", parametros);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0]["nombre"]);
            }
            return "";
        }

        public List<BE.Evento> ListarPorUbicacion()
        {
            Conexion conexion = new Conexion();

            List<BE.Evento> eventos = new List<BE.Evento>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_ubicacion_eventos");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Evento unEvento = new BE.Evento();

                // tomar cada fila como se usa en el procedimiento almacenado
                unEvento.IdEvento = Convert.ToInt32(fila["id"]);
                unEvento.Nombre = fila["nombre"].ToString();

                BE.Ubicacion ubicacion = new BE.Ubicacion();
                ubicacion.Calle = $"{fila["Calle"]}";
                ubicacion.Altura = Convert.ToInt32(fila["Altura"]);
                ubicacion.Ciudad = $"{fila["Ciudad"]}";
                ubicacion.Provincia = $"{fila["Provincia"]}";
                unEvento.Ubicacion = ubicacion;

                eventos.Add(unEvento);
            }

            return eventos;
        }

    }
}