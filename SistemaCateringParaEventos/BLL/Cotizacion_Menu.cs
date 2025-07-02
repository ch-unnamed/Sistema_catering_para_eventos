using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Lógica de negocio para la gestión de la relación Cotización-Menú.
    /// </summary>
    public class Cotizacion_Menu
    {
        /// <summary>
        /// Lista todas las relaciones Cotización-Menú.
        /// </summary>
        /// <returns>Lista de objetos <see cref="BE.Cotizacion_Menu"/>.</returns>
        public List<BE.Cotizacion_Menu> Listar()
        {
            DAL.Cotizacion_Menu cotizacionMenuDAL = new DAL.Cotizacion_Menu();
            return cotizacionMenuDAL.Listar();
        }

        /// <summary>
        /// Valida los datos de una relación Cotización-Menú.
        /// </summary>
        /// <param name="cotizacionMenu">La relación Cotización-Menú a validar.</param>
        /// <returns>Diccionario con los errores encontrados, donde la clave es el campo y el valor es el mensaje de error.</returns>
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

        /// <summary>
        /// Edita una relación Cotización-Menú después de validar los datos.
        /// </summary>
        /// <param name="cotizacionMenu">La relación Cotización-Menú a editar.</param>
        /// <param name="errores">Diccionario de errores de validación, si los hay.</param>
        /// <returns>True si la edición fue exitosa, false en caso contrario.</returns>
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