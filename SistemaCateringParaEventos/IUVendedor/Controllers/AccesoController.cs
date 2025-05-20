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
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(Usuario _usuario)
        {
            DA_Logica _da_usuario = new DA_Logica();
            var usuario = _da_usuario.ValidarUsuario(_usuario.Email, _usuario.PasswordHash);

            if (usuario != null)
            {
                FormsAuthentication.SetAuthCookie(usuario.Email, false);
                Session["usuario"] = usuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View();
            }
        }

        public ActionResult Salir()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Acceso"); 
        }


    }
}