using DUOC.Cotizador.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DUOC.Cotizador.Web.Controllers
{
    public class CotizadorController : Controller
    {
        // GET: Cotizador
        public ActionResult Index()
        {

            var productos = BaseController.ObtenerDatos("SELECT IDProducto, NombreProducto FROM Productos");
            var proveedores = BaseController.ObtenerDatos("SELECT IDProveedor, NombreProveedor FROM Proveedores");
            ViewBag.Productos = new MultiSelectList(productos, "IDProducto", "NombreProducto");
            ViewBag.Proveedores = new MultiSelectList(proveedores, "IDProveedor", "NombreProveedor");
            return View();
        }
        [HttpGet]
        public ActionResult getProductos(string get)
        {
            Uri UriRequest = new Uri(this.Request.Url.AbsoluteUri);
            string IDsProveedores = HttpUtility.ParseQueryString(UriRequest.Query).Get("prov[]");
            List<SelectModels> listaProductos = new List<SelectModels>();
            if (IDsProveedores != "")
            {
                var reader = BaseController.ObtenerDatos("SELECT IDProducto, NombreProducto + ' (' + CAST(ValorProducto AS VARCHAR) +' USD)' NombreProducto FROM Productos WHERE IDProveedor IN (" + IDsProveedores + ")");

                while (reader.Read())
                {
                    listaProductos.Add(new SelectModels
                    {
                        id = Convert.ToInt32(reader["IDProducto"]),
                        text = reader["NombreProducto"].ToString()
                    });
                }
            }
            return Json(listaProductos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult generarCotizacion()
        {
            var productos = Request.Form[1];
            var fechaCalculo = Request.Form[2];
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IDProductos", productos.ToString()));
            parametros.Add(new SqlParameter("@FechaCalculo", fechaCalculo.ToString()));
            var id = BaseController.ExecuteScalar("SP_GeneraCotizacion '" + productos.ToString() + "', '" + fechaCalculo.ToString() + "'");
            Conexion.Cerrar();
            return Json(new { ID = id }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult generarCotizacionTBL(int id = 0)
        {
            DataTableModels dataTable = new DataTableModels();
            dataTable.draw = 1;
            var reader = BaseController.ObtenerDatos("CalculoCotizacion " + id.ToString());
            List<CotizacionModels> listaProductos = new List<CotizacionModels>();

            while (reader.Read())
            {

                CotizacionModels c = new CotizacionModels
                {
                    Proveedor = reader["PROVEEDOR"].ToString(),
                    Producto = reader["NombreProducto"].ToString(),
                    ValorPesos = (int)(Int64)reader["VALOR_PESOS"],
                    ValorCambio = Convert.ToDateTime(reader["FechaCambio"]).ToString("dd-MM-yyyy")
                };
                listaProductos.Add(c);
            }

            dataTable.data = listaProductos.ToArray();
            dataTable.recordsTotal = listaProductos.Count;
            dataTable.recordsFiltered = listaProductos.Count();
            return Json(dataTable, JsonRequestBehavior.AllowGet);
        }
    }
}