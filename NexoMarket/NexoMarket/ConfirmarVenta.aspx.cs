using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NexoMarket.Business;
using NexoMarket.Data;

namespace NexoMarket.NexoMarket
{
    public partial class ConfirmarVenta : System.Web.UI.Page
    {
        ProductoBusiness _businessProducto = new ProductoBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            var Products = _businessProducto.GetProductsFromCart();
            decimal subtotal = 0;
            foreach (var p in Products)
            {
                subtotal += p.Product.Precio * p.Cantidad;
            }
            lblTotal.Text = subtotal.ToString("N2");
        }
        
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {                          
            // 1) Leer campos
            var calle = txtCalle.Text.Trim();
            var numero = txtNumero.Text.Trim();
            var pisoDepto = txtPisoDepto.Text.Trim();
            var ciudad = txtCiudad.Text.Trim();
            var cp = txtCP.Text.Trim();
            var aclaraciones = txtAclaraciones.Text.Trim();

            // 2) (Opcional) Componer dirección completa
            var direccion = $"{calle} {numero}";
            if (!string.IsNullOrEmpty(pisoDepto)) direccion += $" - {pisoDepto}";
            direccion += $", {ciudad} ({cp})";

            // 3) Guardar en BD la venta + dirección + items del carrito
            // TODO: tu lógica de persistencia aquí

            // 4) Redirigir a "gracias" o mostrar comprobante
            // Response.Redirect("Gracias.aspx");
        }
    }
}