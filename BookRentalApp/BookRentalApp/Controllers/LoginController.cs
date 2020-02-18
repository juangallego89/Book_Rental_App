using BookRentalApp.Models.ViewModels;
using BookRentalApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BookRentalApp.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Método que retorna la vista para iniciar sesión en el sistema
        /// </summary>
        /// <returns>Vista de login</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método que retorna la vista para registrar un nuevo usuario
        /// </summary>
        /// <returns>vista para registrar un nuevo usuario</returns>
        public ActionResult Registro()
        {
            return View();
        }

        /// <summary>
        /// Método para autenticar un usuario registrado en el sistema
        /// </summary>
        /// <param name="Usuario">Correo registrado por el usuario</param>
        /// <param name="Password">Contraseña para acceder al sistema</param>
        /// <returns>Vista para acceder a las funcionalidades de administrador o cliente, dependiendo del Rol</returns>
        [HttpPost]
        public ActionResult AutenticarUsuario(string Usuario, String Password) 
        {
            try
            {
                using (BookRentalEntities db = new BookRentalEntities()) 
                {
                    var usuario = (from u in db.Usuarios
                                   where u.Correo == Usuario.Trim()
                                   && u.Contrasena == Password.Trim()
                                   && u.EstadoRegistro == 1
                                   select u).FirstOrDefault();

                    if (usuario == null) 
                    {
                        ViewBag.Error = "Credenciales no válidas";
                        return View("Index");
                    }

                    Session["Usuario"] = usuario;

                    if (usuario.Rol.Trim() == "Administrador")
                    {
                        return RedirectToAction("Index", "Administrador");
                    }
                    if (usuario.Rol.Trim() == "Cliente")
                    {
                        return RedirectToAction("Index", "Cliente");
                    }
                }
                return RedirectToAction("Index", "Login");
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
                return View("~/Shared/Error");
            }
        }

        /// <summary>
        /// Método que registra un nuevo usuario en la base de datos
        /// </summary>
        /// <returns>vista para acceder al sistema</returns>
        [HttpPost]
        public ActionResult RegistrarUsuario(Usuario model)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    ViewBag.Error = "Ocurrió un error al validar el validar los datos del usuario.";
                    return View("~/Shared/Error");
                }
                using (BookRentalEntities db = new BookRentalEntities())
                {
                    var usuario = new Usuarios
                    {
                        PrimerNombre = model.PrimerNombre,
                        SegundoNombre = model.SegundoNombre,
                        PrimerApellido = model.PrimerApellido,
                        SegundoApellido = model.SegundoApellido,
                        Correo = model.Correo,
                        Contrasena = model.Contrasena,
                        Telefono = model.Telefono,
                        Direccion = model.Direccion,
                        Documento = model.Documento,
                        TipoDocumento = model.TipoDocumento,
                        Rol = "Cliente",
                        Estado = "Activo"
                    };

                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View("Index");
        }

        /// <summary>
        /// Método que finaliza la sesión del usuario
        /// </summary>
        /// <returns>vista para acceder al sistema</returns>
        public ActionResult CerrarSesion() 
        {
            Session["Usuario"] = null;
            return View("Index");
        }
    }
}
