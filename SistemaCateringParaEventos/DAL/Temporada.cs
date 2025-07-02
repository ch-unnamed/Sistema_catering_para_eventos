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
    /// Proporciona métodos de acceso a datos para la entidad Temporada.
    /// </summary>
    public class Temporada
    {
        private readonly Conexion conexion = new Conexion();

        /// <summary>
        /// Obtiene una lista de todas las temporadas desde la base de datos.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Temporada"/>.</returns>
        public List<BE.Temporada> ListarTemporada()
        {
            Conexion conexion = new Conexion();
            List<BE.Temporada> temporadas = new List<BE.Temporada>();
            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_temporada");

            foreach (DataRow row in dt.Rows)
            {
                BE.Temporada unaTemp = new BE.Temporada
                {
                    IdTemporada = Convert.ToInt32(row["id"]),
                    FechaComienzoTemp = Convert.ToDateTime(row["fecha_comienzo_temp"].ToString()),
                    FechaFin = Convert.ToDateTime(row["fecha_fin"].ToString()),
                    CantidadEvento = Convert.ToInt32(row["cantidad_evento"]),
                    Id_CategoriaTemporada = new BE.Categoria_Temporada
                    {
                        IdCategoriaTemporada = Convert.ToInt32(row["categoria_id"]),
                        Nombre = row["nombre_categoria"].ToString()
                    }
                };

                temporadas.Add(unaTemp);
            }

            return temporadas;
        }

        /// <summary>
        /// Obtiene una temporada específica por su identificador.
        /// </summary>
        /// <param name="idTemporada">Identificador de la temporada.</param>
        /// <returns>Objeto <see cref="BE.Temporada"/> correspondiente al identificador, o null si no existe.</returns>
        public BE.Temporada ObtenerTemporadaPorId(int idTemporada)
        {
            var entrada = new SqlParameter[]
            {
                    conexion.crearParametro("@IdTemporada", idTemporada)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_obtener_temporada_por_id", entrada);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            BE.Temporada unaTemp = new BE.Temporada
            {
                IdTemporada = Convert.ToInt32(row["id"]),
                FechaComienzoTemp = Convert.ToDateTime(row["fecha_comienzo_temp"]),
                FechaFin = Convert.ToDateTime(row["fecha_fin"]),
                CantidadEvento = Convert.ToInt32(row["cantidad_evento"]),
                Id_CategoriaTemporada = new BE.Categoria_Temporada
                {
                    IdCategoriaTemporada = Convert.ToInt32(row["categoria_id"]),
                    Nombre = row["nombre_categoria"].ToString()
                }
            };

            return unaTemp;
        }

        /// <summary>
        /// Compara la cantidad de eventos entre dos temporadas.
        /// </summary>
        /// <param name="id1">Identificador de la primera temporada.</param>
        /// <param name="id2">Identificador de la segunda temporada.</param>
        /// <returns>Lista de objetos <see cref="BE.Temporada"/> con los resultados de la comparación.</returns>
        public List<BE.Temporada> CompararTemporadas(int id1, int id2)
        {
            var parametros = new SqlParameter[]
            {
                    conexion.crearParametro("@idTemp1", id1),
                    conexion.crearParametro("@idTemp2", id2)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_comparar_ventas_temporadas", parametros);
            List<BE.Temporada> temporadas = new List<BE.Temporada>();

            foreach (DataRow row in dt.Rows)
            {
                var temp = new BE.Temporada
                {
                    IdTemporada = Convert.ToInt32(row["IdTemporada"]),
                    CantidadEvento = Convert.ToInt32(row["CantidadEvento"]),
                    Id_CategoriaTemporada = new BE.Categoria_Temporada
                    {
                        Nombre = row["NombreCategoria"].ToString()
                    }
                };

                temporadas.Add(temp);
            }

            return temporadas;
        }

        /// <summary>
        /// Compara la cantidad de eventos de un tipo específico entre dos temporadas.
        /// </summary>
        /// <param name="id1">Identificador de la primera temporada.</param>
        /// <param name="id2">Identificador de la segunda temporada.</param>
        /// <param name="tipo">Identificador del tipo de evento.</param>
        /// <returns>Lista de objetos <see cref="BE.Temporada"/> con los resultados de la comparación por tipo de evento.</returns>
        public List<BE.Temporada> CompararTemporadasPorTipoEvento(int id1, int id2, int tipo)
        {
            var parametros = new SqlParameter[]
            {
                    conexion.crearParametro("@idTemp1", id1),
                    conexion.crearParametro("@idTemp2", id2),
                    conexion.crearParametro("@idTipoEvento", tipo)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_comparar_tipo_evento_temporadas", parametros);
            List<BE.Temporada> temporadas = new List<BE.Temporada>();

            foreach (DataRow row in dt.Rows)
            {
                var temp = new BE.Temporada
                {
                    IdTemporada = Convert.ToInt32(row["IdTemporada"]),
                    CantidadEvento = Convert.ToInt32(row["CantidadEvento"])
                };

                temporadas.Add(temp);
            }

            return temporadas;
        }
    }
}
