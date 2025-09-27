using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace NexoMarket
{
    /// <summary>
    /// Descripción breve de EstadisticaService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class EstadisticaService : System.Web.Services.WebService
    {

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hola a todos";
        //}

        [WebMethod]
        public DataTable GetTopProductos(DataTable datosSP)
        {
            // Creamos la tabla de salida
            DataTable resultado = new DataTable();
            resultado.Columns.Add("Orden", typeof(int));
            resultado.Columns.Add("Nombre", typeof(string));
            resultado.Columns.Add("Facturacion", typeof(decimal));
            resultado.Columns.Add("Porcentaje", typeof(decimal));
            resultado.Columns.Add("Rentabilidad", typeof(decimal));

            if (datosSP.Rows.Count == 0)
                return resultado;

            // Tomamos el total de facturación (columna repetida en el SP)
            decimal totalFacturacion = Convert.ToDecimal(datosSP.Rows[0]["TotalFacturacion"]);

            int orden = 1;
            foreach (DataRow row in datosSP.Rows)
            {
                string nombre = row["NombreProducto"].ToString();
                decimal monto = Convert.ToDecimal(row["Monto"]);
                int cantidad = Convert.ToInt32(row["Cantidad"]);

                decimal porcentaje = totalFacturacion > 0 ? (monto / totalFacturacion) * 100 : 0;
                decimal rentabilidad = cantidad > 0 ? monto / cantidad : 0;

                // Agregamos fila procesada
                resultado.Rows.Add(orden, nombre, monto, Math.Round(porcentaje, 2), Math.Round(rentabilidad, 2));
                orden++;
            }

            return resultado;
        }

    }
}
