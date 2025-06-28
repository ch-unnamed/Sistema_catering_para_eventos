using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PlatoInsumo
    {
        public List<BE.PlatoInsumo> ObtenerInsumosDelPlato(BE.PlatoInsumo platoId)
        {
            DAL.PlatoInsumo platoInsumoDAL = new DAL.PlatoInsumo();
            return platoInsumoDAL.ObtenerInsumosDelPlato(platoId);
        }
    }
}
