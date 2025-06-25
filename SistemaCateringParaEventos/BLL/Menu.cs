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
            return new DAL.Menu().Listar();
        }
        public List<BE.Menu> ListarMenus()
        {
            return new DAL.Menu().ListarMenus();
        }

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

        public int EditarMenu(BE.Menu menu, out string mensaje)
        {
            mensaje = string.Empty;

            if (menu.Id <= 0)
            {
                mensaje = "ID de menú inválido.";
                return 0;
            }

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
    }

}
