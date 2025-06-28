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
    public class PlatoInsumo
    {
        /// <summary>
        /// Obtiene la lista de insumos asociados a un plato específico.
        /// Utiliza el procedimiento almacenado <c>sp_ObtenerInsumosDelPlato</c>.
        /// </summary>
        /// <param name="plato_insumo">Objeto <see cref="BE.PlatoInsumo"/> que contiene el identificador del plato.</param>
        /// <returns>Lista de objetos <see cref="BE.PlatoInsumo"/> que representan los insumos asociados al plato.</returns>
        public List<BE.PlatoInsumo> ObtenerInsumosDelPlato(BE.PlatoInsumo plato_insumo)
        {
            Conexion conexion = new Conexion();

            List<BE.PlatoInsumo> insumosPorPlatoId = new List<BE.PlatoInsumo>();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@plato_id", plato_insumo.Plato.Id)
            };

            DataTable dt = conexion.LeerPorStoreProcedure("sp_ObtenerInsumosDelPlato", parametros);

            foreach (DataRow fila in dt.Rows)
            {
                BE.PlatoInsumo unPlatoInsumo = new BE.PlatoInsumo();
                unPlatoInsumo.Plato = new BE.Plato();
                unPlatoInsumo.Insumo = new BE.Insumo();

                unPlatoInsumo.Plato.Id = Convert.ToInt32(fila["plato_id"]);
                unPlatoInsumo.Insumo.Id = Convert.ToInt32(fila["id"]);
                unPlatoInsumo.Insumo.Nombre = fila["Nombre"].ToString();
                unPlatoInsumo.Insumo.TipoNombre = fila["tipoNombre"].ToString();

                insumosPorPlatoId.Add(unPlatoInsumo);
            }

            return insumosPorPlatoId;
        }
    }
}
