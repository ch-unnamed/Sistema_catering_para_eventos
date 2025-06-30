using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Cotizacion_Menu_Plato
    {
        public List<BE.Cotizacion_Menu_Plato> Listar(int cotizacionMenuPlatoId)
        {
            DAL.Cotizacion_Menu_Plato cotizacionMenuPlatoDAL = new DAL.Cotizacion_Menu_Plato();

            return cotizacionMenuPlatoDAL.Listar(cotizacionMenuPlatoId);
        }
    }
}