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
    public class Temporada
    {

        private readonly Conexion conexion = new Conexion();
        public List<BE.Temporada> ListarTemporada()
        {
            Conexion conexion = new Conexion();

            List<BE.Temporada> temporadas = new List<BE.Temporada>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_temporada");

            foreach (DataRow row in dt.Rows)
            {
                BE.Temporada unaTemp = new BE.Temporada();

                unaTemp.IdTemporada = Convert.ToInt32(row["id"]);
                unaTemp.FechaComienzoTemp = Convert.ToDateTime(row["fecha_comienzo_temp"].ToString());
                unaTemp.FechaFin = Convert.ToDateTime(row["fecha_fin"].ToString());
                unaTemp.CantidadEvento = Convert.ToInt32(row["cantidad_evento"]);

                BE.Categoria_Temporada unaCategoriaTemp = new BE.Categoria_Temporada();
                unaCategoriaTemp.IdCategoriaTemporada = Convert.ToInt32(row["categoria_id"]);
                unaCategoriaTemp.Nombre = row["nombre_categoria"].ToString();
                unaTemp.Id_CategoriaTemporada = unaCategoriaTemp;


                temporadas.Add(unaTemp);
            }

            return temporadas;
        }
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
