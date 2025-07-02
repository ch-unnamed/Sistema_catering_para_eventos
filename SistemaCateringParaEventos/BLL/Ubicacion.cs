using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Proporciona métodos de negocio relacionados con la gestión de ubicaciones.
    /// </summary>
    public class Ubicacion
    {
        /// <summary>
        /// Obtiene la ubicación inicial desde la capa de acceso a datos.
        /// </summary>
        /// <returns>
        /// Un objeto <see cref="BE.Ubicacion"/> que representa la ubicación inicial.
        /// </returns>
        public BE.Ubicacion ObtenerUbicacionInicial()
        {
            DAL.Ubicacion dalUbicacion = new DAL.Ubicacion();
            return dalUbicacion.ObtenerUbicacionInicial();
        }
    }

}
