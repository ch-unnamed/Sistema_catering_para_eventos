using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Tipo_Evento
    {
        public List<BE.Tipo_Evento> ListarTipoEvento()
        {
            DAL.Tipo_Evento teDAL = new DAL.Tipo_Evento();
            return teDAL.ListarTipoEvento();
        }
    }
}
