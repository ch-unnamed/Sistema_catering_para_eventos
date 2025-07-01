using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Ubicacion
    {
        public BE.Ubicacion ObtenerUbicacionInicial()
        {
            DAL.Ubicacion dalUbicacion = new DAL.Ubicacion();
            return dalUbicacion.ObtenerUbicacionInicial();


    }
    }

}
