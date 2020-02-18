using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRentalApp.Models.ViewModels
{
    /// <summary>
    /// Modelo usado para mapear la data de la vista administrar tarifas
    /// </summary>
    public class TarifasViewModel
    {
        public long ID_Tarifa { get; set; }
        public Nullable<long> ID_Libro { get; set; }
        public string Nombre { get; set; }
        public decimal Tarifa { get; set; }
        public string Estado { get; set; }
        public int EstadoRegistro { get; set; }
    }
}