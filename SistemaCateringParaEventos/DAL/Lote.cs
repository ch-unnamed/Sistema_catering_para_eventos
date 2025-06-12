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
                    InsumoId = Convert.ToInt32(fila["IdInsumo"]),
                    Cantidad = Convert.ToInt32(fila["Cantidad"]),
                    FechaDeVencimiento = Convert.ToDateTime(fila["FechaVencimiento"])
                };

                lotes.Add(unLote);
            }

            return lotes;
        }

        public void Insertar(BE.Lote objLote)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@IdInsumo", objLote.InsumoId),
                conexion.crearParametro("@Cantidad", objLote.Cantidad),
                conexion.crearParametro("@FechaVencimiento", objLote.FechaDeVencimiento)
            };

            conexion.EscribirPorStoreProcedure("sp_InsertarLote", parametros);
        }

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
