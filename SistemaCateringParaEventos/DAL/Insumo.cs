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

                // tomar cada fila como se usa en el procedimiento almacenado

                unInsumo.Id = Convert.ToInt32(fila["id"]);
                unInsumo.Nombre = fila["nombre"].ToString();
                unInsumo.Unidades = Convert.ToInt32(fila["unidad"]);
                unInsumo.Tipo = fila["tipo"].ToString();
                unInsumo.Costo = Convert.ToInt32(fila["costo"]);
                unInsumo.StockMinimo = Convert.ToInt32(fila["stock_minimo"]);

                Insumos.Add(unInsumo);
            }

            return Insumos;
        }

        public void Insertar(BE.Insumo objInsumo)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@Nombre", objInsumo.Nombre),
                conexion.crearParametro("@Tipo", objInsumo.Tipo),
                conexion.crearParametro("@Unidades",objInsumo.Unidades),
                conexion.crearParametro("@StockMinimo", objInsumo.StockMinimo),
                conexion.crearParametro("@Costo", objInsumo.Costo)
            };

            conexion.EscribirPorStoreProcedure("sp_InsertarInsumo", parametros);
        }

        public void Editar(BE.Insumo objInsumo)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@Nombre", objInsumo.Nombre),
                conexion.crearParametro("@Tipo", objInsumo.Tipo),
                conexion.crearParametro("@Unidades",objInsumo.Unidades),
                conexion.crearParametro("@StockMinimo", objInsumo.StockMinimo),
                conexion.crearParametro("@Costo", objInsumo.Costo)
            };

            conexion.EscribirPorStoreProcedure("sp_EditarInsumo", parametros);
        }

        public void Eliminar(BE.Insumo objInsumo)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@Id", objInsumo.Id)
            };

            conexion.EscribirPorStoreProcedure("sp_EliminarInsumo", parametros);
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
                Tipo = row["Tipo"].ToString(),
                StockMinimo = Convert.ToInt32(row["StockMinimo"]),
                //FechaDeCreacion = Convert.ToDateTime(row["FechaDeCreacion"])
            };
        }
    }
}
