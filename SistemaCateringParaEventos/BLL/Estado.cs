using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Estado
    {
        public List<BE.Estado> Listar()
        {
            DAL.Estado estadoDAL = new DAL.Estado();
            return estadoDAL.Listar();
        }
    }
}