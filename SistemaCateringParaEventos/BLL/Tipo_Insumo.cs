using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Tipo_Insumo
    {
        public List<BE.Tipo_Insumo> Listar()
        {
            return DAL.Tipo_Insumo.ListarTipos();
        }
    }
}
