using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IUVendedor.Models;
using IUVendedor.Data;
using System.Web.Security;  
using System.Security.Principal; 


namespace IUVendedor.Controllers
{
    /// <summary>
    /// Controlador responsable de la autenticación y acceso de usuarios.
    /// </summary>
    public class AccesoController : Controller
    {
        /// <summary>
        /// Muestra la vista de inicio de sesión.
        /// </summary>
        /// <returns>Vista de inicio de sesión.</returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Procesa el inicio de sesión del usuario.
        /// </summary>
        /// <param name="_usuario">Objeto Usuario con los datos ingresados.</param>
        /// <returns>Redirige al Home si es exitoso, de lo contrario muestra error.</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(Usuario _usuario)
        {
            System.Diagnostics.Debug.WriteLine($"Intento login con Email: {_usuario.Email} / Password: {_usuario.PasswordHash}");

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

        /// <summary>
        /// Cierra la sesión del usuario actual.
        /// </summary>
        /// <returns>Redirige a la vista de inicio de sesión.</returns>
        public ActionResult Salir()
        {
            Session["usuario"] = null;
            return RedirectToAction("Index", "Acceso");
        }
    }
}