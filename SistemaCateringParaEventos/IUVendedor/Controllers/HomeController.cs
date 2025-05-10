using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security; // Para autenticación basada en Forms Authentication
using IUVendedor.Permisos;

namespace IUVendedor.Controllers
{
    [Authorize] // Solo usuarios autenticados pueden acceder a este controlador
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        [PermisosRol(Models.Rol.Gerente, Models.Rol.Vendedor)]
        public ActionResult Eventos()
        {
            return View();
        }

        [PermisosRol(Models.Rol.Vendedor)]
        public ActionResult Clientes()
        {
            return View();
        }

        public ActionResult SinPermiso()
        {
            ViewBag.Message = "No tenes permisos.";

            return View();
        }
    }
}