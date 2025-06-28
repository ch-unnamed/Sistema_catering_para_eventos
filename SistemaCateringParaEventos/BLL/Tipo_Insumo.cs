using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Clase de lógica de negocio para operaciones relacionadas con Tipo_Insumo.
    /// </summary>
    public class Tipo_Insumo
    {
        /// <summary>
        /// Obtiene una lista de todos los tipos de insumo.
        /// </summary>
        /// <returns>Lista de objetos Tipo_Insumo.</returns>
        public List<BE.Tipo_Insumo> Listar()
        {
            return DAL.Tipo_Insumo.ListarTipos();
        }
    }
}
