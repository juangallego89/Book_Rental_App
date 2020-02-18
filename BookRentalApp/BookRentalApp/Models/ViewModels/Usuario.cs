using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRentalApp.Models.ViewModels
{
    /// <summary>
    /// Modelo usado para mapear la data de la vista registrar un nuevo usuario
    /// </summary>
    public class Usuario
    {
        public long ID_Usuario { get; set; }
        [Required]
        [StringLength(30)]
        public string PrimerNombre { get; set; }
        [StringLength(30)]
        public string SegundoNombre { get; set; }
        [Required]
        [StringLength(30)]
        public string PrimerApellido { get; set; }
        [StringLength(30)]
        public string SegundoApellido { get; set; }
        [Required]
        [StringLength(30)]
        public string Correo { get; set; }
        [Required]
        [StringLength(30)]
        public string Contrasena { get; set; }
        [Required]
        [StringLength(10)]
        public string Telefono { get; set; }
        [Required]
        [StringLength(10)]
        public string Direccion { get; set; }
        [Required]
        [StringLength(20)]
        public string Documento { get; set; }
        [Required]
        [StringLength(4)]
        public string TipoDocumento { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }
        public int EstadoRegistro { get; set; }
    }
}