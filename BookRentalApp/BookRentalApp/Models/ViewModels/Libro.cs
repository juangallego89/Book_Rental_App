using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRentalApp.Models.ViewModels
{
    /// <summary>
    /// Modelo para mapear la data de un libro
    /// </summary>
    public class Libro
    {
        public long ID_Libro { get; set; }
        [Required]
        public long ID_Tarifa { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public string Categoria { get; set; }
        [Required]
        public int Ejemplares { get; set; }
        public string Imagen { get; set; }
        public string Estado { get; set; }
        public int EstadoRegistro { get; set; }
    }
}