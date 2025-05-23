using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security; // Para autenticación basada en Forms Authentication
using IUVendedor.Permisos;

using BE;
using BLL;
using System.Web.Helpers;

namespace IUVendedor.Controllers
{
    [Authorize] // Solo usuarios autenticados pueden acceder a este controlador
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //VENDEDOR (mati)
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

        [HttpPost]
        public JsonResult EliminarCliente(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new BLL.Cliente().EliminarCliente(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarEventos()
        {
            List<BE.Evento> oLista = new List<BE.Evento>();

            oLista = new BLL.Evento().Listar();

            var eventosFormateados = oLista.Select(e => new
            {
                e.IdEvento,
                e.Nombre,
                e.Capacidad,
                e.Tipo,
                Ubicacion = new
                {
                    e.Ubicacion.Direccion,
                    e.Ubicacion.Ciudad,
                    e.Ubicacion.Pais
                }
            });

            return Json(new { data = eventosFormateados }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarEvento(BE.Evento oEvento)
        {
            object resultado;
            string mensaje = string.Empty;

            if(oEvento.IdEvento == 0)
            {
                resultado = new BLL.Evento().CrearEvento(oEvento, out mensaje);
            } else
            {
                resultado = new BLL.Evento().EditarEvento(oEvento, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarEvento(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new BLL.Evento().EliminarEvento(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // me va a servir para la cotizacion
        //Fecha = e.Fecha.ToString("dd/MM/yyyy"), // Formato personalizado

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult Insumos()
        {
            return View();
        }

        public ActionResult SinPermiso()
        {
            ViewBag.Message = "No tenes permisos.";

            return View();
        }

        [PermisosRol(Models.Rol.Gerente)]
        public ActionResult Temporadas()
        {
            return View();
        }
    }
}