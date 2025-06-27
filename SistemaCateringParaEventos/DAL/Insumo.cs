using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Insumo
    {
        private readonly Conexion conexion = new Conexion();

        public List<BE.Insumo> ListarInsumos(string nombre = null, string tipo = null)
        {
            Conexion conexion = new Conexion();

            List<BE.Insumo> Insumos = new List<BE.Insumo>();

            DataTable dt = conexion.LeerPorStoreProcedure("sp_listar_Insumo");

            foreach (DataRow fila in dt.Rows)
            {
                BE.Insumo unInsumo = new BE.Insumo();

                unInsumo.Id = Convert.ToInt32(fila["id"]);
                unInsumo.Nombre = fila["nombre"].ToString();
                unInsumo.Unidad = Convert.ToInt32(fila["unidad"]);
                unInsumo.TipoId = Convert.ToInt32(fila["tipo_insumo_id"]);
                unInsumo.TipoNombre = fila["tipo_nombre"].ToString();
                unInsumo.Costo = Convert.ToInt32(fila["costo"]);
                unInsumo.StockMinimo = Convert.ToInt32(fila["stock_minimo"]);

                Insumos.Add(unInsumo);
            }

            return Insumos;
        }

        public int Insertar(BE.Insumo objInsumo)
        {
            var parametros = new SqlParameter[]
            {
        conexion.crearParametro("@Nombre", objInsumo.Nombre),
        conexion.crearParametro("@TipoInsumoId", objInsumo.TipoId),
        conexion.crearParametro("@Unidades", objInsumo.Unidad),
        conexion.crearParametro("@StockMinimo", objInsumo.StockMinimo),
        conexion.crearParametro("@Costo", objInsumo.Costo)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_InsertarInsumo", parametros);

            if (dt.Rows.Count == 0)
                throw new Exception("No se pudo obtener el Id del insumo insertado.");

            return Convert.ToInt32(dt.Rows[0][0]);
        }



        public void Editar(BE.Insumo objInsumo)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@Id", objInsumo.Id),
                conexion.crearParametro("@Nombre", objInsumo.Nombre),
                conexion.crearParametro("@TipoInsumoId", objInsumo.TipoId),
                conexion.crearParametro("@StockMinimo", objInsumo.StockMinimo),
                conexion.crearParametro("@Costo", objInsumo.Costo)
            };

            conexion.EscribirPorStoreProcedure("sp_EditarInsumo", parametros);
        }

        public void Eliminar(int id)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@Id", id)
            };

            conexion.EscribirPorStoreProcedure("sp_Insumo_Eliminar", parametros);
        }

        public BE.Insumo Buscar(string nombreInsumo)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@Nombre", nombreInsumo)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_BuscarInsumoPorNombre", parametros);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new BE.Insumo
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                TipoId = Convert.ToInt32(row["tipo_insumo_id"]),
                StockMinimo = Convert.ToInt32(row["StockMinimo"]),
                //FechaDeCreacion = Convert.ToDateTime(row["FechaDeCreacion"])
            };
        }

        public void DescontarStockEnLotes(int idInsumo, int cantidadAUsar)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@IdInsumo", idInsumo),
                conexion.crearParametro("@CantidadAUsar", cantidadAUsar)
            };

            try
            {
                conexion.EscribirPorStoreProcedure("sp_DescontarStockEnLotes", parametros);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("No hay stock suficiente"))
                    throw new Exception("Stock insuficiente para realizar la operación.");

                throw;
            }
        }

    }
}
