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

        public List<BE.Configuracion_Empresa> Listar()
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();

            return configuracionDAL.Listar();
        }

        public Dictionary<string, string> ValidarConfiguracion(BE.Configuracion_Empresa configuracion)
        {
            var errores = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(configuracion.Nombre) || (configuracion.Nombre is string))
                errores["nombre"] = "La Configuracion debe tener un Nombre valido";

            if (!decimal.TryParse(configuracion.Porcentaje.ToString(), out decimal porcentaje) || porcentaje <= 0)
                errores["porcentaje"] = "La Configuracion debe tener un porcentaje valido";

            return errores;
        }

        public int CrearConfiguracion(BE.Configuracion_Empresa configuracion, out Dictionary<string, string> errores)
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();
            errores = ValidarConfiguracion(configuracion);

            if (errores.Count == 0)
                return configuracionDAL.CrearConfiguracion(configuracion, out _);
            else
                return 0;
        }

        public bool EditarConfiguracion(BE.Configuracion_Empresa configuracion, out Dictionary<string, string> errores)
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();
            errores = ValidarConfiguracion(configuracion);

            if (errores.Count == 0)
                return configuracionDAL.EditarConfiguracion(configuracion, out _);
            else
                return false;
        }

        public bool EliminarConfiguracion(int idConfiguracion, out string mensaje)
        {
            DAL.Configuracion_Empresa configuracionDAL = new DAL.Configuracion_Empresa();
            return configuracionDAL.EliminarConfiguracion(idConfiguracion, out mensaje);
        }
    }
}