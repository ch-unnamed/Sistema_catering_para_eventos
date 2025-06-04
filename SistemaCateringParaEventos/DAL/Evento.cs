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

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdEvento", idEvento),
                new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output },
                new SqlParameter("@Resultado", SqlDbType.Bit) { Direction = ParameterDirection.Output }
            };

            // Ejecupar el proc.almc
            int filasAfectadas = conexion.EscribirPorStoreProcedure("sp_eliminar_evento", parametros);

            // Capturar los valores de salida
            mensaje = parametros[1].Value.ToString();
            resultado = Convert.ToBoolean(parametros[2].Value);

            return resultado;
        }
    }
}