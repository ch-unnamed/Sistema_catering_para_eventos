using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Cotizacion_Menu
    {
        public List<BE.Cotizacion_Menu> Listar()
        {
            DAL.Cotizacion_Menu cotizacionMenuDAL = new DAL.Cotizacion_Menu();

            return cotizacionMenuDAL.Listar();
        }

        public Dictionary<string, string> ValidarCotizacionMenu(BE.Cotizacion_Menu cotizacionMenu)
        {
            var errores = new Dictionary<string, string>();

            if (!int.TryParse(cotizacionMenu.Cotizacion.IdCotizacion.ToString(), out int cotizacionId) || cotizacionId <= 0)
                errores["cotizacionId"] = "No debe modificar el ID";

            if (!int.TryParse(cotizacionMenu.Menu.Id.ToString(), out int menuId) || menuId <= 0)
                errores["menuId"] = "No debe modificar el ID";

            if (string.IsNullOrWhiteSpace(cotizacionMenu.Estado.ToString()))
                errores["estado"] = "Debe ingresar un Estado";

            return errores;
        }

        public bool EditarCotizacionMenu(BE.Cotizacion_Menu cotizacionMenu, out Dictionary<string, string> errores)
        {
            DAL.Cotizacion_Menu cotizacionMenuDAL = new DAL.Cotizacion_Menu();
            errores = ValidarCotizacionMenu(cotizacionMenu);

            if (errores.Count == 0)
                return cotizacionMenuDAL.EditarCotizacionMenu(cotizacionMenu, out _);
            else
                return false;
        }
    }
}