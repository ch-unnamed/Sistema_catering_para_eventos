using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Lógica de negocio para la gestión de la relación Cotización-Menú-Plato.
    /// </summary>
    public class Cotizacion_Menu_Plato
    {
        /// <summary>
        /// Lista las relaciones Cotización-Menú-Plato según el identificador proporcionado.
        /// </summary>
        /// <param name="cotizacionMenuPlatoId">Identificador de la relación Cotización-Menú-Plato.</param>
        /// <returns>Lista de objetos <see cref="BE.Cotizacion_Menu_Plato"/> asociados al identificador.</returns>
        public List<BE.Cotizacion_Menu_Plato> Listar(int cotizacionMenuPlatoId)
        {
            DAL.Cotizacion_Menu_Plato cotizacionMenuPlatoDAL = new DAL.Cotizacion_Menu_Plato();

            return cotizacionMenuPlatoDAL.Listar(cotizacionMenuPlatoId);
        }
    }
}