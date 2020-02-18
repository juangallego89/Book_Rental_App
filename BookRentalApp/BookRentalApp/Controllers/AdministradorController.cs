using BookRentalApp.Filters;
using BookRentalApp.Models;
using BookRentalApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookRentalApp.Controllers
{
    public class AdministradorController : Controller
    {
        /// <summary>
        /// Método que carga la vista principal del Administrador
        /// </summary>
        /// <returns>vista para administrar los libro</returns>
        [Permissions]
        public ActionResult Index()
        {
            return View("AdministrarInventario");
        }
        /// <summary>
        /// Método que carga la vista con el formulario para agregar un nuevo libro
        /// </summary>
        /// <returns>vista para agregar un nuevo libro</returns>
        [Permissions]
        public ActionResult AgregarLibro()
        {
            return View();
        }
        /// <summary>
        /// Método que carga la vista para crear una tarifa
        /// </summary>
        /// <returns>vista para crear una tarifa</returns>
        [Permissions]
        public ActionResult AgregarTarifa()
        {
            return View();
        }
        /// <summary>
        /// Método que carga la vista para administrar el inventario de libros
        /// </summary>
        /// <returns>vista para administrar la cantidad de libros</returns>
        [Permissions]
        public ActionResult AdministrarInventario()
        {
            return View();
        }
        /// <summary>
        /// Método que carga la vista para administrar las tarifas
        /// </summary>
        /// <returns>vista para administrar las tarifas</returns>
        [Permissions]
        public ActionResult AdministrarTarifas()
        {
            return View();
        }

        /// <summary>
        /// Método que carga la vista para editar un libro
        /// </summary>
        /// <param name="IdLibro">Id del libro a editar</param>
        /// <returns>vista para editar un libro</returns>
        [Permissions]
        public ActionResult EditarLibro(int IdLibro)
        {
            try
            {
                using (BookRentalEntities db = new BookRentalEntities())
                {
                     var libro = (from l in db.Libros
                                 where l.ID_Libro == IdLibro
                                 && l.EstadoRegistro == 1
                                 select l).FirstOrDefault();

                    if (libro == null)
                    {
                        ViewBag.Error = "No fue posible obtener la información del libro";
                        return View("~/Views/Shared/Error.cshtml");
                    }

                    libro.ID_Libro = IdLibro;

                    return View(libro);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        /// <summary>
        /// Método que carga la vista para editar las tarifas de alquiler de los libros
        /// </summary>
        /// <returns>vista para editar las tarifas</returns>
        /// <summary>
        [Permissions]
        public ActionResult EditarTarifa(int IdTarifa)
        {
            try
            {
                using (BookRentalEntities db = new BookRentalEntities())
                {
                    var tarifa = (from t in db.Tarifas
                                 where t.ID_Tarifa == IdTarifa
                                 && t.EstadoRegistro == 1
                                 select t).FirstOrDefault();

                    if (tarifa == null)
                    {
                        ViewBag.Error = "No fue posible obtener la información de la tarifa";
                        return View("~/Views/Shared/Error.cshtml");
                    }
                    return View(tarifa);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        /// <summary>
        /// Método que trae el listado de libros registrados en el sistema
        /// </summary>
        /// <returns>Json con el listado de los libros registrados</returns>
        [Permissions]
        public ActionResult ObtenerLibros() 
        {
            try
            {
                List<Libro> listaLibros;

                using (BookRentalEntities db = new BookRentalEntities())
                {
                    listaLibros = (from d in db.Libros
                                   where d.EstadoRegistro == 1
                                   select new Libro
                                   {
                                       ID_Libro = d.ID_Libro,
                                       Titulo = d.Titulo,
                                       Autor = d.Autor,
                                       Categoria = d.Categoria,
                                       Ejemplares = d.Ejemplares,
                                       Imagen = d.Imagen
                                   }).ToList();

                    if (listaLibros == null)
                    {
                        ViewBag.Error = "No fue posible obtener el listado de libros";
                        return View("~/Views/Shared/Error.cshtml");
                    }

                    return Json(new { data = listaLibros }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }              
        }

        /// <summary>
        /// Método que registra un nuevo libro en la base de datos
        /// </summary>
        /// <returns>Vista para registrar un nuevo libro</returns>
        [Permissions]
        [HttpPost]
        public ActionResult AgregarLibro(Libro model) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = ModelState;
                    return View();
                }

                model.ID_Tarifa = 1;//agregar DropDown en la vista para seleccionar las tarifas
                using (BookRentalEntities db = new BookRentalEntities()) 
                {
                    var libro = new Libros
                    {
                        Autor = model.Autor,
                        Titulo = model.Titulo,
                        Ejemplares = model.Ejemplares,
                        Categoria = model.Categoria,
                        Estado = "Activo",
                        EstadoRegistro = 1,
                        ID_Tarifa = model.ID_Tarifa
                    };

                    db.Libros.Add(libro);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al procesar la transacción.  " + ex.Message;
                return View("~/Views/Shared/Error.cshtml");
            }
            
            return View();
        }

        /// <summary>
        /// Actualiza un libro registrado 
        /// </summary>
        /// <param name="model">data del libro a actualizar</param>
        /// <returns>Json con el estado de la transacción</returns>
        [Permissions]
        [HttpPost]
        public ActionResult ActualizarLibro(Libro model)
        {
            var transaction = new Transaction();

            try
            {
                using (BookRentalEntities db = new BookRentalEntities())
                {
                    var libro = db.Libros.Find(model.ID_Libro);

                    libro.Titulo = model.Titulo;
                    libro.Autor = model.Autor;
                    libro.Categoria = model.Categoria;
                    libro.Ejemplares = model.Ejemplares;

                    db.Entry(libro).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    if (libro == null)
                    {
                        transaction.Estado = false;
                        transaction.Message = "Ocurrió un error al actualizar el libro en la base de datos.";
                        return Json(new { data = transaction }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Estado = false;
                transaction.Message = "Ocurrió un error al actualizar el libro en la base de datos. - " + ex.Message;
            }
            transaction.Estado = true;
            transaction.Message = "Se actualizó exitosamente el libro";
            return View("AdministrarInventario");
            //return Json(new { data = transaction }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Elimina un libro registrado 
        /// </summary>
        /// <param name="IdLibro">Id del libro a eliminar</param>
        /// <returns>Json con el estado de la transacción</returns>
        [Permissions]
        public ActionResult EliminarLibro(int IdLibro) 
        {
           var transaction = new Transaction();

            try
            {
                using (BookRentalEntities db = new BookRentalEntities()) 
                {
                    var libro = db.Libros.SingleOrDefault(b => b.ID_Libro == IdLibro);

                    if (libro == null)
                    {
                        transaction.Estado = false;
                        transaction.Message = "Ocurrió un error al intentar eliminar el libro de la base de datos";
                        return Json(new { data = transaction }, JsonRequestBehavior.AllowGet);
                    }

                    // EstadoRegistro = 2 Eliminado
                    // EstadoRegistro = 1 No Eliminado
                    libro.EstadoRegistro = 2;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                transaction.Estado = false;
                transaction.Message = "Ocurrió un error al intentar eliminar el libro de la base de datos. " + ex.Message;
            }
            transaction.Estado = true;
            transaction.Message = "Se eliminó exitosamente el libro";

            return View("AdministrarInventario");

            //return Json(new { data = transaction }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Método que registra una nueva tarifa en la base de datos
        /// </summary>
        /// <returns>Vista para registrar una nueva tarifa</returns>
        [Permissions]
        [HttpPost]
        public ActionResult AgregarTarifa(Tarifas model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = ModelState;
                    return View();
                }
                using (BookRentalEntities db = new BookRentalEntities())
                {
                    var tarifa = new Tarifas
                    {
                        Nombre = model.Nombre,
                        Tarifa = model.Tarifa,
                        Estado = "Activo",
                        EstadoRegistro = 1
                    };

                    db.Tarifas.Add(tarifa);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al procesar la transacción.  " + ex.Message;
                return View("~/Views/Shared/Error.cshtml");
            }

            return View();
        }
        /// <summary>
        /// Método que trae el listado de tarifas
        /// </summary>
        /// <returns>Json con el listado de las  tarifas</returns>
        [Permissions]
        public ActionResult ObtenerTarifas()
        {
            try
            {
                List<TarifasViewModel> ListaTarifas;

                using (BookRentalEntities db = new BookRentalEntities())
                {
                    ListaTarifas = (from d in db.Tarifas
                                    where d.EstadoRegistro == 1
                                   && d.Estado == "Activo"
                                   select new TarifasViewModel
                                   {
                                       ID_Tarifa = d.ID_Tarifa,
                                       Nombre = d.Nombre,
                                       Tarifa = d.Tarifa
                                   }).ToList();

                    if (ListaTarifas == null)
                    {
                        ViewBag.Error = "No fue posible obtener el listado de tarifas";
                        return View("~/Views/Shared/Error.cshtml");
                    }

                    return Json(new { data = ListaTarifas }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método que retorna la vista de usuario sin permisos.
        /// </summary>
        /// <returns>vista PermissionsError</returns>
        public ActionResult PermissionsError() 
        {
            return View("~/Views/Shared/PermissionsError.cshtml");
        }


    }
}