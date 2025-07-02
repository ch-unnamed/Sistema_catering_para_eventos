using BE;
using BLL;
using DAL;
using IUVendedor.Permisos;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security; 
using System.Web.Services.Description;

namespace IUVendedor.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Muestra la vista principal del sistema.
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Muestra la vista de eventos para gerente o vendedor.
        /// </summary>
        [PermisosRol(Models.Rol.Gerente, Models.Rol.Vendedor)]
        public ActionResult Eventos()
        {
            return View();
        }

        /// <summary>
        /// Muestra la vista de clientes para el vendedor.
        /// </summary>
        [PermisosRol(Models.Rol.Vendedor)]
        public ActionResult Clientes()
        {
            return View();
        }

        /// <summary>
        /// Muestra la vista de cotizaciones para el vendedor.
        /// </summary>
        [PermisosRol(Models.Rol.Vendedor)]
        public ActionResult Cotizaciones()
        {
            return View();
        }

        /// <summary>
        /// Obtiene la lista de clientes y la retorna en formato JSON.
        /// </summary>
        [HttpGet]
        public JsonResult ListarClientes()
        {
            List<BE.Cliente> oLista = new List<BE.Cliente>();
            oLista = new BLL.Cliente().Listar();
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
            return Json(new { data = clientesFormateados }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guarda un cliente nuevo o edita uno existente.
        /// </summary>
        /// <param name="oCliente">Cliente a guardar o editar.</param>
        [HttpPost]
        public JsonResult GuardarCliente(BE.Cliente oCliente)
        {
            Dictionary<string, string> errores;
            object resultado;
            if (oCliente.IdCliente == 0)
            {
                resultado = new BLL.Cliente().CrearCliente(oCliente, out errores);
            }
            else
            {
                resultado = new BLL.Cliente().EditarCliente(oCliente, out errores);
            }
            return Json(new { resultado = resultado, errores = errores }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Elimina un cliente por su identificador.
        /// </summary>
        /// <param name="id">Identificador del cliente.</param>
        [HttpPost]
        public JsonResult EliminarCliente(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new BLL.Cliente().EliminarCliente(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene la lista de eventos y la retorna en formato JSON.
        /// </summary>
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

        /// <summary>
        /// Guarda un evento nuevo o edita uno existente.
        /// </summary>
        /// <param name="oEvento">Evento a guardar o editar.</param>
        [HttpPost]
        public JsonResult GuardarEvento(BE.Evento oEvento)
        {
            Dictionary<string, string> errores;
            object resultado;
            if (oEvento.IdEvento == 0)
            {
                resultado = new BLL.Evento().CrearEvento(oEvento, out errores);
            }
            else
            {
                resultado = new BLL.Evento().EditarEvento(oEvento, out errores);
            }
            return Json(new { resultado = resultado, errores = errores }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Elimina un evento por su identificador.
        /// </summary>
        /// <param name="id">Identificador del evento.</param>
        [HttpPost]
        public JsonResult EliminarEvento(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new BLL.Evento().EliminarEvento(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene la lista de cotizaciones y la retorna en formato JSON.
        /// </summary>
        [HttpGet]
        public JsonResult ListarCotizaciones()
        {
            List<BE.Cotizacion> oLista = new List<BE.Cotizacion>();
            oLista = new BLL.Cotizacion().Listar();
            var eventosFormateados = oLista.Select(e => new
            {
                e.IdCotizacion,
                Evento = new
                {
                    e.Evento.Nombre
                },
                Cliente = new
                {
                    e.Cliente.Dni
                },
                Vendedor = new
                {
                    e.Vendedor.IdUsuario
                },
                Estado = new
                {
                    e.Estado.Nombre
                },
                FechaPedido = e.FechaPedido.ToString("yyyy-MM-dd"),
                e.Total,
                FechaRealizacion = e.FechaRealizacion.ToString("yyyy-MM-dd")
            });
            return Json(new { data = eventosFormateados }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista los menús disponibles para el vendedor.
        /// </summary>
        [HttpGet]
        public JsonResult ListarMenusVendedor()
        {
            List<BE.Menu> oLista = new BLL.Menu().ListarMenusVendedor();
            var menusFiltrados = oLista.Select(
                menu => new
                {
                    menu.Id,
                    menu.Nombre
                });
            return Json(new { data = menusFiltrados }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista los estados disponibles.
        /// </summary>
        [HttpGet]
        public JsonResult ListarEstados()
        {
            List<BE.Estado> oLista = new BLL.Estado().Listar();
            var estadosFiltrados = oLista.Select(
                estado => new
                {
                    estado.IdEstado,
                    estado.Nombre
                });
            return Json(new { data = estadosFiltrados }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene los nombres de los platos asociados a un menú.
        /// </summary>
        /// <param name="menu_id">Identificador del menú.</param>
        [HttpGet]
        public JsonResult ObtenerNombresPlatosPorMenu(int menu_id)
        {
            BE.Menu_Plato menuPlato = new BE.Menu_Plato();
            menuPlato.Menu = new BE.Menu();
            menuPlato.Menu.Id = menu_id;
            List<BE.Menu_Plato> listaPlatos = new BLL.Menu_Plato().ObtenerNombresPlatosPorMenu(menuPlato);
            var platos = listaPlatos.Select(mp => new
            {
                IdPlato = mp.Plato.Id,
                Nombre = mp.Plato.Nombre,
                Descripcion = mp.Plato.Descripcion,
                Precio = mp.Plato.Precio
            }).ToList();
            var resultado = new
            {
                menu_id = menu_id,
                platos = platos
            };
            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene la capacidad de un evento por su identificador.
        /// </summary>
        /// <param name="evento_id">Identificador del evento.</param>
        [HttpGet]
        public JsonResult ObtenerCapacidadPorIdEvento(int evento_id)
        {
            BE.Evento evento = new BLL.Evento().ObtenerCapacidadPorIdEvento(evento_id);
            var resultado = new
            {
                IdEvento = evento.IdEvento,
                Capacidad = evento.Capacidad,
                Fecha = evento.Fecha.ToString("yyyy-MM-dd")
            };
            if (evento != null)
            {
                return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = "Evento no encontrado" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Inserta platos personalizados en una cotización.
        /// </summary>
        [HttpPost]
        public JsonResult InsertarPlatosCotizacionPersonalizada(int eventoId, int clienteId, List<MenuConPlatosDTO> menus, DateTime fechaRealizacion, decimal total,
            int estado_id, int vendedor_id)
        {
            try
            {
                BE.Cotizacion cotizacion = new BE.Cotizacion
                {
                    FechaRealizacion = fechaRealizacion,
                    Total = total
                };
                BE.Evento evento = new BE.Evento { IdEvento = eventoId };
                BE.Cliente cliente = new BE.Cliente { IdCliente = clienteId };
                var menu_platos = new List<BE.Menu_Plato>();
                foreach (var menu in menus)
                {
                    foreach (var platoId in menu.Platos)
                    {
                        menu_platos.Add(new BE.Menu_Plato
                        {
                            Menu = new BE.Menu { Id = menu.MenuId },
                            Plato = new BE.Plato { Id = platoId }
                        });
                    }
                }
                BE.Estado estado = new BE.Estado { IdEstado = estado_id };
                BE.Usuario vendedor = new BE.Usuario { IdUsuario = vendedor_id };
                BLL.Plato gestor = new BLL.Plato();
                int idGenerado = gestor.InsertarPlatosCotizacion(cotizacion, evento, cliente, menu_platos, estado, vendedor);
                return Json(new
                {
                    success = true,
                    message = "Platos insertados correctamente.",
                    idCotizacion = idGenerado
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al insertar los platos: " + ex.Message });
            }
        }

        /// <summary>
        /// Obtiene el porcentaje de ganancia general de la empresa.
        /// </summary>
        [HttpGet]
        public JsonResult ObtenerPorcentajeGananciaGeneral(string nombre)
        {
            nombre = "Ganancia General";
            BE.Configuracion_Empresa configuracion = new BLL.Configuracion_Empresa().ObtenerPorcentajeGanancia(nombre);
            var resultado = new
            {
                Porcentaje = configuracion.Porcentaje
            };
            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene el porcentaje de descuento para clientes de primera vez.
        /// </summary>
        [HttpGet]
        public JsonResult ObtenerPorcentajeDescuentoPrimeraVez(string nombre)
        {
            nombre = "Primera vez ";
            BE.Configuracion_Empresa configuracion = new BLL.Configuracion_Empresa().ObtenerPorcentajeGanancia(nombre);
            var resultado = new
            {
                Porcentaje = configuracion.Porcentaje
            };
            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene el porcentaje de descuento para más de 3 menús.
        /// </summary>
        [HttpGet]
        public JsonResult ObtenerPorcentajeDescuentoMasDe3Menus(string nombre)
        {
            nombre = "Mas de 3 menus";
            BE.Configuracion_Empresa configuracion = new BLL.Configuracion_Empresa().ObtenerPorcentajeGanancia(nombre);
            var resultado = new
            {
                Porcentaje = configuracion.Porcentaje
            };
            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Consulta la cantidad asociada a un cliente.
        /// </summary>
        [HttpGet]
        public JsonResult consultarCliente(int cliente_id)
        {
            int configuracion = new BLL.Configuracion_Empresa().consultarCliente(cliente_id);
            return Json(new { data = configuracion }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene la cantidad de registros asociados a un cliente.
        /// </summary>
        [HttpGet]
        public JsonResult cantidadCliente(int cliente_id)
        {
            int cliente = new BLL.Cliente().cantidadCliente(cliente_id);
            return Json(new { data = cliente }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene la cantidad de registros asociados a un evento.
        /// </summary>
        [HttpGet]
        public JsonResult cantidadEvento(int evento_id)
        {
            int evento = new BLL.Evento().cantidadEvento(evento_id);
            return Json(new { data = evento }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene el nombre de un evento por su identificador.
        /// </summary>
        [HttpGet]
        public JsonResult nombreEvento(int evento_id)
        {
            string evento = new BLL.Evento().nombreEvento(evento_id);
            return Json(new { data = evento }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene el DNI de un cliente por su identificador.
        /// </summary>
        [HttpGet]
        public JsonResult dniCliente(int cliente_id)
        {
            BE.Cliente cliente = new BLL.Cliente().dniCliente(cliente_id);
            var resultado = new
            {
                dni = cliente.Dni
            };
            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene el identificador de un vendedor por el id de rol.
        /// </summary>
        [HttpGet]
        public JsonResult idVendedor(int rol_id)
        {
            BE.Usuario usuario = new BLL.Usuario().idVendedor(rol_id);
            var resultado = new
            {
                idUsuario = usuario.IdUsuario
            };
            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Elimina una cotización por su identificador.
        /// </summary>
        [HttpPost]
        public JsonResult EliminarCotizacion(int cotizacion_id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new BLL.Cotizacion().EliminarCotizacion(cotizacion_id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edita una cotización existente.
        /// </summary>
        [HttpPost]
        public JsonResult EditarCotizacion(BE.Cotizacion cotizacion)
        {
            object resultado;
            string mensaje = string.Empty;
            resultado = new BLL.Cotizacion().EditarCotizacion(cotizacion, out mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene el correo electrónico de un cliente por su identificador.
        /// </summary>
        [HttpGet]
        public JsonResult ObtenerMailCliente(int cliente_id)
        {
            string cotizacion = new BLL.Cotizacion().ObtenerMailCliente(cliente_id);
            var resultado = new
            {
                email = cotizacion
            };
            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }

        //////////////////////////////////////////////////////////////////

        /// <summary>
        /// Envía un correo electrónico con la cotización generada.
        /// </summary>
        [HttpPost]
        public JsonResult EnviarMailCotizacion(string email, string html)
        {
            bool enviado = new BLL.Cotizacion().EnviarMailCotizacion(email, html);
            return Json(new { success = enviado }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Muestra la vista de insumos.
        /// </summary>
        public ActionResult Insumos()
        {
            return View();
        }

        /// <summary>
        /// Lista todos los insumos y retorna en formato JSON.
        /// </summary>
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

        /// <summary>
        /// Elimina un insumo por su identificador.
        /// </summary>
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

        /// <summary>
        /// Guarda un insumo nuevo o edita uno existente.
        /// </summary>
        [HttpPost]
        public JsonResult GuardarInsumo(BE.Insumo oInsumo)
        {
            string mensaje = string.Empty;
            bool exito = true;
            int nuevoId = 0;
            try
            {
                var gestor = new BLL.Insumo();
                var duplicado = gestor.ObtenerPorNombre(oInsumo.Nombre);
                if (duplicado != null && duplicado.Id != oInsumo.Id)
                {
                    return Json(new
                    {
                        nuevoId = 0,
                        exito = false,
                        mensaje = "Ya existe un insumo con ese nombre."
                    }, JsonRequestBehavior.AllowGet);
                }
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

        /// <summary>
        /// Descuenta stock de un insumo en lotes.
        /// </summary>
        [HttpPost]
        public JsonResult DescontarStock(int idInsumo, int cantidad)
        {
            try
            {
                new BLL.Insumo().DescontarStockEnLotes(idInsumo, cantidad);
                return Json(new { success = true, message = "Stock descontado." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Muestra la vista de platos para el chef.
        /// </summary>
        [PermisosRol(Models.Rol.Chef)]
        public ActionResult Platos()
        {
            return View();
        }

        /// <summary>
        /// Lista todos los platos y retorna en formato JSON.
        /// </summary>
        [HttpGet]
        public JsonResult ListarPlatos()
        {
            List<BE.Plato> oLista = new BLL.Plato().Listar();
            var platosFiltrados = oLista.Select(
                plato => new
                {
                    plato.Id,
                    plato.Nombre,
                    plato.Precio,
                    plato.Descripcion,
                    FechaDeCreacion = plato.FechaDeCreacion.ToString("dd-MM-yyyy"),
                });
            return Json(new { data = platosFiltrados }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guarda un plato nuevo o edita uno existente.
        /// </summary>
        [HttpPost]
        public JsonResult GuardarPlato(BE.Plato plato)
        {
            object resultado;
            string mensaje = string.Empty;
            if (plato.Id == 0)
            {
                resultado = new BLL.Plato().CrearPlato(plato, out mensaje);
            }
            else
            {
                resultado = new BLL.Plato().EditarPlato(plato, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Elimina un plato por su identificador.
        /// </summary>
        [HttpPost]
        public JsonResult EliminarPlato(int idPlato)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new BLL.Plato().EliminarPlato(idPlato, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene los insumos asociados a un plato.
        /// </summary>
        [HttpGet]
        public JsonResult ObtenerInsumosDelPlato(int plato_id)
        {
            BE.PlatoInsumo platoInsumo = new BE.PlatoInsumo();
            platoInsumo.Plato = new BE.Plato();
            platoInsumo.Plato.Id = plato_id;
            List<BE.PlatoInsumo> listaInsumos = new BLL.PlatoInsumo().ObtenerInsumosDelPlato(platoInsumo);
            var insumos = listaInsumos.Select(p_i => new
            {
                Id = p_i.Insumo.Id,
                Nombre = p_i.Insumo.Nombre,
                TipoNombre = p_i.Insumo.TipoNombre
            }).ToList();
            return Json(new { data = insumos }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Guarda o edita un lote de insumo.
        /// </summary>
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

        /// <summary>
        /// Lista los lotes asociados a un insumo.
        /// </summary>
        [HttpGet]
        public JsonResult ListarLoteInsumo(int idInsumo)
        {
            try
            {
                BLL.Lote gestorLote = new BLL.Lote();
                var lotes = gestorLote.ListarLotes()
                    .Where(l => l.InsumoId == idInsumo)
                    .Select(l => new
                    {
                        Id = l.Id,
                        insumo_id = l.InsumoId,
                        Cantidad = l.Cantidad,
                        fecha_ingreso = l.FechaDeIngreso.ToString("yyyy-MM-dd"),
                        fecha_vencimiento = l.FechaDeVencimiento.ToString("yyyy-MM-dd")
                    })
                    .ToList();
                return Json(new { data = lotes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Muestra la vista de menús para el chef.
        /// </summary>
        [PermisosRol(Models.Rol.Chef)]
        public ActionResult Menus()
        {
            return View();
        }
        

        /// Muestra la vista de órdenes para el chef.
        /// </summary>
        [PermisosRol(Models.Rol.Chef)]
        public ActionResult Ordenes()
        {
            return View();
        }


        /// <summary>
        /// Lista todos los menús para el chef.
        /// </summary>
        [HttpGet]
        public JsonResult ListarMenusChef()
        {
            List<BE.Menu> oLista = new BLL.Menu().ListarMenus();
            var menusFiltrados = oLista.Select(
                menu => new
                {
                    menu.Id,
                    menu.Nombre,
                    menu.Descripcion,
                    FechaDeCreacion = menu.FechaDeCreacion.ToString("dd-MM-yyyy"),
                });
            return Json(new { data = menusFiltrados }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene los platos asociados a un menú.
        /// </summary>
        [HttpGet]
        public JsonResult ObtenerPlatosDelMenu(int menu_id)
        {
            BE.Menu_Plato menuPlato = new BE.Menu_Plato();
            menuPlato.Menu = new BE.Menu();
            menuPlato.Menu.Id = menu_id;
            List<BE.Menu_Plato> listaPlatos = new BLL.Menu_Plato().ObtenerPlatosDelMenu(menuPlato);
            var platos = listaPlatos.Select(mp => new
            {
                Id = mp.Plato.Id,
                Nombre = mp.Plato.Nombre,
                Precio = mp.Plato.Precio,
                Descripcion = mp.Plato.Descripcion
            }).ToList();
            return Json(new { data = platos }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ConsultarPlato(int idPlato)
        {
            BE.Plato plato = new BLL.Plato().ConsultarPlato(idPlato);

            var resultado = new
            {
                Nombre = plato.Nombre,
                Precio = plato.Precio
            };
            
            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }

        
        /// <summary>
        /// Guarda un menú nuevo o edita uno existente.
        /// </summary>
        [HttpPost]
        public JsonResult GuardarMenu(BE.Menu menu)
        {
            object resultado;
            string mensaje = string.Empty;
            if (menu.Id == 0)
            {
                resultado = new BLL.Menu().CrearMenu(menu, out mensaje);
            }
            else
            {
                resultado = new BLL.Menu().EditarMenu(menu, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Elimina un menú por su identificador.
        /// </summary>
        [HttpPost]
        public JsonResult EliminarMenu(int idMenu)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new BLL.Menu().EliminarMenu(idMenu, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarCotizacionMenu()
        {
            List<BE.Cotizacion_Menu> oLista = new List<BE.Cotizacion_Menu>();

            oLista = new BLL.Cotizacion_Menu().Listar();

            var resultado = oLista.Select(e => new
            {
                e.Id,
                Cotizacion = new
                {
                    e.Cotizacion.IdCotizacion
                },
                Menu = new
                {
                    e.Menu.Id
                },
                e.Estado
            });

            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet); // SE PUEDEN CAMBIAR LOS VALORES DEL JSON
        }


        /// <summary>
        /// Edita una relación Cotización-Menú después de validar los datos.
        /// </summary>
        /// <param name="oCotizacionMenu">La relación Cotización-Menú a editar.</param>
        /// <returns>Resultado de la edición y posibles errores de validación.</returns>
        [HttpPost]
        public JsonResult EditarCotizacionMenu(BE.Cotizacion_Menu oCotizacionMenu)
        {
            Dictionary<string, string> errores;
            object resultado;

            resultado = new BLL.Cotizacion_Menu().EditarCotizacionMenu(oCotizacionMenu, out errores);

            return Json(new { resultado = resultado, errores = errores }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista las relaciones Cotización-Menú-Plato según el identificador proporcionado.
        /// </summary>
        /// <param name="cotizacionMenuPlatoId">Identificador de la relación Cotización-Menú-Plato.</param>
        /// <returns>Lista de platos asociados a la cotización-menú.</returns>
        [HttpGet]
        public JsonResult ListarCotizacionMenuPlato(int cotizacionMenuPlatoId)
        {
            List<BE.Cotizacion_Menu_Plato> oLista = new List<BE.Cotizacion_Menu_Plato>();

            oLista = new BLL.Cotizacion_Menu_Plato().Listar(cotizacionMenuPlatoId);

            var resultado = oLista.Select(e => new
            {
                Plato = new
                {
                    e.Plato.Id,
                    e.Plato.Nombre
                }
            });

            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet); // SE PUEDEN CAMBIAR LOS VALORES DEL JSON
        }

        //Tipo_Insumo
        /// <summary>
        /// Obtiene los tipos de insumo disponibles.
        /// </summary>
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
            catch (Exception)
            {
                return Json(new { success = false, message = "Error al obtener los tipos de insumo." }, JsonRequestBehavior.AllowGet);
            }
        }



        /// <summary>
        /// Lista todas las configuraciones de empresa.
        /// </summary>
        /// <returns>Lista de configuraciones de empresa.</returns>
        [HttpGet]
        public JsonResult ListarConfiguraciones()
        {
            List<BE.Configuracion_Empresa> oLista = new List<BE.Configuracion_Empresa>();

            oLista = new BLL.Configuracion_Empresa().Listar(); // Almacena los clientes de negocio

            var configuraciones = oLista.Select(e => new
            {
                e.ID,
                e.Nombre,
                e.Porcentaje
            });

            return Json(new { data = configuraciones }, JsonRequestBehavior.AllowGet); // SE PUEDEN CAMBIAR LOS VALORES DEL JSON
        }

        /// <summary>
        /// Guarda una configuración de empresa nueva o edita una existente.
        /// </summary>
        /// <param name="oConfiguracion">Configuración de empresa a guardar o editar.</param>
        /// <returns>Resultado de la operación y posibles errores de validación.</returns>
        [HttpPost]
        public JsonResult GuardarConfiguracion(BE.Configuracion_Empresa oConfiguracion)
        {
            Dictionary<string, string> errores;
            object resultado;

            if (oConfiguracion.ID == 0)
            {
                resultado = new BLL.Configuracion_Empresa().CrearConfiguracion(oConfiguracion, out errores);
            }
            else
            {
                resultado = new BLL.Configuracion_Empresa().EditarConfiguracion(oConfiguracion, out errores);
            }

            return Json(new { resultado = resultado, errores = errores }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Elimina una configuración de empresa por su identificador.
        /// </summary>
        /// <param name="idConfiguracion">Identificador de la configuración a eliminar.</param>
        /// <returns>Resultado de la operación y mensaje asociado.</returns>
        [HttpPost]
        public JsonResult EliminarConfiguracion(int idConfiguracion)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new BLL.Configuracion_Empresa().EliminarConfiguracion(idConfiguracion, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //TEMPORADAS

        [HttpGet]
        public JsonResult ListarTemporadas()
        {
            List<BE.Temporada> oLista = new List<BE.Temporada>();

            oLista = new BLL.Temporada().ListarTemporada();

            var TemporadasFormateadas = oLista.Select(t => new
            {
                t.IdTemporada,
                t.CantidadEvento,
                FechaComienzo = t.FechaComienzoTemp.ToString("yyyy-MM-dd"),
                FechaFin = t.FechaFin.ToString("yyyy-MM-dd"),
                Categoria_Temporada = new
                {
                    t.Id_CategoriaTemporada.IdCategoriaTemporada,
                    t.Id_CategoriaTemporada.Nombre
                },
            });


            return Json(new { data = TemporadasFormateadas }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerTemporada(int id)
        {
            var temporada = new BLL.Temporada().ObtenerTemporada(id);

            if (temporada == null)
                return Json(null, JsonRequestBehavior.AllowGet);

            var temporadaJson = new
            {
                temporada.IdTemporada,
                temporada.CantidadEvento,
                FechaComienzo = temporada.FechaComienzoTemp.ToString("yyyy-MM-dd"),
                FechaFin = temporada.FechaFin.ToString("yyyy-MM-dd"),
                Categoria_Temporada = new
                {
                    temporada.Id_CategoriaTemporada.IdCategoriaTemporada,
                    temporada.Id_CategoriaTemporada.Nombre
                }
            };

            return Json(temporadaJson, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CompararCantidadEventos(int id1, int id2)
        {
            string resultado = new BLL.Temporada().CompararCantidadEventos(id1, id2);
            return Json(new { mensaje = resultado }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CompararCantidadTipoEventos(int id1, int id2, int tipo)
        {
            string resultado = new BLL.Temporada().CompararTipoEvento(id1, id2, tipo);
            return Json(new { mensaje = resultado }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarTipoEvento()
        {
            List<BE.Tipo_Evento> oLista = new List<BE.Tipo_Evento>();

            oLista = new BLL.Tipo_Evento().ListarTipoEvento();

            var TipoEFormateados = oLista.Select(te => new
            {
                te.Id_TipoEvento,
                te.Nombre
            });

            return Json(new { data = TipoEFormateados }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarEventosPorUbicacion()
        {
            List<BE.Evento> oLista = new List<BE.Evento>();
            oLista = new BLL.Evento().ListarPorUbicacion();
            var eventosFormateados = oLista.Select(e => new
            {
                e.IdEvento,
                e.Nombre,
                Ubicacion = new
                {
                    e.Ubicacion.Calle,
                    e.Ubicacion.Altura,
                    e.Ubicacion.Ciudad,
                    e.Ubicacion.Provincia
                }
            });
            return Json(new { data = eventosFormateados }, JsonRequestBehavior.AllowGet);
        }



        // USUARIOS

        [HttpGet]
        public JsonResult ListarUsuario()
        {
            List<BE.Usuario> oLista = new List<BE.Usuario>();

            oLista = new BLL.Usuario().ListarUsuario();

            var UsuariosFormateados = oLista.Select(u => new
            {
                u.IdUsuario,
                u.Email,
                FechaCreacion = u.FechaCreacion.ToString("yyyy-MM-dd"),
                u.Nombre,
                u.Apellido,
                RolUsuario = new
                {
                    u.RolUsuario.IdRol,
                    u.RolUsuario.NombreRol
                },
            });

            return Json(new { data = UsuariosFormateados }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(BE.Usuario usuario)
        {
            Dictionary<string, string> errores = new Dictionary<string, string>();
            int nuevoId = 0;
            bool exito = true;
            string mensaje = string.Empty;

            try
            {
                if (usuario.IdUsuario == 0) // Crear
                {
                    nuevoId = new BLL.Usuario().CrearUsuario(usuario, out errores);
                    if (nuevoId == 0) exito = false;
                    else mensaje = "Usuario creado correctamente.";
                }
            }
            catch (Exception ex)
            {
                exito = false;
                mensaje = ex.Message;
                errores.Add("general", ex.Message);
            }

            return Json(new { resultado = nuevoId, exito = exito, mensaje = mensaje, errores = errores }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarUsuario(int Id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new BLL.Usuario().EliminarUsuario(Id);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        // ROLES

        [HttpGet]
        public JsonResult ListarRol()
        {
            List<BE.Rol> oLista = new List<BE.Rol>();

            oLista = new BLL.Rol().ListarRol();

            var RolesFormateados = oLista.Select(r => new
            {
                r.IdRol,
                r.NombreRol
            });

            return Json(new { data = RolesFormateados }, JsonRequestBehavior.AllowGet);
        }

        // GEOLOCALIZACION

        [HttpGet]
        public JsonResult ObtenerEventosGeolocalizados()
        {
            var eventoBLL = new BLL.Evento();
            List<BE.Evento> eventos = eventoBLL.ListarEventosConGeolocalizacion();

            var datos = eventos.Select(ev => new
            {
                ev.IdEvento,
                ev.Nombre,
                ev.Fecha,
                Latitud = ev.Ubicacion?.IdGeolocalizacion?.Latitud ?? 0,
                Longitud = ev.Ubicacion?.IdGeolocalizacion?.Longitud ?? 0,
                Ubicacion = $"{ev.Ubicacion?.Calle} {ev.Ubicacion?.Altura}, {ev.Ubicacion?.Ciudad}, {ev.Ubicacion?.Provincia}"
            }).ToList();

            return Json(datos, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ObtenerUbicacionInicial()
        {
            var ubiBLL = new BLL.Ubicacion();
            var ubicacion = ubiBLL.ObtenerUbicacionInicial();
            if (ubicacion == null) return Json(null, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                ubicacion.IdUbicacion,
                ubicacion.Calle,
                ubicacion.Altura,
                ubicacion.Ciudad,
                ubicacion.Provincia,
                IdGeolocalizacion = ubicacion.IdGeolocalizacion.IdGeolocalizacion,
                Latitud = ubicacion.IdGeolocalizacion.Latitud,
                Longitud = ubicacion.IdGeolocalizacion.Longitud
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GuardarUbicacionInicial(BE.Ubicacion ubicacion)
        {
            try
            {
                var geoBLL = new BLL.Geolocalizacion();
                var ubiBLL = new BLL.Ubicacion();

                var geo = await geoBLL.ObtenerGeolocalizacion(
                    ubicacion.Calle, ubicacion.Altura, ubicacion.Ciudad, ubicacion.Provincia);

                if (geo == null)
                    return Json(new { resultado = false, mensaje = "No se pudo obtener geolocalización." });

                var ubicacionInicial = ubiBLL.ObtenerUbicacionInicial();

                if (ubicacionInicial == null)
                    return Json(new { resultado = false, mensaje = "No hay ubicaciones en la base de datos" });

                ubicacion.IdUbicacion = ubicacionInicial.IdUbicacion;

                new DAL.Ubicacion().ActualizarUbicacionInicial(ubicacion, geo.Latitud, geo.Longitud);

                return Json(new { resultado = true });
            }
            catch (Exception ex)
            {
                return Json(new { resultado = false, mensaje = $"Error: {ex.Message}" });
            }
        }







        //////////////////////////////////////////////////////////////////
        /// <summary>
        /// Muestra la vista de temporadas para el gerente.
        /// </summary>
        [PermisosRol(Models.Rol.Gerente)]
        public ActionResult Temporadas()
        {
            return View();
        }

        [PermisosRol(Models.Rol.Gerente)]
        public ActionResult Geolocalizacion()
        {
            return View();
        }

        /// <summary>
        /// Muestra la vista de usuarios para el administrador.
        /// </summary>
        [PermisosRol(Models.Rol.Administrador)]
        public ActionResult Usuarios()
        {
            return View();
        }

        /// <summary>
        /// Muestra la vista de configuración del sistema para el administrador.
        /// </summary>
        [PermisosRol(Models.Rol.Administrador)]
        public ActionResult ConfigurarSistema()
        {
            return View();
        }

        /// <summary>
        /// Muestra la vista de acceso denegado.
        /// </summary>
        public ActionResult SinPermiso()
        {
            ViewBag.Message = "No tenes permisos.";
            return View();
        }
    }
}