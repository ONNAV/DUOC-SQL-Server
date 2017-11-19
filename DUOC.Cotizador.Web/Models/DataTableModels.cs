using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DUOC.Cotizador.Web.Models
{
    public class DataTableModels
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public Array data { get; set; }
    }
}