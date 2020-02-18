using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRentalApp.Models
{
    /// <summary>
    /// Modelo para el objeto transaction, el cual almacena el resultado de una transacción
    /// </summary>
    public class Transaction
    {
        public bool Estado { get; set; }
        public string Message { get; set; }
    }
}