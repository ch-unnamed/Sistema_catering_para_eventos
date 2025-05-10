using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IUVendedor.Models;
using IUVendedor.Models.Data;
using System.Web.Security;  // Para manejar la autenticación con Forms Authentication
using System.Security.Principal; // Para obtener información del usuario autenticado


namespace IUVendedor.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario _usuario)
        {
            
            DA_Logica _da_usuario = new DA_Logica();

            var usuario = _da_usuario.ValidarUsuario(_usuario.Correo, _usuario.Clave);

            // Si encuentra un usuario lo redirijo a su dashboard correspondinete
            if (usuario != null)
            {
                // Crear la cookie de autenticación
                FormsAuthentication.SetAuthCookie(usuario.Correo, false);

                Session["usuario"] = usuario;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Salir()
        {
            FormsAuthentication.SignOut(); // Cierra la sesión del usuario
            Session["usuario"] = null;

            return RedirectToAction("Index", "Acceso");
        }
    }
}