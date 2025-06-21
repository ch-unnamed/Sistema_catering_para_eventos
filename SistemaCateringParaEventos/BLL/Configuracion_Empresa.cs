using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Configuracion_Empresa
    {
        public BE.Configuracion_Empresa ObtenerPorcentajeGanancia(string nombre)
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();
            
            return configuracionDAL.ObtenerPorcentajeGanancia(nombre);
        }

        public int consultarCliente(int cliente_id)
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();

            return configuracionDAL.consultarCliente(cliente_id);
        }
    }
}