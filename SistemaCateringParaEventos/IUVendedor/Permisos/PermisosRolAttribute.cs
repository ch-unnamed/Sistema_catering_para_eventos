using IUVendedor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IUVendedor.Permisos
{
	public class PermisosRolAttribute : ActionFilterAttribute //para evaluar el permiso en el controller antes de mostrar
	{
		private readonly List<Rol> rolesPermitidos;

    public PermisosRolAttribute(params Rol[] roles)// constructor que recibe los roles permitidos y los almacena en una lista
    {
        rolesPermitidos = roles.ToList();
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var usuario = HttpContext.Current.Session["Usuario"] as Usuario; //obtener el usuario desde la sesion actual
            
        if (usuario == null || !rolesPermitidos.Contains(usuario.Rol_id)) // no va a pasar pero es para preveer
        {
            filterContext.Result = new RedirectResult("~/Home/SinPermiso");
        }
        // si el usuario tiene el permiso continua
        base.OnActionExecuting(filterContext);
    }

    }
}