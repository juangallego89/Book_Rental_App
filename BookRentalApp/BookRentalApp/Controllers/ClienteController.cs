using BookRentalApp.Models;
using BookRentalApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace BookRentalApp.Controllers
{
    public class ClienteController : Controller
    {

        /// <summary>
        /// Método que retorna la vista con el catálogo de los libros para rentar.
        /// </summary>
        /// <returns>Vista con el catálogo de los libros disponibles para rentar.</returns>
        public ActionResult Index()
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
                                       Imagen = d.Imagen.Trim()                                     
                                   }).ToList();

                    if (listaLibros == null)
                    {
                        ViewBag.Error = "No fue posible obtener el listado de libros";
                        return View("~/Views/Shared/Error.cshtml");
                    }

                    return View(listaLibros);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        /// <summary>
        /// Método que retorna la vista para alquilar un ejemplar
        /// </summary>
        /// <returns>vista con la información del libro a rentar</returns>
        /// <param name="IdLibro">id del libro a rentar</param>
        public ActionResult DetalleLibro(int IdLibro)
        {

            try
            {
                var libro = new Libros();

                using (BookRentalEntities db = new BookRentalEntities())
                {
                    libro = (from l in db.Libros
                             where l.ID_Libro == IdLibro
                             && l.EstadoRegistro == 1
                             && l.Estado == "Activo"
                             select l).FirstOrDefault();

                    if (libro == null)
                    {
                        ViewBag.Error = "Error al obtener la información del libro";
                        return View("~/Views/Shared/Error.cshtml");
                    }

                }
                return View(libro);
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        /// <summary>
        /// Método que registra el alquiler de un libro
        /// </summary>
        /// <returns></returns>
        /// <param name="IdLibro">Id del libro a rentar</param>
        /// <param name="FechaEntrega">Fecha de devolución del libro</param>
        public ActionResult Alquilar(int IdLibro, string FechaEntrega)
        {          
            try
            {
                var tarifa = new Tarifas();

                using (BookRentalEntities db = new BookRentalEntities())
                {
                    tarifa = (from t in db.Tarifas
                             where t.ID_Libro == IdLibro
                             && t.Estado == "Activo"
                             && t.EstadoRegistro == 1
                             select t).FirstOrDefault();

                    if (tarifa == null)
                    {
                        ViewBag.Error = "Error al obtener la tarifa de alquiler";
                        return View("~/Views/Shared/Error.cshtml");
                    }

                    var CantidadDias = Convert.ToDateTime(FechaEntrega) - DateTime.Now;
                    var ValorReserva = tarifa.Tarifa * CantidadDias.Days;
                    var usuario =  System.Web.HttpContext.Current.Session["ID_Usuario"] as String;//Validar como obtener la variable de sesion

                    var reserva = new Reservas
                    {
                        ID_Usuario = 2, //Obtener id de usuario de la sesión
                        ValorReserva = ValorReserva,
                        ID_Libro = IdLibro,
                        FechaEntrega = Convert.ToDateTime(FechaEntrega)
                    };

                    db.Reservas.Add(reserva);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}