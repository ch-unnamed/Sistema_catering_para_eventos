using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PlatoInsumo
    {
        /// <summary>
        /// Obtiene la lista de insumos asociados a un plato específico.
        /// </summary>
        /// <param name="platoId">Objeto <see cref="BE.PlatoInsumo"/> que contiene la información del plato.</param>
        /// <returns>Lista de objetos <see cref="BE.PlatoInsumo"/> que representan los insumos del plato.</returns>
        public List<BE.PlatoInsumo> ObtenerInsumosDelPlato(BE.PlatoInsumo platoId)
        {
            DAL.PlatoInsumo platoInsumoDAL = new DAL.PlatoInsumo();
            return platoInsumoDAL.ObtenerInsumosDelPlato(platoId);
        }
    }
}
