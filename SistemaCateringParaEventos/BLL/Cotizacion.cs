using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Cotizacion
    {
        public List<BE.Cotizacion> Listar()
        {
            DAL.Cotizacion cotizacionDAL = new DAL.Cotizacion();
            return cotizacionDAL.Listar();
        }
    }
}
