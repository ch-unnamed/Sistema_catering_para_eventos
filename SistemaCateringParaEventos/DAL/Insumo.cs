using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Insumo
    {
        private readonly Conexion conexion = new Conexion();

        /// <summary>
        /// Lista todos los insumos disponibles en la base de datos.
        /// Utiliza el procedimiento almacenado <c>sp_listar_Insumo</c>.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Insumo"/> con los datos de los insumos.</returns>
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

        /// <summary>
        /// Inserta un nuevo insumo en la base de datos.
        /// Utiliza el procedimiento almacenado <c>sp_InsertarInsumo</c>.
        /// </summary>
        /// <param name="objInsumo">Objeto <see cref="BE.Insumo"/> con los datos del insumo a insertar.</param>
        /// <returns>El identificador (Id) del insumo insertado.</returns>
        /// <exception cref="Exception">Se lanza si no se puede obtener el Id del insumo insertado.</exception>
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



        /// <summary>
        /// Edita los datos de un insumo existente en la base de datos.
        /// Utiliza el procedimiento almacenado <c>sp_EditarInsumo</c>.
        /// </summary>
        /// <param name="objInsumo">Objeto <see cref="BE.Insumo"/> con los datos actualizados del insumo.</param>
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


        /// <summary>
        /// Elimina un insumo de la base de datos según su identificador.
        /// Utiliza el procedimiento almacenado <c>sp_Insumo_Eliminar</c>.
        /// </summary>
        /// <param name="id">Identificador del insumo a eliminar.</param>
        public void Eliminar(int id)
        {
            var parametros = new SqlParameter[]
            {
                conexion.crearParametro("@Id", id)
            };

            conexion.EscribirPorStoreProcedure("sp_Insumo_Eliminar", parametros);
        }

        /// <summary>
        /// Busca un insumo en la base de datos a partir de su nombre.
        /// Utiliza el procedimiento almacenado <c>sp_BuscarInsumoPorNombre</c>.
        /// Si hay múltiples coincidencias, se devuelve solo la primera.
        /// </summary>
        /// <param name="nombreInsumo">
        /// Nombre exacto del insumo a buscar.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="BE.Insumo"/> si se encuentra una coincidencia; de lo contrario, <c>null</c>.
        /// </returns>
        /// <exception cref="SqlException">
        /// Se produce si ocurre un error al acceder a la base de datos.
        /// </exception>
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
                StockMinimo = Convert.ToInt32(row["stock_minimo"])
            };
        }

        /// <summary>
        /// Descuenta del stock la cantidad indicada de un insumo, gestionando los lotes correspondientes.
        /// </summary>
        /// <param name="idInsumo">Identificador del insumo al que se le descontará stock.</param>
        /// <param name="cantidadAUsar">Cantidad de unidades a descontar del stock.</param>
        /// <exception cref="Exception">
        /// Se lanza si no hay stock suficiente o si ocurre un error al actualizar la base de datos.
        /// </exception>
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
