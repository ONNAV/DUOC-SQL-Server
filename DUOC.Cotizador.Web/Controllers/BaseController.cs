using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DUOC.Cotizador.Web.Controllers
{
    public class BaseController
    {
        private static SqlCommand cmd;

        public static DataTable ObtenerTabla(string sql)
        {
            cmd = Conexion.Conectar();

            cmd.CommandText = sql;

            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable("Items");

            dt.Load(reader);

            Conexion.Cerrar();

            return dt;
        }

        public static int Ejecutar(string sql, List<SqlParameter> parametros, bool scalar = false)
        {
            cmd = Conexion.Conectar();

            cmd.CommandText = sql;

            foreach (SqlParameter parameter in parametros)
            {
                cmd.Parameters.Add(parameter);
            }
            int id = 0;
            if (scalar)
            {
                var asdasd = cmd.ExecuteScalar();
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            else
            {
                cmd.ExecuteNonQuery();
            }
            return id;
        }

        public static SqlDataReader ObtenerDatos(string sql)
        {
            cmd = Conexion.Conectar();
            cmd.CommandText = sql;
            SqlDataReader reader = cmd.ExecuteReader();
            //Conexion.Cerrar();
            return reader;
        }

        public static string ExecuteScalar(string sql)
        {
            cmd = Conexion.Conectar();
            cmd.CommandText = sql;
            //Conexion.Cerrar();
            return cmd.ExecuteScalar().ToString(); 
        }
    }
}