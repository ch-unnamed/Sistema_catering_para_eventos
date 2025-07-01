using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Rol
    {
       public List<BE.Rol> ListarRol()
        {
            DAL.Rol rolDAL = new DAL.Rol();
            return rolDAL.ListarRol();
        }
    }
}
