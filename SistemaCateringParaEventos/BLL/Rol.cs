using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Proporciona métodos para la gestión de roles en la capa de negocio.
    /// </summary>
    public class Rol
    {
        /// <summary>
        /// Obtiene una lista de todos los roles disponibles.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Rol"/> que representan los roles.</returns>
        public List<BE.Rol> ListarRol()
        {
            DAL.Rol rolDAL = new DAL.Rol();
            return rolDAL.ListarRol();
        }
    }
}
