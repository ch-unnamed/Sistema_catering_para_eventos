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
using System.Web.Services.Description;

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

        [HttpGet]
        public JsonResult ListarMenusVendedor()
        {
            List<BE.Menu> oLista = new BLL.Menu().ListarMenusVendedor();

            // proyecto solo el campo nombre
            var menusFiltrados = oLista.Select(
                menu => new
                {
                    menu.Id,
                    menu.Nombre
                });

            return Json(new { data = menusFiltrados }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarEstados()
        {
            List<BE.Estado> oLista = new BLL.Estado().Listar();

            // proyecto solo el campo nombre
            var estadosFiltrados = oLista.Select(
                estado => new
                {
                    estado.IdEstado,
                    estado.Nombre
                });

            return Json(new { data = estadosFiltrados }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerNombresPlatosPorMenu(int menu_id)
        {
            // creo un objeto Menu_Plato y le asigno un Menu con el id recibido
            BE.Menu_Plato menuPlato = new BE.Menu_Plato();
            menuPlato.Menu = new BE.Menu();
            menuPlato.Menu.Id = menu_id;

            List<BE.Menu_Plato> listaPlatos = new BLL.Menu_Plato().ObtenerNombresPlatosPorMenu(menuPlato);

            // lista de platos con nombre
            var platos = listaPlatos.Select(mp => new
            {
                IdPlato = mp.Plato.Id,
                Nombre = mp.Plato.Nombre,
                Descripcion = mp.Plato.Descripcion,
                Precio = mp.Plato.Precio
            }).ToList(); // es lista para que dentro del id de menu se ponga todos los platos correspondientes

            // estructura final
            var resultado = new
            {
                menu_id = menu_id,
                platos = platos
            };

            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ObtenerCapacidadPorIdEvento(int evento_id)
        {
            BE.Evento evento = new BLL.Evento().ObtenerCapacidadPorIdEvento(evento_id);

            var resultado = new
            {
                IdEvento = evento.IdEvento,
                Capacidad = evento.Capacidad
            };

            if (evento != null)
            {
                return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { error = "Evento no encontrado" }, JsonRequestBehavior.AllowGet);
        }

        //plato para cotizacion
        [HttpPost]
        public JsonResult InsertarPlatosCotizacionPersonalizada(int eventoId, int clienteId, List<MenuConPlatosDTO> menus, DateTime fechaRealizacion, decimal total,
            int estado_id, int vendedor_id)
        {
            try
            {
                // Crear objetos BE
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

                // devuelvo una respuesta con ID generado
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

        [HttpGet]
        public JsonResult ObtenerPorcentajeGananciaGeneral(string nombre)
        {
            nombre = "Ganancia General";

            BE.Configuracion_Empresa configuracion = new BLL.Configuracion_Empresa().ObtenerPorcentajeGanancia(nombre);


            var resultado = new
            {
                PorcentajeGanancia = configuracion.PorcentajeGanancia
            };

            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult ObtenerPorcentajeDescuentoPrimeraVez(string nombre)
        {
            nombre = "Primera vez ";

            BE.Configuracion_Empresa configuracion = new BLL.Configuracion_Empresa().ObtenerPorcentajeGanancia(nombre);


            var resultado = new
            {
                PorcentajeGanancia = configuracion.PorcentajeGanancia
            };

            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult ObtenerPorcentajeDescuentoMasDe3Menus(string nombre)
        {
            nombre = "Mas de 3 menus";

            BE.Configuracion_Empresa configuracion = new BLL.Configuracion_Empresa().ObtenerPorcentajeGanancia(nombre);


            var resultado = new
            {
                PorcentajeGanancia = configuracion.PorcentajeGanancia
            };

            return Json(new { data = resultado }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult consultarCliente (int cliente_id)
        {
            int configuracion = new BLL.Configuracion_Empresa().consultarCliente(cliente_id);

            return Json(new { data = configuracion }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult cantidadCliente (int cliente_id)
        {
            int cliente = new BLL.Cliente().cantidadCliente(cliente_id);

            return Json(new { data = cliente }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult cantidadEvento(int evento_id)
        {
            int evento = new BLL.Evento().cantidadEvento(evento_id);

            return Json(new { data = evento }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult nombreEvento(int evento_id)
        {
            string evento = new BLL.Evento().nombreEvento(evento_id);

            return Json(new { data = evento }, JsonRequestBehavior.AllowGet);

        }

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

        [HttpPost]
        public JsonResult EliminarCotizacion(int cotizacion_id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new BLL.Cotizacion().EliminarCotizacion(cotizacion_id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditarCotizacion(BE.Cotizacion cotizacion)
        {
            object resultado;
            string mensaje = string.Empty;

            resultado = new BLL.Cotizacion().EditarCotizacion(cotizacion, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

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

        [HttpPost]
        public JsonResult EnviarMailCotizacion(string email, string html)
        {
            bool enviado = new BLL.Cotizacion().EnviarMailCotizacion(email, html);
            return Json(new { success = enviado }, JsonRequestBehavior.AllowGet);
        }



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

        [HttpPost]
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


        [PermisosRol(Models.Rol.Chef)]
        public ActionResult Platos()
        {
            return View();
        }

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

        [HttpPost]
        public JsonResult EliminarPlato(int idPlato)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new BLL.Plato().EliminarPlato(idPlato, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerInsumosDelPlato(int plato_id)
        {
            // creo un objeto PlatoInsumo y le asigno un Plato con el id recibido
            BE.PlatoInsumo platoInsumo = new BE.PlatoInsumo();
            platoInsumo.Plato = new BE.Plato();
            platoInsumo.Plato.Id = plato_id;

            // obtengo la lista de insumos asociados al plato seleccionado
            List<BE.PlatoInsumo> listaInsumos = new BLL.PlatoInsumo().ObtenerInsumosDelPlato(platoInsumo);

            // proyecto la lista para devolver los campos necesarios
            var insumos = listaInsumos.Select(p_i => new
            {
                Id = p_i.Insumo.Id,
                Nombre = p_i.Insumo.Nombre,
                TipoNombre = p_i.Insumo.TipoNombre
            }).ToList();

            return Json(new { data = insumos }, JsonRequestBehavior.AllowGet);
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

        
        
        
        [PermisosRol(Models.Rol.Chef)]
        public ActionResult Menus()
        {
            return View();
        }

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

        [HttpGet]
        public JsonResult ObtenerPlatosDelMenu(int menu_id)
        {
            // creo un objeto Menu_Plato y le asigno un Menu con el id recibido
            BE.Menu_Plato menuPlato = new BE.Menu_Plato();
            menuPlato.Menu = new BE.Menu();
            menuPlato.Menu.Id = menu_id;

            // obtengo la lista de platos asociados al menú seleccionado
            List<BE.Menu_Plato> listaPlatos = new BLL.Menu_Plato().ObtenerPlatosDelMenu(menuPlato);

            // proyecto la lista para devolver los campos necesarios
            var platos = listaPlatos.Select(mp => new
            {
                Id = mp.Plato.Id,
                Nombre = mp.Plato.Nombre,
                Precio = mp.Plato.Precio,
                Descripcion = mp.Plato.Descripcion
            }).ToList();

            return Json(new { data = platos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMenu(BE.Menu menu)
        {
            object resultado;
            string mensaje = string.Empty;

            if (menu.Id == 0)
            {
                resultado = new BLL.Menu().CrearMenu(menu, out mensaje);
            }
            else // TODO
            {
                resultado = new BLL.Menu().EditarMenu(menu, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarMenu(int idMenu)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new BLL.Menu().EliminarMenu(idMenu, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
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
            catch (Exception)
            {
                //Control de error
                return Json(new { success = false, message = "Error al obtener los tipos de insumo." }, JsonRequestBehavior.AllowGet);
            }
        }


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
        public JsonResult ListarUsuario()
        {
            List<BE.Usuario> oLista = new List<BE.Usuario>();

            oLista = new BLL.Usuario().ListarUsuario();

            var UsuariosFormateados = oLista.Select(u => new
            {
                u.IdUsuario,
                u.Email,
                u.FechaCreacion,
                u.Nombre,
                u.Apellido
            });

            return Json(new { data = UsuariosFormateados }, JsonRequestBehavior.AllowGet);
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

        //////////////////////////////////////////////////////////////////
        public ActionResult SinPermiso()
        {
            ViewBag.Message = "No tenes permisos.";

            return View();
        }
    }
}