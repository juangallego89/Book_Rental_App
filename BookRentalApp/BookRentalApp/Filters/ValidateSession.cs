using BookRentalApp.Controllers;
using BookRentalApp.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace BookRentalApp.Filters
{
    /// <summary>
    /// Clase para validar si un usuario tiene una sesión activa.
    /// </summary>
    public class ValidateSession : ActionFilterAttribute
    {
        private Usuarios usuario;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {         
            try
            {
                base.OnActionExecuting(filterContext);

                usuario = (Usuarios)HttpContext.Current.Session["Usuario"];

                if (usuario == null) 
                {
                    if (filterContext.Controller is LoginController == false) 
                    {
                        filterContext.HttpContext.Response.Redirect("/Login/Index");
                    }
                }

            }
            catch (Exception)
            {

                filterContext.Result = new RedirectResult("~/Login/Index");
            }

        }
    }
}