using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Lote
    {
        private readonly Conexion conexion = new Conexion();

        /// <summary>
        /// Lista los lotes, opcionalmente filtrando por el identificador de insumo.
        /// </summary>
        /// <param name="idInsumo">Identificador del insumo para filtrar (opcional).</param>
        /// <returns>Lista de objetos <see cref="BE.Lote"/> recuperados de la base de datos.</returns>
        public List<BE.Lote> ListarLotes(int? idInsumo = null)
        {
            List<BE.Lote> lotes = new List<BE.Lote>();

            var parametros = new List<SqlParameter>();
            if (idInsumo.HasValue)
                parametros.Add(conexion.crearParametro("@IdInsumo", idInsumo.Value));

            DataTable dt = conexion.LeerPorStoreProcedure("sp_ListarLotes", parametros.ToArray());

            foreach (DataRow fila in dt.Rows)
            {
                BE.Lote unLote = new BE.Lote
                {
                    Id = Convert.ToInt32(fila["Id"]),
                    InsumoId = Convert.ToInt32(fila["insumo_id"]),
                    Cantidad = Convert.ToInt32(fila["Cantidad"]),
                    FechaDeVencimiento = Convert.ToDateTime(fila["fecha_vencimiento"])
                };

                lotes.Add(unLote);
            }

            return lotes;
        }

        /// <summary>
        /// Inserta un nuevo lote en la base de datos.
        /// </summary>
        /// <param name="objLote">Objeto <see cref="BE.Lote"/> a insertar.</param>
        /// <exception cref="ArgumentNullException">Si el objeto lote es nulo.</exception>
        /// <exception cref="Exception">Si ocurre un error al crear los parámetros o al insertar el lote.</exception>
        public void Insertar(BE.Lote objLote)
        {
            try
            {
                if (objLote == null) throw new ArgumentNullException(nameof(objLote));

                var pIdInsumo = conexion.crearParametro("@IdInsumo", objLote.InsumoId);
                var pCantidad = conexion.crearParametro("@Cantidad", objLote.Cantidad);
                var pFecha = conexion.crearParametro("@FechaVencimiento", objLote.FechaDeVencimiento);

                if (pIdInsumo == null || pCantidad == null || pFecha == null)
                    throw new Exception("Un parámetro creado es nulo.");

                conexion.EscribirPorStoreProcedure("sp_InsertarLote", new SqlParameter[] { pIdInsumo, pCantidad, pFecha });
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de base de datos al insertar el lote", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al insertar el lote", ex);
            }
        }



        /// <summary>
        /// Edita un lote existente en la base de datos.
        /// </summary>
        /// <param name="objLote">Objeto <see cref="BE.Lote"/> con los datos actualizados.</param>
        public void Editar(BE.Lote objLote)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@Id", objLote.Id),
                conexion.crearParametro("@IdInsumo", objLote.InsumoId),
                conexion.crearParametro("@Cantidad", objLote.Cantidad),
                conexion.crearParametro("@FechaVencimiento", objLote.FechaDeVencimiento)
            };

            conexion.EscribirPorStoreProcedure("sp_EditarLote", parametros);
        }

        /// <summary>
        /// Elimina un lote de la base de datos por su identificador.
        /// </summary>
        /// <param name="id">Identificador del lote a eliminar.</param>
        public void Eliminar(int id)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@Id", id)
            };

            conexion.EscribirPorStoreProcedure("sp_EliminarLote", parametros);
        }
    }
}
