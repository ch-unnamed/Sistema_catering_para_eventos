using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Lógica de negocio para la gestión de Tipos de Evento.
    /// </summary>
    public class Tipo_Evento
    {
        /// <summary>
        /// Obtiene una lista de todos los tipos de evento disponibles.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Tipo_Evento"/>.</returns>
        public List<BE.Tipo_Evento> ListarTipoEvento()
        {
            DAL.Tipo_Evento teDAL = new DAL.Tipo_Evento();
            return teDAL.ListarTipoEvento();
        }
    }
}
