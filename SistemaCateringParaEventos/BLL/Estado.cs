using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Clase de lógica de negocio para la entidad Estado.
    /// </summary>
    public class Estado
    {
        /// <summary>
        /// Obtiene la lista de estados desde la capa de acceso a datos.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Estado"/>.</returns>
        public List<BE.Estado> Listar()
        {
            DAL.Estado estadoDAL = new DAL.Estado();
            return estadoDAL.Listar();
        }
    }
}