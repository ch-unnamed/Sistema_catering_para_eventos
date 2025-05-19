using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security; // Para autenticación basada en Forms Authentication
using IUVendedor.Permisos;

using BE;
using BLL;

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

        [HttpGet]
        public JsonResult ListarClientes()
        {
            List<BE.Cliente> oLista = new List<BE.Cliente>();

            oLista = new BLL.Cliente().Listar(); // Almacena los clientes de negocio

            return Json(new {data = oLista}, JsonRequestBehavior.AllowGet); // SE PUEDEN CAMBIAR LOS VALORES DEL JSON
        }

        [HttpPost]
        public JsonResult GuardarCliente(BE.Cliente oCliente)
        {
            object resultado;
            string mensaje = string.Empty;

            if(oCliente.IdCliente == 0)
            {
                resultado = new BLL.Cliente().CrearCliente(oCliente, out mensaje);
            }
            else
            {
                resultado = new BLL.Cliente().EditarCliente(oCliente, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Insumos()
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