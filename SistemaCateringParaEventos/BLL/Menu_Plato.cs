using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Menu_Plato
    {
        /// <summary>
        /// Obtiene los nombres de los platos asociados a un menú específico.
        /// </summary>
        /// <param name="menuId">El objeto Menu_Plato que representa el menú del cual se desean obtener los nombres de los platos.</param>
        /// <returns>Lista de objetos Menu_Plato con los nombres de los platos asociados al menú.</returns>
        public List<BE.Menu_Plato> ObtenerNombresPlatosPorMenu(BE.Menu_Plato menuId)
        {
            DAL.Menu_Plato menuPlatoDAL = new DAL.Menu_Plato();
            return menuPlatoDAL.ObtenerNombresPlatosPorMenu(menuId);
        }

        /// <summary>
        /// Obtiene la lista de platos asociados a un menú específico.
        /// </summary>
        /// <param name="menuId">El objeto Menu_Plato que representa el menú del cual se desean obtener los platos.</param>
        /// <returns>Lista de objetos Menu_Plato que representan los platos asociados al menú.</returns>
        public List<BE.Menu_Plato> ObtenerPlatosDelMenu(BE.Menu_Plato menuId)
        {
            DAL.Menu_Plato menuPlatoDAL = new DAL.Menu_Plato();
            return menuPlatoDAL.ObtenerPlatosDelMenu(menuId);
        }
    }
}