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
    public class Evento
    {
        public List<BE.Evento> Listar()
        {
            Conexion conexion = new Conexion();

            List<BE.Evento> eventos = new List<BE.Evento>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_eventos");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Evento unEvento = new BE.Evento();

                // tomar cada fila como se usa en el procedimiento almacenado
                unEvento.IdEvento = Convert.ToInt32(fila["evento_id"]);
                unEvento.Nombre = fila["evento_nombre"].ToString();
                unEvento.Capacidad = Convert.ToInt32(fila["capacidad"]);
                unEvento.Fecha = Convert.ToDateTime(fila["fecha"].ToString());

                BE.Ubicacion ubicacion_id= new BE.Ubicacion();
                ubicacion_id.IdUbicacion = Convert.ToInt32(fila["ubicacion_id"]);
                ubicacion_id.Calle = $"{fila["ubicacion_calle"]}";
                ubicacion_id.Altura = Convert.ToInt32(fila["ubicacion_altura"]);
                ubicacion_id.Ciudad = $"{fila["ubicacion_ciudad"]}";
                ubicacion_id.Provincia = $"{fila["ubicacion_provincia"]}";
                unEvento.Ubicacion = ubicacion_id;

                // Crear instancia de cada clase individual para obtener el ID
                BE.Tipo_Evento tipo_evento_nombre = new BE.Tipo_Evento();
                tipo_evento_nombre.Nombre = $"{fila["tipo_evento_nombre"]}";
                unEvento.Tipo_Evento = tipo_evento_nombre;

                eventos.Add(unEvento);
            }

            return eventos;
        }

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

            // Capturar los valores de salida
            mensaje = parametros[8].Value.ToString();
            resultado = Convert.ToInt32(parametros[9].Value);

            return resultado;
        }

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
            // Ejecupar el proc.almc
            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_editar_evento", parametros);

            // Capturar los valores de salida
            mensaje = parametros[9].Value.ToString();
            resultado = Convert.ToBoolean(parametros[10].Value);

            return resultado;
        }

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

                // Ejecutar el stored procedure
                int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_eliminar_evento", parametros);

                // Capturar valores de salida
                mensaje = parametros[1].Value.ToString();
                resultado = Convert.ToBoolean(parametros[2].Value);

                return resultado;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Restriccion de FK
                {
                    mensaje = "¡El evento tiene vinculos activos con Cotizacion!";
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
                    Capacidad = Convert.ToInt32(fila["capacidad"])
                };
            }

            return null; // Si no hay datos, devuelve null
        }


        public void InsertarPlatosCotizacion(int cotizacionId, int menuId, List<BE.Plato> platos)
        {
            Conexion conexion = new Conexion();

            // 1. Crear DataTable con los IDs de platos
            DataTable dtPlatos = new DataTable();
            dtPlatos.Columns.Add("plato_id", typeof(int));

            foreach (var plato in platos)
            {
                dtPlatos.Rows.Add(plato.Id);
            }

            // 2. Crear parámetros
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

            // 3. Ejecutar SP
            conexion.EscribirPorStoreProcedure("CrearCotizacionMenuPersonalizado", parametros);
        }

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
    }
}