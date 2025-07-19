using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Provincia
    {
        public BE.Provincia obtenerProvincia(string nombreLocalidad)
        {
            DAL.Provincia provinciaDAL = new DAL.Provincia();
            return provinciaDAL.obtenerProvincia(nombreLocalidad);
        }

    }
}