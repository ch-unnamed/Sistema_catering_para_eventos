using BE;
using BLL;
using DAL;
using IUVendedor.Permisos;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security; // Para autenticación basada en Forms Authentication

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

        [PermisosRol(Models.Rol.Vendedor)]
        public ActionResult Cotizaciones()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarClientes()
        {
            List<BE.Cliente> oLista = new List<BE.Cliente>();

            oLista = new BLL.Cliente().Listar(); // Almacena los clientes de negocio

            var clientesFormateados = oLista.Select(e => new
            {
                e.IdCliente,
                e.Dni,
                e.Email,
                e.Region,
                e.Telefono,
                e.Nombre,
                e.Apellido,
                Tipo_Cliente = new
                {
                    e.Tipo_Cliente.Nombre
                }
            });

            return Json(new { data = clientesFormateados }, JsonRequestBehavior.AllowGet); // SE PUEDEN CAMBIAR LOS VALORES DEL JSON
        }

        [HttpPost]
        public JsonResult GuardarCliente(BE.Cliente oCliente)
        {
            object resultado;
            string mensaje = string.Empty;

            if (oCliente.IdCliente == 0)
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
                Fecha = e.Fecha.ToString("yyyy-MM-dd"),
                Tipo_Evento = new
                {
                    e.Tipo_Evento.Nombre
                },
                Ubicacion = new
                {
                    e.Ubicacion.IdUbicacion,
                    e.Ubicacion.Calle,
                    e.Ubicacion.Altura,
                    e.Ubicacion.Ciudad,
                    e.Ubicacion.Provincia
                }
            });

            return Json(new { data = eventosFormateados }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarEvento(BE.Evento oEvento)
        {
            object resultado;
            string mensaje = string.Empty;

            if (oEvento.IdEvento == 0)
            {
                resultado = new BLL.Evento().CrearEvento(oEvento, out mensaje);
            }
            else
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

        //////////////////////////////////////////////////////////////////
        public ActionResult Insumos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarInsumos()
        {
            List<BE.Insumo> oLista = new List<BE.Insumo>();

            oLista = new BLL.Insumo().Listar();

            var InsumosFormateados = oLista.Select(e => new
            {
                e.Id,
                e.Nombre,
                e.Unidad,
                e.TipoId,
                TipoNombre = e.TipoNombre,
                e.Costo,
                e.StockMinimo,
            });

            return Json(new { data = InsumosFormateados }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarInsumo(int Id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            try
            {
                respuesta = new BLL.Insumo().EliminarInsumo(Id);
                mensaje = respuesta ? "Insumo eliminado correctamente." : "No se pudo eliminar el insumo.";
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar el insumo: " + ex.Message;
            }

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GuardarInsumo(BE.Insumo oInsumo)
        {
            string mensaje = string.Empty;
            bool exito = true;
            int nuevoId = 0;

            try
            {
                if (oInsumo.Id == 0)
                {
                    nuevoId = new BLL.Insumo().CrearInsumo(oInsumo);
                    mensaje = "Insumo creado correctamente.";
                }
                else
                {
                    exito = new BLL.Insumo().EditarInsumo(oInsumo);
                    mensaje = "Insumo editado correctamente.";
                }
            }
            catch (Exception ex)
            {
                exito = false;
                mensaje = ex.Message;
            }

            return Json(new { nuevoId = nuevoId, exito = exito, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }




        public ActionResult Platos()
        {
            return View();
        }
        ///LOTES

        [HttpPost]
        public JsonResult GuardarLote(string Cantidad, string FechaDeVencimiento, string InsumoId, int Id = 0)
        {
            string mensaje = string.Empty;
            bool exito = true;
            if (string.IsNullOrWhiteSpace(Cantidad) ||
                string.IsNullOrWhiteSpace(FechaDeVencimiento) ||
                string.IsNullOrWhiteSpace(InsumoId))
            {
                return Json(new { resultado = false, mensaje = "Todos los campos son obligatorios." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                int cantidad = int.Parse(Cantidad);
                DateTime fechaDeVencimiento = DateTime.Parse(FechaDeVencimiento);
                int insumoId = int.Parse(InsumoId);

                if (cantidad <= 0)
                    return Json(new { resultado = false, mensaje = "La cantidad debe ser mayor a cero." }, JsonRequestBehavior.AllowGet);

                if (fechaDeVencimiento <= DateTime.Today)
                    return Json(new { resultado = false, mensaje = "La fecha de vencimiento debe ser posterior a hoy." }, JsonRequestBehavior.AllowGet);

                // Crear objeto Lote
                BE.Lote oLote = new BE.Lote
                {
                    Id = Id,
                    Cantidad = cantidad,
                    FechaDeVencimiento = fechaDeVencimiento,
                    InsumoId = insumoId
                };

                if (oLote.Id == 0)
                {
                    new BLL.Lote().CrearLote(oLote, out mensaje);
                }
                else
                {
                    new BLL.Lote().EditarLote(oLote, out mensaje);
                }
            }
            catch (Exception ex)
            {
                exito = false;
                mensaje = "Error inesperado al guardar lote: " + ex.Message;
            }

            return Json(new { resultado = exito, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarLoteInsumo(int idInsumo)
        {
            try
            {
                BLL.Lote gestorLote = new BLL.Lote();
                var lotes = gestorLote.ListarLotes()
                    .Where(l => l.InsumoId == idInsumo)
                    .Select(l => new {
                        Id = l.Id,
                        insumo_id = l.InsumoId,  // acá usá el nombre exacto
                        Cantidad = l.Cantidad,
                        fecha_vencimiento = l.FechaDeVencimiento.ToString("yyyy-MM-dd") // formato fecha
                    })
                    .ToList();

                return Json(new { data = lotes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




        //Tipo_Insumo
        [HttpGet]
        public JsonResult ObtenerTiposInsumo()
        {
            try
            {
                var objListInsumo = new BLL.Tipo_Insumo().Listar();

                var tiposInsumosFormateados = objListInsumo.Select(e => new
                {
                    e.Id,
                    e.Nombre
                });

                return Json(new { data = tiposInsumosFormateados }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //Control de error
                return Json(new { success = false, message = "Error al obtener los tipos de insumo." }, JsonRequestBehavior.AllowGet);
            }
        }

        //////////////////////////////////////////////////////////////////
        [PermisosRol(Models.Rol.Gerente)]
        public ActionResult Temporadas()
        {
            return View();
        }

        [PermisosRol(Models.Rol.Administrador)]
        public ActionResult Usuarios()
        {
            return View();
        }

        [PermisosRol(Models.Rol.Administrador)]
        public ActionResult ConfigurarSistema()
        {
            return View();
        }

        [PermisosRol(Models.Rol.Gerente, Models.Rol.Chef)]
        public ActionResult Menus()
        {
            return View();
        }

        //////////////////////////////////////////////////////////////////
        public ActionResult SinPermiso()
        {
            ViewBag.Message = "No tenes permisos.";

            return View();
        }
    }
}