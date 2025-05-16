using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Cliente
    {
        public List<BE.Cliente> Listar()
        {
            DAL.Cliente clienteDAL = new DAL.Cliente();
            return clienteDAL.Listar();
        }
    }
}
