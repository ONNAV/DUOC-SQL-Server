using DUOC.Cotizador.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DUOC.Cotizador.Web.Controllers
{
    public class LoMasCotizadoController : Controller
    {
        // GET: LoMasCotizado
        public ActionResult Index()
        {
            var proveedores = BaseController.ObtenerDatos("SELECT IDProveedor, NombreProveedor FROM Proveedores");
            ViewBag.Proveedores = new MultiSelectList(proveedores, "IDProveedor", "NombreProveedor");
            return View();
        }

        [HttpPost]
        public ActionResult CotizacionesTBL()
        {
            var Rangofechas = Request.Form[29];
            string IDsProveedores = Request.Form[30];
            DataTableModels dataTable = new DataTableModels
            {
                draw = 1
            };
            var reader = BaseController.ObtenerDatos("SP_MasCotizados '" + Rangofechas.ToString() + "','" + IDsProveedores.ToString() + "'");
            List<CotizacionModels> listaProductos = new List<CotizacionModels>();

            while (reader.Read())
            {

                CotizacionModels c = new CotizacionModels
                {
                    Proveedor = reader["PROVEEDOR"].ToString(),
                    Producto = reader["NombreProducto"].ToString(),
                    ValorPesos = (int)(Int64)reader["VALOR_PESOS"],
                    Total = Convert.ToInt32(reader["TOTAL"]),
                };
                listaProductos.Add(c);
            }
            Conexion.Cerrar();
            dataTable.data = listaProductos.ToArray();
            dataTable.recordsTotal = listaProductos.Count;
            dataTable.recordsFiltered = listaProductos.Count();
            return Json(dataTable, JsonRequestBehavior.AllowGet);

        }
    }
}