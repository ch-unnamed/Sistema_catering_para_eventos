using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Menu
    {
        public List<BE.Menu> Listar()
        {
            DAL.Menu menuDAL = new DAL.Menu();
            return menuDAL.Listar();
        }

    }
}
