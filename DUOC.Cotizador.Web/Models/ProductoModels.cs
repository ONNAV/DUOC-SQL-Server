using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DUOC.Cotizador.Web.Models
{
    public class ProductoModels
    {
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public float ValorUF { get; set; }
        public float ValorUSD { get; set; }
        public float ValorEURO { get; set; }
        public int IDProveedor { get; set; }
    }
}