using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Menu
    {
        /// <summary>
        /// Lista los menús disponibles para el vendedor.
        /// </summary>
        /// <returns>Lista de menús para el vendedor.</returns>
        public List<BE.Menu> ListarMenusVendedor()
        {
            return new DAL.Menu().ListarMenusVendedor();
        }
        /// <summary>
        /// Lista todos los menús disponibles.
        /// </summary>
        /// <returns>Lista de todos los menús.</returns>
        public List<BE.Menu> ListarMenus()
        {
            return new DAL.Menu().ListarMenus();
        }

        /// <summary>
        /// Crea un nuevo menú después de validar los datos proporcionados.
        /// </summary>
        /// <param name="menu">El menú a crear.</param>
        /// <param name="mensaje">Mensaje de validación o resultado.</param>
        /// <returns>El identificador del menú creado, o 0 si hay error de validación.</returns>
        public int CrearMenu(BE.Menu menu, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(menu.Nombre))
                mensaje = "El menú debe tener un nombre.";

            if (string.IsNullOrWhiteSpace(menu.Descripcion))
                mensaje = "El menú debe tener una descripción.";

            if (menu.Platos == null || menu.Platos.Count < 3)
                mensaje = "Debe seleccionar al menos 3 platos.";

            if (menu.Platos.Count > 15)
                mensaje = "No puede seleccionar más de 15 platos.";

            if (!string.IsNullOrEmpty(mensaje))
                return 0;

            return new DAL.Menu().CrearMenu(menu, out mensaje);
        }

        /// <summary>
        /// Edita un menú existente después de validar los datos proporcionados.
        /// </summary>
        /// <param name="menu">El menú a editar.</param>
        /// <param name="mensaje">Mensaje de validación o resultado.</param>
        /// <returns>El identificador del menú editado, o 0 si hay error de validación.</returns>
        public int EditarMenu(BE.Menu menu, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(menu.Nombre))
                mensaje = "El menú debe tener un nombre.";

            if (string.IsNullOrWhiteSpace(menu.Descripcion))
                mensaje = "El menú debe tener una descripción.";

            if (menu.Platos == null || menu.Platos.Count < 3)
                mensaje = "Se deben seleccionar al menos 3 platos.";

            if (!string.IsNullOrEmpty(mensaje))
                return 0;

            return new DAL.Menu().EditarMenu(menu, out mensaje);
        }
 
      /// <summary>
        /// Elimina un menú por su identificador.
        /// </summary>
        /// <param name="idMenu">Identificador del menú a eliminar.</param>
        /// <param name="mensaje">Mensaje de resultado.</param>
        /// <returns>True si se eliminó correctamente, false en caso contrario.</returns>
        public bool EliminarMenu(int idMenu, out string mensaje)
        {
            DAL.Menu menuDAL = new DAL.Menu();
            return menuDAL.EliminarMenu(idMenu, out mensaje);
        }
    }

}