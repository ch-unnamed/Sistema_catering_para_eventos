using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Menu_Plato
    {
        public List<BE.Menu_Plato> ObtenerNombresPlatosPorMenu(BE.Menu_Plato menuId)
        {
            DAL.Menu_Plato menuPlatoDAL = new DAL.Menu_Plato();
            return menuPlatoDAL.ObtenerNombresPlatosPorMenu(menuId);
        }

        public List<BE.Menu_Plato> ObtenerPlatosDelMenu(BE.Menu_Plato menuId)
        {
            DAL.Menu_Plato menuPlatoDAL = new DAL.Menu_Plato();
            return menuPlatoDAL.ObtenerPlatosDelMenu(menuId);
        }
    }
}