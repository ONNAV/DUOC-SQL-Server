using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DUOC.Cotizador.Web.Models
{
    public class CotizacionModels
    {
        public string Proveedor { get; set; }
        public string Producto { get; set; }
        public int ValorPesos { get; set; }
        public string ValorCambio { get; set; }
        public int Total { get; set; }
    }
}