using System;
using System.Web;
using System.Web.Mvc;
using BookRentalApp.Models;

namespace BookRentalApp.Filters
{
    /// <summary>
    /// Clase usada para verificar los permisos de un usuario logeado en el sistema
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class Permissions : AuthorizeAttribute
    {
        private Usuarios usuario;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                usuario = (Usuarios)HttpContext.Current.Session["Usuario"];

                if (usuario.Rol.Trim() != "Administrador") 
                {
                    filterContext.Result = new RedirectResult("~/Administrador/PermissionsError");
                }
            }
            catch (Exception)
            {              
                filterContext.Result = new RedirectResult("~/Administrador/PermissionsError");
            }
        }
    }
}